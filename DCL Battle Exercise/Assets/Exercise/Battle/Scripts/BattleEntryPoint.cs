using Exercise.Battle.Scripts.Battle;
using UnityEngine;

namespace Exercise.Battle.Scripts
{
	public class BattleEntryPoint : MonoBehaviour
	{
		[SerializeField] private BattleConfiguration _battleConfiguration;
		[SerializeField] private BattleContainer _battleContainer;

		private GameLoop.GameLoop _gameLoop;

		public void Start()
		{
			_gameLoop = _battleContainer.Install(_battleConfiguration);
		}

		private void Update()
		{
			if (_gameLoop == null)
				return;

			if (_gameLoop.Update())
			{
				_gameLoop = null;
			}
		}
	}
}