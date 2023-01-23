using System.Collections.Generic;
using Exercise.Battle.Scripts.GameLoop.Systems;
using NSubstitute;
using NUnit.Framework;

namespace Exercise.Battle.Scripts.Units.Editor.Tests
{
	[TestFixture]
	public class IntentionsTests
	{
		[SetUp]
		public void Setup()
		{
			_intentions = new IntentionsRegistry<TestIntention>();
			_unit = Substitute.For<IUnit>();
		}

		private IntentionsRegistry<TestIntention> _intentions;
		private IUnit _unit;

		private struct TestIntention : IIntention
		{
			public int Value;
		}

		[Test]
		public void AddsIntention()
		{
			var intention = new TestIntention { Value = 1 };

			_intentions.Add(_unit, intention);
			Assert.AreEqual(intention, _intentions.GetIntentions()[_unit][0]);
			intention.Value = 2;

			_intentions.Add(_unit, intention);

			var intentions = _intentions.GetIntentions();
			Assert.AreEqual(1, intentions.Count);
			CollectionAssert.AreEquivalent(new List<TestIntention> { new TestIntention { Value = 1 }, new TestIntention { Value = 2 } },
				intentions[_unit]);
		}

		[Test]
		public void ClearsIntentions()
		{
			_intentions.Add(_unit, new TestIntention {Value = 1});
			_intentions.Add(_unit, new TestIntention {Value = 2});
			_intentions.Add(_unit, new TestIntention {Value = 3});
			
			_intentions.Clear();
			
			Assert.AreEqual(0, _intentions.GetIntentions().Count);
		}
	}
}