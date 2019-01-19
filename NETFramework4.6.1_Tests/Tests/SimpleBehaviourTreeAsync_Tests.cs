using System.Linq;
using System.Threading;
using System.Collections.Generic;

using NUnit.Framework;

using SimpleBT.Core;

namespace SimpleBT.Tests
{
	internal class SuccessActionNode : BTNode
	{
		public SuccessActionNode(string Name) : base(Name) { }

		public SuccessActionNode(string Name,
							List<BTNode> Children) : base(Name, Children) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	internal class RunningActionNode : BTNode
	{
		public RunningActionNode(string Name) : base(Name) { }

		public RunningActionNode(string Name,
							List<BTNode> Children) : base(Name, Children) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	internal class FailActionNode : BTNode
	{
		public FailActionNode(string Name) : base(Name) { }

		public FailActionNode(string Name,
							List<BTNode> Children) : base(Name, Children) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	[TestFixture]
	internal class SimpleBehaviourTreeAsync_Tests
	{
		[TestCase]
		public void TraverseWithNoChildren()
		{
			var behaviourTree = new SimpleBehaviourTreeAsync();
			var nodes = behaviourTree.Traverse();
			Assert.IsTrue(nodes != null);
			Assert.IsTrue(nodes.ToList().Count == 1);
			Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
		}
		[TestCase]
		public void TraverseWithNoChildrenAsync()
		{

			var behaviourTree = new SimpleBehaviourTreeAsync();
			var nodes = behaviourTree.Traverse();
			Assert.IsTrue(nodes != null);
			Assert.IsTrue(nodes.ToList().Count == 1);
			Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
		}
	}
}
