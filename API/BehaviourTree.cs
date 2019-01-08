using System;
using System.Threading;

using NLog;
using NLog.Config;

namespace SimpleBT.Core
{
    public enum BTStatus
    {
        Running = 0,
        Success,
        Failure,
    }

    public class BehaviourTree
    {
        public BTNode Root;

        private ILogger m_Logger;
        private ReaderWriterLockSlim m_BTLock;

        public ILogger Logger
        {
            get
            {
                return m_Logger;
            }
            set 
            {
                m_Logger = value;
            }
        }

        public BehaviourTree()
        {
            m_BTLock = new ReaderWriterLockSlim(
                            LockRecursionPolicy.NoRecursion);
            Root = new RootNode("Root Node");
        }

        public bool AddChild(BTNode parent, BTNode child, int index)
        {
            parent.AddChildAt(index, child);
            return true;   
        }

        public void Execute()
        {
            Console.WriteLine("Starting BT");
            Root.Tick();

            foreach (var child in Root)
            {
                Logger.Info("[{0}] Ticking child: {1}", DateTime.Now, child.ToString());
                child.Tick();
            }
        }
    }
}
