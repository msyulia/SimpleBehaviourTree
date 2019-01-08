using System;
using System.Collections.Generic;
using NLog;
using NLog.Targets;
using NLog.Config;
using SimpleBT.Core;

namespace ExampleApplication
{
    class Program
    {
        class ExampleActionNode : BTNode
        {
            public ExampleActionNode(string Name) : base(Name) {   }

            public ExampleActionNode(string Name,
                                     List<BTNode> Children) : base(Name, Children) {   }

            public override NodeStatus Tick()
            {
                Console.WriteLine("Ticking node: " + Name);
                return NodeStatus.Success;
            }
        }

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var logConsole = new ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, "logconsole");
            LogManager.Configuration = config;

            BehaviourTree bt = new BehaviourTree();

            bt.Logger = LogManager.GetCurrentClassLogger();


            var selector = new BaseSelectorNode("Selector 1");
            selector.AddChildAt(0, new ExampleActionNode("Action no. 1"));
            selector.AddChildAt(1, new ExampleActionNode("Action no. 2"));
            bt.AddChild(bt.Root, selector, 0);

            var sequence = new BaseSequenceNode("Sequence 1");
            sequence.AddChildAt(0, new ExampleActionNode("Action no. 3"));
            sequence.AddChildAt(1, new ExampleActionNode("Action no. 4"));
            bt.AddChild(bt.Root, sequence, 1);
            bt.Execute();

            Console.ReadKey();
        }
    }
}
