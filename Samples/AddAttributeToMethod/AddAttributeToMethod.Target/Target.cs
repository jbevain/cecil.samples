using Cecil.Samples.Framework;
using NUnit.Framework;

namespace AddAttributeToMethod.Target
{
	public class Target : ITarget
	{
		// This samples injects a [Decoration("WOW")] attribute here
		public void TargetMethod()
		{
		}

		public void Run()
		{
			var method = typeof (Target).GetMethod("TargetMethod");

			Assert.IsNotNull(method);

			var attributes = method.GetCustomAttributes(typeof (DecorationAttribute), inherit: false);

			Assert.AreEqual(1, attributes.Length, "There should be a [Decoration] on TargetMethod");

			var attribute = (DecorationAttribute) attributes[0];

			Assert.AreEqual("WOW", attribute.Message);
		}
	}
}
