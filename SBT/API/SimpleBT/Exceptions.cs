using System;
namespace SimpleBT
{
	public class BehaviourTreeException : Exception
	{
		public BehaviourTreeException()
		{
		}

		public BehaviourTreeException(string message)
			: base(message)
		{
		}

		public BehaviourTreeException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
