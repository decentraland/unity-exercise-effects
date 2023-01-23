using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Projectiles;
using Exercise.Battle.Scripts.Rules;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Units;
using Exercise.LaunchMenu.Scripts;
using Exercise.Utils;
using Exercise.Utils.Pool;
using UnityEngine;

namespace Exercise.Battle.Scripts
{
	public class BattleContainer : MonoBehaviour
	{
		[SerializeField] private MovementRestriction[] _movementRestrictions;
		[SerializeField] private Transform _poolRoot;
		[SerializeField] private GameOverMenu _gameOverMenu;

		public GameLoop.GameLoop Install(BattleConfiguration battleConfiguration)
		{
			_poolRoot.gameObject.SetActive(false);

			ServiceLocator.Instance = new ServiceLocator();

			var prefabPool = new PrefabPool(_poolRoot);
			var battle = InstantiateBattle(battleConfiguration);
			var timeProvider = new ScaledTimeProvider();
			var hitIntentions = new IntentionsRegistry<HitIntention>();

			var selectTargetSystem = new SelectTargetSystem();
			var arrowsSystem = new ArrowsSystem(prefabPool, timeProvider, hitIntentions);

			var arrowFactory = new ArrowFactory(arrowsSystem, prefabPool);
			ServiceLocator.Instance.Register(arrowFactory);

			var attackSystem = new AttackSystem(timeProvider, hitIntentions);
			var damageSystem = new DamageSystem(hitIntentions);
			var disposeDeadUnitsSystem = new DisposeDeadUnitsSystem();
			var movementSystem = new MovementSystem(_movementRestrictions, battle, timeProvider);
			var victoryConditionsSystem = new VictoryConditionsSystem(battle, _gameOverMenu);

			var gameLoop = new GameLoop.GameLoop(battle, selectTargetSystem, movementSystem, arrowsSystem, attackSystem, damageSystem,
				disposeDeadUnitsSystem, victoryConditionsSystem);

			return gameLoop;
		}

		private IBattle InstantiateBattle(BattleConfiguration battleConfiguration)
		{
			var unitFactory = new UnitFactory();

			var armyBuilder = new ArmyBuilder(unitFactory);
			var battleFactory = new BattleFactory(armyBuilder);

			return battleFactory.Create(battleConfiguration);
		}
	}
}