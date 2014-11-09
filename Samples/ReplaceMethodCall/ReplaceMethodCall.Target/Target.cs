using Cecil.Samples.Framework;
using NUnit.Framework;

namespace ReplaceMethodCall.Target
{
	public class Target : ITarget
	{
		public void Run()
		{
			// This sample replaces this method call by a call to Authorized
			Unauthorized();
		}

		public void Unauthorized()
		{
			Assert.Fail("This should not be called");
		}

		public void Authorized()
		{
			Assert.Pass("Authorized");
		}
	}
}
