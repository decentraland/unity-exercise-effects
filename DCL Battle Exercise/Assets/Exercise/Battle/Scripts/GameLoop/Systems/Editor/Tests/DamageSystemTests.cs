using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using Exercise.Battle.Scripts.Units.Settings;
using NSubstitute;
using NUnit.Framework;

namespace Exercise.Battle.Scripts.GameLoop.Systems.Editor.Tests
{
	[TestFixture]
	public class DamageSystemTests
	{
		private FakeHealthModule _healthModule;
		private IntentionsRegistry<HitIntention> _hitIntentions;
		private DamageSystem _damageSystem;
		private IUnit _unit;

		private class FakeHealthModule : HealthModule
		{
			public override void Initialize(IUnitStrategyEntry strategyEntry)
			{
				CurrentHealth = 100;
			}
		}
		
		[SetUp]
		public void PrepareDamageSystem()
		{
			_healthModule = new FakeHealthModule { CurrentHealth = 100 };

			_unit = Substitute.For<IUnit>();
			_hitIntentions = new IntentionsRegistry<HitIntention>();

			var outModule = Arg.Any<HealthModule>();
			_unit.TryGetModule(out outModule)
				.Returns(x =>
				{
					x[0] = _healthModule;
					return true;
				});

			_damageSystem = new DamageSystem(_hitIntentions);
		}

		[Test]
		public void DamageSystemKillsUnits()
		{
			var hit = new HitIntention(new AttackSettings { Attack = 125 });
			_hitIntentions.Add(_unit, hit);

			var list = new List<IUnit>();

			_damageSystem.Update(list);
			Assert.LessOrEqual(_healthModule.CurrentHealth, 0);
			
			CollectionAssert.Contains(list, _unit);
		}

		[Test]
		public void DamageSystemTakesDefenseIntoAccount()
		{
			var defenseModule = Arg.Any<DefenseModule>();
			_unit.TryGetModule(out defenseModule)
				.Returns(x =>
				{ 
					x[0] = new DefenseModule
					{
						Settings = new DefenseSettings { Defense = 25 }
					};
					return true;
				});
			
			_hitIntentions.Add(_unit, new HitIntention(new AttackSettings { Attack = 50 }));
			
			_damageSystem.Update(new List<IUnit>());
			Assert.AreEqual(75, _healthModule.CurrentHealth);
		}

		[Test]
		public void DamageSystemReducesHealth()
		{
			_hitIntentions.Add(_unit, new HitIntention(new AttackSettings { Attack = 50 }));
			_damageSystem.Update(new List<IUnit>());
			Assert.AreEqual(50, _healthModule.CurrentHealth);
		}
	}
}