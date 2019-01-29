using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SimpleBT.Core
{
	public class SimpleBehaviourTree : IBehaviourTree
	{
		private readonly ReaderWriterLockSlim @lock;

		public BTStatus TreeStatus { get; private set; }

		public BTNode RootNode { get; set; }

		public ILogger Logger { get; set; }

		public ITreeTraversal<HashSet<BTNode>> TreeTraversalStrategy { get; set; }

		public ICollection<string> TreeNodes { get; private set; }

		public SimpleBehaviourTree()
		{
			@lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

			TreeTraversalStrategy = new BFSTraversal<HashSet<BTNode>>();
			RootNode = new RootNode("Root Node");
			TreeNodes = new HashSet<string>
			{
				RootNode.Name
			};
		}

		public SimpleBehaviourTree(ITreeTraversal<HashSet<BTNode>> treeTraversalStrategy) : this()
		{
			TreeTraversalStrategy = treeTraversalStrategy;
		}

		public BTStatus Execute()
		{
			Logger.Info("Started Behaviour Tree");

			/*
			 * To properly start all nodes we need to explore the tree using Breadth First Search
			 */
			var traverser = new BFSTraversal<HashSet<BTNode>>();
			foreach (var node in traverser.Traverse(this))
			{
				Logger.Info($"Ticking {node}");
				node.Tick();
			}

			return TreeStatus;
		}

		public bool AddChildToParent(BTNode child, string parent, int index)
		{
			if (Exists(child.Name))
			{
				return false;
			}

			@lock.EnterWriteLock();
			try
			{
				TreeNodes.Add(child.Name);
				var parentNode = Find(parent);
				if (!(parentNode is null))
				{
					parentNode.AddChildAt(index, child);
					return true;
				}
				return false;
			}
			finally
			{
				@lock.ExitWriteLock();
			}
		}

		public BTNode RemoveChildFromParent(string child, string parent)
		{
			@lock.EnterWriteLock();
			try
			{
				if (Exists(child) && Exists(parent))
				{
					var parentNode = Find(parent);
					var deletedNode = parentNode.RemoveChild(child);
					TreeNodes.Remove(child);
					return deletedNode;
				}
				throw new BehaviourTreeException(
					$"{child} or {parent} not found in this behaviour tree");
			}
			finally
			{
				@lock.ExitWriteLock();
			}
		}

		public IEnumerable<BTNode> Traverse()
		{
			@lock.EnterReadLock();
			try
			{
				return TreeTraversalStrategy.Traverse(this);
			}
			finally
			{
				@lock.ExitReadLock();
			}
		}

		public bool Exists(string node)
		{
			@lock.EnterReadLock();
			try
			{
				return TreeNodes.Contains(node);
			}
			finally
			{
				@lock.ExitReadLock();
			}
		}

		public BTNode Find(string nodeName)
		{
			@lock.EnterReadLock();
			try
			{
				return TreeTraversalStrategy.Traverse(this).
					Where(treeNode => treeNode.Name == nodeName).
					Single();
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			finally
			{
				@lock.ExitReadLock();
			}
		}
	}
}
