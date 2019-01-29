using NUnit.Framework;
using SimpleBT.Core;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBT.Tests
{
	internal class SuccessActionNode : BTNode
	{
		public SuccessActionNode(string Name) : base(Name) { }

		public SuccessActionNode(string Name,
							List<BTNode> Children) : base(Name) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	internal class RunningActionNode : BTNode
	{
		public RunningActionNode(string Name) : base(Name) { }

		public RunningActionNode(string Name,
							List<BTNode> Children) : base(Name) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	internal class FailActionNode : BTNode
	{
		public FailActionNode(string Name) : base(Name) { }

		public FailActionNode(string Name,
							List<BTNode> Children) : base(Name) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}
	internal class NodeWithChildren : BTNode
	{
		public NodeWithChildren(string Name) : base(Name) { }

		public NodeWithChildren(string Name,
							List<BTNode> Children) : base(Name) { }
		public override NodeStatus Tick()
		{
			throw new System.NotImplementedException();
		}
	}

	[TestFixture]
	public class SimpleBehaviourTree_Tests
	{
		[TestCase("Root Node", 0, ExpectedResult = true)]
		[TestCase("Not Existing Parent", 0, ExpectedResult = false)]
		public bool AddChildToParent(string parent, int index)
		{
			var success = new SuccessActionNode("Success");
			var bt = new SimpleBehaviourTree();

			return bt.AddChildToParent(success, parent, index);
		}

		[TestCase("Success 1")]
		[TestCase("Fail 1")]
		[TestCase("Running 1")]
		public void RemoveChildFromParent(string node)
		{
			var bt = new SimpleBehaviourTree();
			var parentNode = new NodeWithChildren("Node with children");
			var child1 = new SuccessActionNode("Success 1");
			var child2 = new FailActionNode("Fail 1");
			var child3 = new RunningActionNode("Running 1");
			bt.AddChildToParent(parentNode, "Root Node", 0);
			bt.AddChildToParent(child1, parentNode.Name, 0);
			bt.AddChildToParent(child2, parentNode.Name, 1);
			bt.AddChildToParent(child3, parentNode.Name, 2);

			var deletedNode = bt.RemoveChildFromParent(node, parentNode.Name);
			Assert.IsNotNull(deletedNode);
		}

		//[TestCase("Root Node", ExpectedResult = true)]
		//public bool Find(string node)
		//{
		//	var bt = new SimpleBehaviourTree();
		//	var parentNode = new NodeWithChildren("Node with children");
		//	var child1 = new SuccessActionNode("Success 1");
		//	var child2 = new FailActionNode("Fail 1");
		//	var child3 = new RunningActionNode("Running 1");
		//	bt.AddChildToParent(parentNode, "Root Node", 0);
		//	bt.AddChildToParent(child1, parentNode.Name, 0);
		//	bt.AddChildToParent(child2, parentNode.Name, 1);
		//	bt.AddChildToParent(child3, parentNode.Name, 2);


		//}

		[TestCase("Existing Node", ExpectedResult = true)]
		[TestCase("Not existing Node", ExpectedResult = false)]
		public bool Exists(string nodeName)
		{
			var bt = new SimpleBehaviourTree();
			bt.AddChildToParent(new SuccessActionNode("Existing Node"), "Root Node", 0);
			return bt.Exists(nodeName);
		}

		[Test]
		public void TreeNodes()
		{
			var behaviourTree = new SimpleBehaviourTree();
			behaviourTree.AddChildToParent(new SuccessActionNode("Success 1"), behaviourTree.RootNode.Name, 0);
			behaviourTree.AddChildToParent(new FailActionNode("Fail 1"), "Root Node", 1);
			behaviourTree.AddChildToParent(new RunningActionNode("Running 1"), "Root Node", 2);

			var result = behaviourTree.TreeNodes;
			var expected = new List<string>
			{
				behaviourTree.RootNode.Name,
				"Success 1",
				"Fail 1",
				"Running 1"
			};
			Assert.IsTrue(result.SequenceEqual(expected));
		}
	}
}
