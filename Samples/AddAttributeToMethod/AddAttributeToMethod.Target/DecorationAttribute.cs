using System;

namespace AddAttributeToMethod.Target
{
	public class DecorationAttribute : Attribute
	{
		public string Message { get; private set; }

		public DecorationAttribute(string message)
		{
			Message = message;
		}
	}
}