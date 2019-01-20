using NUnit.Framework;
using SimpleBT.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
			bool noThreadExceptions = false;

			void ThreadFunc()
			{
				var nodes = behaviourTree.Traverse();
				Assert.IsTrue(nodes != null);
				Assert.IsTrue(nodes.ToList().Count == 1);
				Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
				noThreadExceptions = true;
			}
			var threads = new Thread[3]
			{
				new Thread(() => ThreadFunc()),
				new Thread(() => ThreadFunc()),
				new Thread(() => ThreadFunc())
			};

			foreach (var thread in threads)
			{
				thread.Start();
			}
			foreach (var thread in threads)
			{
				thread.Join();
			}

			if (noThreadExceptions)
			{
				throw new AssertionException("Failed to execute test");
			}
		}
	}
}
