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
			var behaviourTree = new SimpleBehaviourTreeAsync
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
			var behaviourTree = new SimpleBehaviourTreeAsync
			{
				TreeTraversalStrategy = new DFSTraversal<HashSet<BTNode>>()
			};
			var nodes = behaviourTree.Traverse();
			Assert.IsTrue(nodes != null);
			Assert.IsTrue(nodes.ToList().Count == 1);
			Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
		}
		
	}
	[TestFixture]
	internal class SimpleBehaviourTreeAsync_TestsAsync
	{
		[TestCase]
		public void DFSTraverseWithNoChildrenAsync()
		{
			var behaviourTree = new SimpleBehaviourTreeAsync
			{
				TreeTraversalStrategy = new DFSTraversal<HashSet<BTNode>>()
			};
			Mutex mutex = new Mutex();
			bool noThreadExceptions = false;

			void ThreadFunc()
			{
				try
				{
					var nodes = behaviourTree.Traverse();
					Assert.IsTrue(nodes != null);
					Assert.IsTrue(nodes.ToList().Count == 1);
					Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
				}
				catch
				{
					using (mutex)
					{
						noThreadExceptions = false;
					}
				}
				using (mutex)
				{
					noThreadExceptions = true;
				}
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
		[TestCase]
		public void BFSTraverseWithNoChildrenAsync()
		{
			var behaviourTree = new SimpleBehaviourTreeAsync
			{
				TreeTraversalStrategy = new BFSTraversal<HashSet<BTNode>>()
			};
			Mutex mutex = new Mutex();
			bool noThreadExceptions = false;

			void ThreadFunc()
			{
				try
				{
					var nodes = behaviourTree.Traverse();
					Assert.IsTrue(nodes != null);
					Assert.IsTrue(nodes.ToList().Count == 1);
					Assert.IsTrue(nodes.ToList().Single().Name == "Root Node");
				}
				catch
				{
					using (mutex)
					{
						noThreadExceptions = false;
					}
				}
				using (mutex)
				{
					noThreadExceptions = true;
				}
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
