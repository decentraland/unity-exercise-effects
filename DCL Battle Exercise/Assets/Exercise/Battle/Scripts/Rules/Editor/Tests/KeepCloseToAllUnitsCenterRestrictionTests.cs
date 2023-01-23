using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Exercise.Battle.Scripts.Rules.Editor.Tests
{
	public class KeepCloseToAllUnitsCenterRestrictionTests
	{
		private KeepCloseToAllUnitsCenterRestriction _restriction;
		private IBattle _battle;
		private IUnit _unit;
		
		[SetUp]
		public void Setup()
		{
			_restriction = ScriptableObject.CreateInstance<KeepCloseToAllUnitsCenterRestriction>();
			_restriction.SetMaxDistance(10);
			
			_battle = Substitute.For<IBattle>();
			_battle.Position.Returns(Vector3.zero);
			
			_unit = Substitute.For<IUnit>();
		}
		
		[Test]
		public void RestrictsPosition()
		{
			var direction = new Vector3(0.5f, 0, 0.5f).normalized;
			var worldPosition = _restriction.RestrictPosition(_battle, _unit, new MovementIntention(direction), new Vector3(10, 0, 10));

			var expected = direction * 10f;

			for (var i = 0; i < 3; i++)
			{
				Assert.AreEqual(expected[i], worldPosition[i], Mathf.Epsilon);
			}
		}

		[Test]
		public void DoesNotRestrictPosition()
		{
			var direction = new Vector3(0.5f, 0, 0.5f).normalized;
			var worldPosition = _restriction.RestrictPosition(_battle, _unit, new MovementIntention(direction), new Vector3(5, 0, 5));

			var expected = new Vector3(5f, 0, 5f);

			for (var i = 0; i < 3; i++)
			{
				Assert.AreEqual(expected[i], worldPosition[i], Mathf.Epsilon);
			}
		}
	}
}