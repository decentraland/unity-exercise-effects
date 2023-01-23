using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Units.Settings;

namespace Exercise.Battle.Scripts.Strategies.Attack
{
	public readonly struct HitIntention : IIntention
	{
		public readonly AttackSettings Source;

		public HitIntention(AttackSettings attackSettings)
		{
			Source = attackSettings;
		}
	}
}