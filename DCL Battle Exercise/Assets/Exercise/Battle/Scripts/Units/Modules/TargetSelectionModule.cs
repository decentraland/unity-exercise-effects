using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public class TargetSelectionModule : MonoBehaviour, IUnitModule
	{
		public UnitTargetSelectionStrategyBase Strategy { get; private set; }
		
		public TargetIntention? TargetIntention { get; private set; }

		public void SetTargetIntention(TargetIntention? targetIntention)
		{
			TargetIntention = targetIntention;
		}
		
		public void Initialize(IUnitStrategyEntry strategyEntry)
		{
			Strategy = strategyEntry.TargetSelectionStrategy;
		}
	}
}