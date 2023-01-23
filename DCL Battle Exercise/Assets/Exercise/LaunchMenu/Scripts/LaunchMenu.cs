using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchMenu : MonoBehaviour
{
	[SerializeField] private Button startButton;
	[SerializeField] private List<ArmyNewModelSO> armyModels;
	[SerializeField] private ArmyView prefab;
	[SerializeField] private Transform armiesRoot;
	[SerializeField] private UnitsRegistry unitsRegistry;

	private ArmyPresenter army1Presenter;
	private ArmyPresenter army2Presenter;

	private void Start()
	{
		startButton.onClick.AddListener(OnStart);

		for (var index = 0; index < armyModels.Count; index++)
		{
			var armyModel = armyModels[index];
			var view = Instantiate(prefab, armiesRoot);
			var presenter = new ArmyPresenter(unitsRegistry, armyModel, view, index);
			view.BindPresenter(presenter);
		}
	}

	private void OnStart()
	{
		SceneManager.LoadScene(1);
	}
}