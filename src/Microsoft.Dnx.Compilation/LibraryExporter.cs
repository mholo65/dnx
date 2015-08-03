using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Microsoft.Dnx.Compilation;
using Microsoft.Dnx.Compilation.Caching;
using Microsoft.Framework.Runtime;

namespace Microsoft.Dnx.Runtime.Compilation
{
    public class LibraryExporter : ILibraryExporter
    {
        // Taking LibraryExporter as an arg allows this list to be static but the methods to be instance :)
        private static Dictionary<string, Func<LibraryExporter, RuntimeLibrary, CompilationTarget, LibraryExport>> _exporters = new Dictionary<string, Func<LibraryExporter, RuntimeLibrary, CompilationTarget, LibraryExport>>()
        {
            { LibraryTypes.GlobalAssemblyCache, (e, l) => e.ExportAssemblyLibrary(l) },
            { LibraryTypes.ReferenceAssembly, (e, l) => e.ExportReferenceAssembly(l) },
            { LibraryTypes.Project, (e, l) => e.ExportProject(l) },
            { LibraryTypes.Package, (e, l) => e.ExportPackage(l) },
        };

        private readonly IFrameworkReferenceResolver _frameworkReferenceResolver;
        private readonly ILibraryManager _libraryManager;
        private readonly FrameworkName _targetFramework;
        private readonly string _configuration;
        private readonly ICache _cache;

        public LibraryExporter(FrameworkName targetFramework,
                               string configuration,
                               ILibraryManager libraryManager,
                               ICache cache,
                               IFrameworkReferenceResolver frameworkReferenceResolver)
        {
            _targetFramework = targetFramework;
            _configuration = configuration;
            _libraryManager = libraryManager;
            _cache = cache;
            _frameworkReferenceResolver = frameworkReferenceResolver;
        }

        public LibraryExport GetLibraryExport(string name)
        {
            return GetLibraryExport(name, aspect: null);
        }

        public LibraryExport GetAllExports(string name)
        {
            return GetAllExports(name, aspect: null);
        }

        public LibraryExport GetLibraryExport(string name, string aspect)
        {
            throw new NotImplementedException();
        }

        public LibraryExport GetAllExports(string name, string aspect)
        {
            throw new NotImplementedException();
        }

        public LibraryExport GetAllExports(string name, bool includeProjects)
        {
            throw new NotImplementedException();
        }

        private LibraryExport ExportLibrary(RuntimeLibrary library, CompilationTarget target)
        {
            Func<LibraryExporter, RuntimeLibrary, CompilationTarget, LibraryExport> exporter;
            if(!_exporters.TryGetValue(library.Type, out exporter))
            {
                return LibraryExport.Empty;
            }
            return exporter(this, library, target);
        }

        private LibraryExport ExportPackage(RuntimeLibrary library, CompilationTarget target)
        {
            throw new NotImplementedException();
        }

        private LibraryExport ExportProject(RuntimeLibrary library, CompilationTarget target)
        {
            throw new NotImplementedException();
        }

        private LibraryExport ExportAssemblyLibrary(RuntimeLibrary library, CompilationTarget target)
        {
            // Just use the path as the path to the managed assembly
            return new LibraryExport(new MetadataFileReference(library.Identity.Name, library.Path));
        }

        private LibraryExport ExportReferenceAssembly(RuntimeLibrary library, CompilationTarget target)
        {
            // We can't use resolved paths since it might be different to the target framework
            // being passed in here. After we know this resolver is handling the
            // requested name, we can call back into the FrameworkResolver to figure out
            //  the specific path for the target framework

            string path;
            Version version;

            var asmName = LibraryRange.GetAssemblyName(target.Name);
            if (_frameworkReferenceResolver.TryGetAssembly(asmName, target.TargetFramework, out path, out version))
            {
                return new LibraryExport(new MetadataFileReference(asmName, path));
            }
            return null;
        }

    }
}
