using System;
using System.Collections.Generic;
using System.Collections;

namespace SimpleBT.Core
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
            get => Children;
            private set => Children = value;
        }

        protected BTNode(string Name)
        {
            this.Name = Name;
            this.m_Children = new List<BTNode>();
        }

        protected BTNode(string Name, 
                         List<BTNode> Children) : this(Name)
        {
            m_Children = Children;
        }

        public virtual void AddChild(BTNode node) => m_Children.Add(node);
        public virtual bool RemoveChild(BTNode node) => m_Children.Remove(node);
        public virtual void AddChildAt(int index, BTNode node) => m_Children.Insert(index, node);
        public virtual void RemoveChildAt(int index) => m_Children.RemoveAt(index);

        public bool Equals(BTNode other) => Name.Equals(other.Name);
        public IEnumerator<BTNode> GetEnumerator() => m_Children.GetEnumerator();

        public abstract NodeStatus Tick();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class RootNode : BTNode
    {
        public RootNode(string Name) : base(Name)   {   }

        public RootNode(string Name, 
                        List<BTNode> Children) : base(Name, Children)   {   }

        public override NodeStatus Tick()
        {
            return NodeStatus.Success;
        }
    }

    public sealed class BaseSequenceNode : BTNode
    {
        public BaseSequenceNode(string Name) : base(Name)   {   }

        public BaseSequenceNode(string Name, 
                                List<BTNode> Children) : base(Name, Children)   {    }

        public override NodeStatus Tick()
        {
            foreach (var child in m_Children)
            {
                var nodeStatus = child.Tick();
                if (nodeStatus == NodeStatus.Running)
                {
                    return NodeStatus.Running;
                }
                if(nodeStatus == NodeStatus.Success)
                {
                    return NodeStatus.Success;
                }
            }
            return NodeStatus.Failure;
        }
    }

    public sealed class BaseSelectorNode : BTNode
    {
        public BaseSelectorNode(string Name) : base(Name)   {   }

        public BaseSelectorNode(string Name, 
                            List<BTNode> Children) : base(Name, Children)  {   }

        public override NodeStatus Tick()
        {
            foreach (var child in m_Children)
            {
                var nodeStatus = child.Tick();
                if (nodeStatus == NodeStatus.Running)
                {
                    return NodeStatus.Running;
                }
                if (nodeStatus == NodeStatus.Success)
                {
                    return NodeStatus.Success;
                }
            }
            return NodeStatus.Failure;
        }
    }
}
