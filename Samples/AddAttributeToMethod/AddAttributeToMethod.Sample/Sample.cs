using System.Linq;
using Cecil.Samples.Framework;
using Mono.Cecil;

namespace AddAttributeToMethod.Sample
{
	public class Sample : ISample
	{
		private readonly string _targetFileName;

		public Sample(string targetFileName)
		{
			_targetFileName = targetFileName;
		}

		public void Run()
		{
			// Read the module with default parameters
			var module = ModuleDefinition.ReadModule(_targetFileName);

			// Retrieve the target method onto which we want to add the attribute
			var targetType = module.Types.Single(t => t.Name == "Target");
			var targetMethod = targetType.Methods.Single(m => m.Name == "TargetMethod");

			// Retrieve the type of the attribute
			var decorationAttributeType = module.Types.Single(t => t.Name == "DecorationAttribute");

			// Create the equivalent of [Decoration("WOW")]
			// All custom attributes are created from a constructor
			var decorationAttributeConstructor = decorationAttributeType
				.Methods
				.Single(m => m.IsConstructor
					&& m.Parameters.Count == 1
					&& m.Parameters[0].ParameterType.MetadataType == MetadataType.String);

			var decorationAttribute = new CustomAttribute(decorationAttributeConstructor);

			decorationAttribute.ConstructorArguments.Add(
				new CustomAttributeArgument(
					type: module.TypeSystem.String,
					value: "WOW"));

			// Add the custom attribute to the method
			targetMethod.CustomAttributes.Add(decorationAttribute);

			// Mark the module for the samples runner
			module.Types.Add(new TypeDefinition("", "IMarked", TypeAttributes.Interface | TypeAttributes.Abstract));

			// Write the module with default parameters
			module.Write(_targetFileName);
		}
	}
}
