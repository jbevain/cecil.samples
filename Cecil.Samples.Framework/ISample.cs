using Mono.Cecil;

namespace Cecil.Samples.Framework
{
    public interface ISample
    {
	    ModuleDefinition TargetModule { get; }

	    void Run();
    }
}
