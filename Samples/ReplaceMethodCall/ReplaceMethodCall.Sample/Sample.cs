using System.Linq;
using Cecil.Samples.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReplaceMethodCall.Sample
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
			var runMethod = targetType.Methods.Single(m => m.Name == "Run");

			// Get a ILProcessor for the Run method
			var il = runMethod.Body.GetILProcessor();

			// Retrieve the instruction calling the Unauthorized method
			var callUnauthorized = runMethod
				.Body
				.Instructions
				.Single(i =>
					i.OpCode == OpCodes.Call
					&& ((MethodReference) i.Operand).Name == "Unauthorized");


			// Retrieve the Authorized method
			var authorized = targetType.Methods.Single(m => m.Name == "Authorized");

			// Create a new instruction to call the Authorized method
			var callAuthorized = il.Create(OpCodes.Call, authorized);

			// Replace the call to Unauthorized by the call to Authorized
			il.Replace(callUnauthorized, callAuthorized);

			// Write the module with default parameters
			_module.Write(_targetFileName);
		}
	}
}
