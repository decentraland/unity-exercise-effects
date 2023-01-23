using Exercise.Battle.Scripts.Strategies;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public interface IUnitModule
	{
		void Initialize(IUnitStrategyEntry strategyEntry);
	}
}