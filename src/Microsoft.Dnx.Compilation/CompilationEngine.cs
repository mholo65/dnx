using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Dnx.Compilation.Caching;
using Microsoft.Dnx.Runtime;

namespace Microsoft.Dnx.Compilation
{
    public class CompilationEngine : ICompilationEngine
    {
        public ILibraryExporter LibraryExporter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICache Cache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public INamedCacheDependencyProvider NamedCacheDependencyProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        } 

        public event Action<string> OnInputFileChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Assembly CompileAndLoad(string name, string aspect)
        {
            throw new NotImplementedException();
        }
    }
}
