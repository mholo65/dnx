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
        private static Dictionary<string, Func<LibraryExporter, RuntimeLibrary, LibraryExport>> _exporters = new Dictionary<string, Func<LibraryExporter, RuntimeLibrary, LibraryExport>>()
        {
            { LibraryTypes.GlobalAssemblyCache, (e, l) => e.ExportAssemblyLibrary(l) },
            { LibraryTypes.ReferenceAssembly, (e, l) => e.ExportAssemblyLibrary(l) },
            { LibraryTypes.Project, (e, l) => e.ExportProject(l) },
            { LibraryTypes.Package, (e, l) => e.ExportPackage(l) },
        };

        private readonly ILibraryManager _libraryManager;
        private readonly FrameworkName _targetFramework;
        private readonly string _configuration;
        private readonly ICache _cache;

        public LibraryExporter(FrameworkName targetFramework,
                               string configuration,
                               ILibraryManager libraryManager,
                               ICache cache)
        {
            _targetFramework = targetFramework;
            _configuration = configuration;
            _libraryManager = libraryManager;
            _cache = cache;
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

        private LibraryExport ExportLibrary(RuntimeLibrary library)
        {
            Func<LibraryExporter, RuntimeLibrary, LibraryExport> exporter;
            if(!_exporters.TryGetValue(library.Type, out exporter))
            {
                return LibraryExport.Empty;
            }
            return exporter(this, library);
        }

        private LibraryExport ExportPackage(RuntimeLibrary library)
        {
            throw new NotImplementedException();
        }

        private LibraryExport ExportProject(RuntimeLibrary library)
        {
            throw new NotImplementedException();
        }

        private LibraryExport ExportAssemblyLibrary(RuntimeLibrary library)
        {
            // Assume the path to the library is the path to the DLL
            return new LibraryExport(new MetadataFileReference(library.Identity.Name, library.Path));
        }
    }
}
