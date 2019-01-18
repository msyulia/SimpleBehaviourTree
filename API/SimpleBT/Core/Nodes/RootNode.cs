using System.Collections.Generic;

namespace SimpleBT.Core
{
	public class RootNode : BTNode
	{
		public RootNode(string Name) : base(Name) { }

		public RootNode(string Name,
						List<BTNode> Children) : base(Name, Children) { }

		public override NodeStatus Tick()
		{
			return NodeStatus.Success;
		}
	}
}
