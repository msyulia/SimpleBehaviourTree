using System.Collections.Generic;

namespace SimpleBT.Core
{
	public class DFSTraversal<T> : ITreeTraversal<T> 
		where T : ICollection<BTNode>, new()
	{
		public IEnumerable<BTNode> Traverse(IBehaviourTree tree)
		{
			T discoveredNodes = new T();

			void ExploreTree(BTNode node)
			{
				discoveredNodes.Add(node);
				foreach (var childNode in tree.RootNode)
				{
					if (!discoveredNodes.Contains(childNode))
					{
						ExploreTree(childNode);
					}
				}
			}
			ExploreTree(tree.RootNode);

			return discoveredNodes;
		}
	}
}
