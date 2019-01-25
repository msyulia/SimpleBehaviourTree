using NUnit.Framework;
using SimpleBT.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
		public void BFSTraverseWithNoChildren()
		{
			var behaviourTree = new SimpleBehaviourTree
			{
				TreeTraversalStrategy = new BFSTraversal<HashSet<BTNode>>()
			};
			var nodes = behaviourTree.Traverse();
			Assert.IsTrue(nodes != null);
			Assert.IsTrue(nodes.ToList().Count == 1);
			Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
		}
		[TestCase]
		public void DFSTraverseWithNoChildren()
		{
			var behaviourTree = new SimpleBehaviourTree
			{
				TreeTraversalStrategy = new DFSTraversal<HashSet<BTNode>>()
			};
			var nodes = behaviourTree.Traverse();
			Assert.IsTrue(nodes != null);
			Assert.IsTrue(nodes.ToList().Count == 1);
			Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
		}
		
	}
}
