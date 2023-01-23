using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using Exercise.Battle.Scripts.Units.Settings;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Exercise.Battle.Scripts.GameLoop.Systems.Editor.Tests
{
	[TestFixture]
	public class AttackSystemTests
	{
		[SetUp]
		public void PrepareSystem()
		{
			_attackModule = new AttackModule
				{ Settings = new AttackSettings { Attack = 10, AttackCooldown = 5, AttackRange = 10, PostAttackDelay = 1 } };

			var source = Substitute.For<IUnit>();
			var target = Substitute.For<IUnit>();

			_hitIntentions = new IntentionsRegistry<HitIntention>();

			var strategy = new FakeAttackStrategy(target);

			_timeProvider = Substitute.For<ITimeProvider>();
			_timeProvider.GetTime().Returns(10f);

			var strategiesEntry = Substitute.For<IUnitStrategyEntry>();
			strategiesEntry.AttackStrategy.Returns(strategy);

			_attackModule.Initialize(strategiesEntry);

			var outAttackModule = Arg.Any<AttackModule>();
			source.TryGetModule(out outAttackModule)
				.Returns(x =>
				{
					x[0] = _attackModule;
					return true;
				});

			var targetModule = new TargetSelectionModule();
			targetModule.SetTargetIntention(new TargetIntention(target, Vector3.zero));

			var outTargetModule = Arg.Any<TargetSelectionModule>();
			source.TryGetModule(out outTargetModule)
				.Returns(x =>
				{
					x[0] = targetModule;
					return true;
				});

			_units = new List<IUnit> { source, target };

			_attackSystem = new AttackSystem(_timeProvider, _hitIntentions);
		}

		private AttackModule _attackModule;

		private class FakeAttackStrategy : UnitAttackStrategyBase
		{
			private readonly IUnit _target;

			public FakeAttackStrategy(IUnit target)
			{
				_target = target;
			}

			protected override bool ExecuteInternal(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
				IMutableIntentionsRegistry<HitIntention> hitIntentions)
			{
				AddHitIntention(hitIntentions, _target, attackModule.Settings);
				return true;
			}
		}

		private class EmptyAttackStrategy : UnitAttackStrategyBase
		{
			protected override bool ExecuteInternal(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
				IMutableIntentionsRegistry<HitIntention> hitIntentions)
			{
				return false;
			}
		}

		private ITimeProvider _timeProvider;
		private AttackSystem _attackSystem;
		private List<IUnit> _units;
		private IntentionsRegistry<HitIntention> _hitIntentions;

		[Test]
		public void SetsAttackPerformed()
		{
			_attackSystem.Update(_units);
			Assert.AreEqual(_timeProvider.GetTime(), _attackModule.AttackPerformedTime);
		}

		[Test]
		public void AddsHitIntentionToTarget()
		{
			_attackSystem.Update(_units);

			var expectedHitIntention = new HitIntention(new AttackSettings
				{ Attack = 10, AttackCooldown = 5, AttackRange = 10, PostAttackDelay = 1 });

			var target = _units[1];
			CollectionAssert.Contains(_hitIntentions.GetIntentions()[target], expectedHitIntention);
		}

		[Test]
		public void SetsOnCooldown()
		{
			_attackSystem.Update(_units);

			Assert.IsTrue(_attackModule.OnCooldown);
			Assert.IsTrue(_attackModule.OnPostAttackDelay);
		}

		[Test]
		public void ResetsCooldown()
		{
			_attackSystem.Update(_units);
			_timeProvider.GetTime().Returns(20f);

			var strategiesEntry = Substitute.For<IUnitStrategyEntry>();
			strategiesEntry.AttackStrategy.Returns(new EmptyAttackStrategy());
			_attackModule.Initialize(strategiesEntry);
			
			_attackSystem.Update(_units);

			Assert.IsFalse(_attackModule.OnCooldown);
			Assert.IsFalse(_attackModule.OnPostAttackDelay);
		}
	}
}