using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Exercise.LaunchMenu.Scripts
{
	public class GameOverMenu : MonoBehaviour, IGameOverMenu
	{
		public TextMeshProUGUI armyWins;
		public Button goToMenu;

		private void Awake()
		{
			goToMenu.onClick.AddListener(GoToMenu);
		}

		public void Show(int? victoriousArmy)
		{
			gameObject.SetActive(true);
			armyWins.text = $"Army {victoriousArmy} wins";
		}

		private void GoToMenu()
		{
			SceneManager.LoadScene(0);
		}
	}
}