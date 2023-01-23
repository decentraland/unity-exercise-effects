using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	public class DamageSystem
	{
		private readonly IntentionsRegistry<HitIntention> _hitsIntentions;

		public DamageSystem(IntentionsRegistry<HitIntention> hitsIntentions)
		{
			_hitsIntentions = hitsIntentions;
		}
		
		public void Update(ICollection<IUnit> deadUnits)
		{
			foreach (var intention in _hitsIntentions.GetIntentions())
			{
				var unit = intention.Key;
				var intentions = intention.Value;

				if (unit.TryGetModule(out HealthModule healthModule) && intentions.Count > 0)
				{
					foreach (var hitIntention in intentions)
					{
						var defense = unit.TryGetModule(out DefenseModule defenseModule) ? defenseModule.Settings.Defense : 0;
						var damage = Mathf.Max(hitIntention.Source.Attack - defense, 0);
						healthModule.CurrentHealth -= damage;
					}
					
					if (healthModule.CurrentHealth <= 0)
					{
						unit.AllyArmy.RemoveUnit(unit);
						unit.SetTrigger(healthModule.DeathTriggerId);
						deadUnits.Add(unit);
					}
					else
					{
						unit.SetTrigger(healthModule.HitTriggerId);
					}
				}
			}
			
			_hitsIntentions.Clear();
		}
	}
}