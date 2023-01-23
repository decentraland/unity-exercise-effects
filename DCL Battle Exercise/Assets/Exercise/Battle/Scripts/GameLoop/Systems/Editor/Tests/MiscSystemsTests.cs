using System;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using Exercise.LaunchMenu.Scripts;
using NSubstitute;
using NUnit.Framework;

namespace Exercise.Battle.Scripts.GameLoop.Systems.Editor.Tests
{
	[TestFixture]
	public class MiscSystemsTests
	{
		[Test]
		public void VictoryConditionsCanBeMet()
		{
			var battle = Substitute.For<IBattle>();
			var gameOverMenu = Substitute.For<IGameOverMenu>();

			var army1 = Substitute.For<IArmy>();
			var army2 = Substitute.For<IArmy>();

			army1.Units.Returns(Array.Empty<IUnit>());
			army2.Units.Returns(new[] { Substitute.For<IUnit>(), Substitute.For<IUnit>() });

			battle.Armies.Returns(new[] { army1, army2 });

			var system = new VictoryConditionsSystem(battle, gameOverMenu);
			var isFinished = system.IsVictoryConditionMet();

			Assert.IsTrue(isFinished);

			gameOverMenu.Received().Show(1);
		}
	}
}