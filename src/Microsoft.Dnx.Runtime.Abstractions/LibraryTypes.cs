using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Framework.Runtime
{
    public static class LibraryTypes
    {
        public static readonly string Package = nameof(Package);
        public static readonly string Project = nameof(Project);
        public static readonly string ReferenceAssembly = nameof(ReferenceAssembly);
        public static readonly string GlobalAssemblyCache = nameof(GlobalAssemblyCache);
        public static readonly string Unresolved = nameof(Unresolved);
    }
}
