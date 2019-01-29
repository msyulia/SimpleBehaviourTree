using NUnit.Framework;
using SimpleBT.Core;
using System.Linq;
using System.Collections.Generic;

namespace SimpleBT.Tests
{
	[TestFixture]
	public class TreeTraversal
	{
		[Test]
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
		[Test]
		public void BFSTraversalHeightOne()
		{
			var bt = new SimpleBehaviourTree();

			var child1 = new SuccessActionNode("Success 1");
			var child2 = new FailActionNode("Fail 1");
			var child3 = new RunningActionNode("Running 1");
			
			var expected = new List<string>
			{
				bt.RootNode.Name,
				"Success 1",
				"Fail 1",
				"Running 1"
			};

			Assert.IsTrue(bt.AddChildToParent(child1, bt.RootNode.Name, 0) &&
				bt.AddChildToParent(child2, bt.RootNode.Name, 1) &&
				bt.AddChildToParent(child3, bt.RootNode.Name, 2));
			var traversalResult = bt.Traverse();
			var result = traversalResult.Select(node => node.Name).ToList();
			Assert.IsTrue(expected.SequenceEqual(result));
		}
		[Test]
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
