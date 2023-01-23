using Exercise.Battle.Scripts.Strategies;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public abstract class UnitModuleBase<TSettings> : MonoBehaviour
	{
		[field: SerializeField]
		public TSettings Settings { get; set; }

		public virtual void Initialize(IUnitStrategyEntry strategyEntry)
		{
		}
	}
}