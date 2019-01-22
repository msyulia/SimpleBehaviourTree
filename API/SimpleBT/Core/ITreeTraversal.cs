using System.Collections.Generic;

namespace SimpleBT.Core
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITreeTraversal<T> 
		where T : IEnumerable<BTNode>, ICollection<BTNode>, new()
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tree"></param>
		/// <returns></returns>
		IEnumerable<BTNode> Traverse(IBehaviourTree tree);
	}
}
