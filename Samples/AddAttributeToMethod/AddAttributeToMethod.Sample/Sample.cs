using System.Linq;
using Cecil.Samples.Framework;
using Mono.Cecil;

namespace AddAttributeToMethod.Sample
{
	public class Sample : ISample
	{
		private readonly string _targetFileName;
		private readonly ModuleDefinition _module;

		public ModuleDefinition TargetModule { get { return _module; } }

		public Sample(string targetFileName)
		{
			_targetFileName = targetFileName;

			// Read the module with default parameters
			_module = ModuleDefinition.ReadModule(_targetFileName);
		}

		public void Run()
		{
			// Retrieve the target method onto which we want to add the attribute
			var targetType = _module.Types.Single(t => t.Name == "Target");
			var targetMethod = targetType.Methods.Single(m => m.Name == "TargetMethod");

			// Retrieve the type of the attribute
			var decorationAttributeType = _module.Types.Single(t => t.Name == "DecorationAttribute");

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
					type: _module.TypeSystem.String,
					value: "WOW"));

			// Add the custom attribute to the method
			targetMethod.CustomAttributes.Add(decorationAttribute);

			// Write the module with default parameters
			_module.Write(_targetFileName);
		}
	}
}
