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

		public BTNode RootNode { get; }

		public ILogger Logger { get; set; }

		public ICollection<string> TreeNodes { get; }

		public ITreeTraversal<HashSet<BTNode>> TreeTraversalStrategy { get; set; }

		public SimpleBehaviourTree()
		{
			@lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

			RootNode = new RootNode("Root Node");
			TreeNodes = new HashSet<string>
			{
				RootNode.Name
			};
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
			@lock.EnterWriteLock();
			try
			{
				if (Exists(child.Name))
				{
					throw new BehaviourTreeException($"A node with name {child} already exists in the tree! " +
						"Consider changing the node name");
				}
				Find(parent).AddChildAt(index, child);
				return true;
			}
			catch (Exception)
			{
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
					return Find(parent)
						.RemoveChild(child);
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
				return Traverse().
					Where(treeNode => treeNode.Name == nodeName).
					Single();
			}
			finally
			{
				@lock.ExitReadLock();
			}
		}
	}
}
