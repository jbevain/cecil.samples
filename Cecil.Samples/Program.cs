using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Cecil.Samples.Framework;
using Mono.Cecil;
using NUnit.Framework;
using TypeAttributes = Mono.Cecil.TypeAttributes;

#region Cheat
using SamplesAttribute = NUnit.Framework.TestFixtureAttribute;
using SampleAttribute = NUnit.Framework.TestAttribute;
#endregion

namespace Cecil.Samples
{
	[Samples]
	public class SamplesDirectory
	{
		[Sample]
		public void AddAttributeToMethod()
		{
			RunSample("AddAttributeToMethod");
		}

		[Sample]
		public void ReplaceMethodCall()
		{
			RunSample("ReplaceMethodCall");
		}

		private void RunSample(string sampleName)
		{
			var dataPath = Path.GetDirectoryName(GetType().Assembly.Location);
		
			Assert.IsNotNull(dataPath);

			var sampleAssemblyPath = Path.Combine(dataPath, sampleName + ".Sample.dll");

			if (!File.Exists(sampleAssemblyPath))
				Assert.Fail("Could not find sample named " + sampleName);

			var targetAssemblyPath = Path.Combine(dataPath, sampleName + ".Target.dll");

			if (!File.Exists(targetAssemblyPath))
				Assert.Fail("Could not find target named " + sampleName);

			var targetModule = ModuleDefinition.ReadModule(targetAssemblyPath);
			if (!IsMarked(targetModule))
			{
				var sampleAssembly = Assembly.LoadFrom(sampleAssemblyPath);
				var sampleType = sampleAssembly.GetTypes().FirstOrDefault(t => typeof(ISample).IsAssignableFrom(t));

				Assert.IsNotNull(sampleType, "Could not find a sample type to run");

				var sample = (ISample)Activator.CreateInstance(sampleType, targetAssemblyPath);

				sample.TargetModule.Types.Add(
					new TypeDefinition("", "IMarked", TypeAttributes.Interface | TypeAttributes.Abstract));

				sample.Run();				
			}

			var targetAssembly = Assembly.LoadFrom(targetAssemblyPath);
			var targetType = targetAssembly.GetTypes().FirstOrDefault(t => typeof(ITarget).IsAssignableFrom(t));

			Assert.IsNotNull(targetType, "Could not find a target type to run");

			var target = (ITarget) Activator.CreateInstance(targetType);

			target.Run();
		}

		private static bool IsMarked(ModuleDefinition targetModule)
		{
			return targetModule.Types.Any(t => t.Name == "IMarked");
		}
	}
}
