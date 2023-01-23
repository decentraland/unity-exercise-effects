using System.Linq;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Battle;
using Exercise.LaunchMenu.Scripts;
using Exercise.Utils.Pool;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
    /// <summary>
    ///     Checks if either army is dead, stops the game loop and shows the ending screen
    /// </summary>
    public class VictoryConditionsSystem
    {
	    private readonly IBattle _battle;
	    private readonly IGameOverMenu _gameOverMenu;

	    public VictoryConditionsSystem(IBattle battle, IGameOverMenu gameOverMenu)
	    {
		    _battle = battle;
		    _gameOverMenu = gameOverMenu;
	    }

		public bool IsVictoryConditionMet()
		{
			var aliveArmies = ListPool<IArmy>.Rent();
			
			aliveArmies.AddRange(_battle.Armies.Where(army => army.Units.Count > 0));
			var result = false;

			if (aliveArmies.Count <= 1)
			{
				var index = aliveArmies.FirstOrDefault()?.Index;
				if (index != null) index++;
				
				_gameOverMenu.Show(index);
				result = true;
			}
			
			ListPool<IArmy>.Return(aliveArmies);
			return result;
		}
	}
}