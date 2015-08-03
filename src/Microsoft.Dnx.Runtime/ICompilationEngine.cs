using System;
using System.Reflection;
using Microsoft.Dnx.Compilation;
using Microsoft.Dnx.Compilation.Caching;

namespace Microsoft.Dnx.Runtime
{
    public interface ICompilationEngine : IDisposable
    {
        event Action<string> OnInputFileChanged;

        Assembly CompileAndLoad(string name, string aspect);
    }
}
