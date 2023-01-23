using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;

public interface IArmyPresenter
{
	void UpdateUnits(UnitObject unitObject, int count);
	void UpdateStrategy(ArmyStrategy strategy);
}

public class ArmyPresenter : IArmyPresenter
{
	private readonly IArmyNewModel model;
	private readonly IArmyView view;

	public ArmyPresenter(UnitsRegistry unitsRegistry, IArmyNewModel model, IArmyView view, int index)
	{
		this.model = model;
		this.view = view;
		this.view.UpdateWithModel(unitsRegistry, model, index);
	}

	public void UpdateUnits(UnitObject unitObject, int count)
	{
		model.SetUnits(unitObject, count);
	}

	public void UpdateStrategy(ArmyStrategy strategy)
	{
		model.Strategy = strategy;
	}
}