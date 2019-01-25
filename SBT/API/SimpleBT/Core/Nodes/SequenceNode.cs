using System.Collections.Generic;

namespace SimpleBT.Core
{
	public class SequenceNode : BTNode
	{
		public SequenceNode(string Name) : base(Name) { }

		public SequenceNode(string Name,
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
				if (nodeStatus == NodeStatus.Failure)
				{
					return NodeStatus.Failure;
				}
			}
			return NodeStatus.Success;
		}
	}
}
