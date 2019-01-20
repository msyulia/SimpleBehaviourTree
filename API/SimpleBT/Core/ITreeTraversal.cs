using System.Collections.Generic;

namespace SimpleBT.Core
{
	public interface ITreeTraversal<T> 
		where T : IEnumerable<BTNode>, ICollection<BTNode>, new()
	{
		IEnumerable<BTNode> Traverse(IBehaviourTree tree);
	}
}
