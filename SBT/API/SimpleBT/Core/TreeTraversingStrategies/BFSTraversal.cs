using System.Collections.Generic;

namespace SimpleBT.Core
{
	public class BFSTraversal<T> : ITreeTraversal<T>
		where T : ICollection<BTNode>, new()
	{
		public IEnumerable<BTNode> Traverse(IBehaviourTree tree)
		{
			T resultNodes = new T();
			var unexploredNodes = new Queue<BTNode>();
			var exploredNodes = new HashSet<BTNode>();

			void ExploreTree(BTNode node)
			{
				unexploredNodes.Enqueue(node);
				while (unexploredNodes.Count > 0)
				{
					var currentNode = unexploredNodes.Dequeue();
					resultNodes.Add(currentNode);
					foreach (var childNode in currentNode)
					{
						if (!exploredNodes.Contains(childNode))
						{
							unexploredNodes.Enqueue(childNode);
						}
					}
				}
			}
			ExploreTree(tree.RootNode);
			return resultNodes;
		}
	}
}
