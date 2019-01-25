using System.Collections.Generic;

namespace SimpleBT.Core
{
	public class SelectorNode : BTNode
	{
		public SelectorNode(string Name) : base(Name) { }

		public SelectorNode(string Name,
							List<BTNode> Children) : base(Name, Children) { }

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
