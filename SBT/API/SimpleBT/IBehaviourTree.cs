using System.Collections.Generic;

namespace SimpleBT
{
	/// <summary>
	/// Enumeration containing multiple states in which a Behaviour Tree can be in.
	/// </summary>
	public enum BTStatus
	{
		Running = 0,
		Success,
		Failure,
	}

	/// <summary>
	/// An interface for implementing a Behaviour Tree.
	/// I assume that every node has a unique name.
	/// </summary>
	public interface IBehaviourTree
	{
		BTNode RootNode { get; set; }
		ICollection<string> TreeNodes { get; }

		/// <summary>
		/// Access the current status of the behaviour tree.
		/// </summary>
		BTStatus TreeStatus { get; }

		/// <summary>
		/// Adds a child node to the specified parent node at a position.
		/// </summary>
		/// <param name="child">A reference to the node you want to add.</param>
		/// <param name="parent">Name of the parent node.</param>
		/// <param name="index">Index at which to add the new node.</param>
		/// <returns>Returns true if the child was succesfully added from the parent, false if parent was not found in the tree</returns>
		bool AddChildToParent(BTNode child, string parent, int index);

		/// <summary>
		/// Removes a node from the parent.
		/// </summary>
		/// <param name="child">A reference to the node you want to add.</param>
		/// <param name="parent">Name of the parent node.</param>
		/// <param name="index">Index at which to add the new node.</param>
		/// <returns>Returns the removed node if it was found</returns>
		BTNode RemoveChildFromParent(string child, string parent);

		/// <summary>
		/// Method for traversing the tree. 
		/// </summary>
		/// <returns>A enumeration of every node met during traversing</returns>
		IEnumerable<BTNode> Traverse();

		/// <summary>
		/// Checks if a node with a given name exists in the tree.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		bool Exists(string node);

		/// <summary>
		/// Finds a Node given his name and returns it.
		/// </summary>
		/// <param name="node"></param>
		/// <returns>Returns the node if found.</returns>
		BTNode Find(string node);

		/// <summary>
		/// Checks the current Behaviour tree status
		/// </summary>
		/// <returns>A BTStatus enumeration, indicating the current state of the Behaviour Tree</returns>
		BTStatus Execute();
	}
}
