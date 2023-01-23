using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;
using Exercise.LaunchMenu.Scripts;
using TMPro;
using UnityEngine;

public interface IArmyView
{
    void UpdateWithModel(UnitsRegistry unitsRegistry, IArmyNewModel model, int index);
}

public class ArmyView : MonoBehaviour, IArmyView
{
    [SerializeField] private Transform slidersRoot;
    [SerializeField] private UnitSliderView sliderPrefab;
    [SerializeField] private TMP_Text armyName;
    [SerializeField] private TMP_Dropdown strategyDropdown;

    private EnumDropdownWrapper<ArmyStrategy> enumDropdown;
    private IArmyPresenter presenter = null;

    private void Awake()
    {
        enumDropdown = new EnumDropdownWrapper<ArmyStrategy>(strategyDropdown);
        enumDropdown.OnValueChanged += OnStrategyChanged;
    }

    public void BindPresenter(IArmyPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void UpdateWithModel(UnitsRegistry unitsRegistry, IArmyNewModel model, int index)
    {
        armyName.SetText($"Army {index + 1}");
        enumDropdown.SetValueWithoutNotify(model.Strategy);

        foreach (var unit in unitsRegistry.Units)
        {
            var slider = Instantiate(sliderPrefab, slidersRoot);
            var count = model.GetCount(unit);

            slider.UpdateWithModel(unit, count, value =>
            {
                presenter?.UpdateUnits(unit, (int) value);
            });
        }
    }

    private void OnStrategyChanged(ArmyStrategy strategy)
    {
        presenter?.UpdateStrategy(strategy);
    }

    private void OnDestroy()
    {
        enumDropdown.OnValueChanged -= OnStrategyChanged;
        enumDropdown?.Dispose();
    }
}