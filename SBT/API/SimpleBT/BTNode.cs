using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SimpleBT
{
	public enum NodeStatus
	{
		Running = 0,
		Success,
		Failure,
	}

	public abstract class BTNode : IEquatable<BTNode>,
								   IEnumerable<BTNode>
	{
		protected string m_Name;
		protected List<BTNode> m_Children;

		public virtual string Name
		{
			get => m_Name;
			private set => m_Name = value;
		}

		public virtual List<BTNode> Children
		{
			get => m_Children;
			private set => Children = value;
		}

		protected BTNode(string Name)
		{
			this.Name = Name;
			this.m_Children = new List<BTNode>();
		}

		public virtual void AddChild(BTNode node) => m_Children.Add(node);
		public virtual bool RemoveChild(BTNode node) => m_Children.Remove(node);
		public virtual BTNode RemoveChild(string nodeName) => 
			m_Children.Where(treeNode => treeNode.Name == nodeName)
				.Single();
		public virtual void AddChildAt(int index, BTNode node) => m_Children.Insert(index, node);
		public virtual void RemoveChildAt(int index) => m_Children.RemoveAt(index);

		public bool Equals(BTNode other) => Name.Equals(other.Name);
		public IEnumerator<BTNode> GetEnumerator() => m_Children.GetEnumerator();

		public abstract NodeStatus Tick();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
