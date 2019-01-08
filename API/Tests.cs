using System.Collections.Generic;
using NUnit.Framework;
using SimpleBT.Exceptions;

namespace SimpleBT.Core
{
    class SuccessAction : BTNode
    {
        public SuccessAction(string Name) : base(Name) { }
        public SuccessAction(string Name, List<BTNode> Children) : base(Name, Children) { }
        
        public override NodeStatus Tick() => NodeStatus.Success;
        
    }

    class FailureAction : BTNode
    {
        public FailureAction(string Name) : base(Name) { }
        public FailureAction(string Name, List<BTNode> Children) : base(Name, Children) { }
        
        public override NodeStatus Tick() => NodeStatus.Failure;
    }

    class RunningAction : BTNode
    {
        public RunningAction(string Name) : base(Name) { }
        
        public RunningAction(string Name, List<BTNode> Children) : base(Name, Children) { }
        
        public override NodeStatus Tick() => NodeStatus.Running;
    }

    [TestFixture]
    public class BaseSequenceNodeTests
    {
        [Test]
        public void NoChildren()
        {
            var sequenceNode = new BaseSequenceNode("ExampleSequence");
            Assert.Throws(typeof(NodeWithoutChildrenException),
                            () => sequenceNode.Tick());
        }
    }

    [TestFixture]
    public class BaseSelectorNodeTests
    {
        [Test]
        public void NoChildren()
        {
            var selectorNode = new BaseSelectorNode("ExampleSelector");
            Assert.Throws(typeof(NodeWithoutChildrenException),
                            () => selectorNode.Tick());
        }
    }
}
