using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Exercise.Battle.Scripts.Rules.Editor.Tests
{
	public class KeepDistanceBetweenUnitsRestrictionTests
	{
		private KeepDistanceBetweenUnitsRestriction _restriction;
		private IBattle _battle;
		private IArmy _army;
		private List<IUnit> _units;
		
		[SetUp]
		public void Setup()
		{
			_restriction = ScriptableObject.CreateInstance<KeepDistanceBetweenUnitsRestriction>();
			
			_battle = Substitute.For<IBattle>();
			_army = Substitute.For<IArmy>();

			var unit1 = Substitute.For<IUnit>();
			unit1.Position.Returns(Vector3.zero);

			var unit2 = Substitute.For<IUnit>();
			unit2.Position.Returns(new Vector3(-1, 0, 0));

			var unit3 = Substitute.For<IUnit>();
			unit3.Position.Returns(new Vector3(0, 0, 1));

			_units = new List<IUnit>
			{
				unit1,
				unit2,
				unit3
			};

			_army.Units.Returns(_units);

			_battle.Armies.Returns(new List<IArmy>
			{
				_army
			});
		}
		
		[Test]
		public void RestrictsPosition()
		{
			_restriction.SetDistance(2);

			var worldPosition =
				_restriction.RestrictPosition(_battle, _units[0], new MovementIntention(Vector3.zero), _units[0].Position);

			var expected = new Vector3(Mathf.Sqrt(2), 0, 1 - Mathf.Sqrt(2));
			
			Assert.AreEqual(expected, worldPosition);
			
			worldPosition =
				_restriction.RestrictPosition(_battle, _units[2], new MovementIntention(Vector3.zero), _units[2].Position);

			expected = new Vector3(0, 0, 2);
			
			Assert.AreEqual(expected, worldPosition);
		}

		[Test]
		public void DoesNotRestrictPosition()
		{
			_restriction.SetDistance(0.5f);
			
			var worldPosition =
				_restriction.RestrictPosition(_battle, _units[0], new MovementIntention(Vector3.zero), _units[0].Position);

			var expected = Vector3.zero;
			
			Assert.AreEqual(expected, worldPosition);
		}
	}
}