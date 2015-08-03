using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Dnx.Runtime;

namespace Microsoft.Dnx.Compilation
{
    internal static class ProjectExtensions
    {
        public static CompilationProjectContext ToCompilationContext(this Project self, CompilationTarget target)
        {
            Debug.Assert(string.Equals(target.Name, self.Name, StringComparison.Ordinal), "The provided target should be for the current project!");
            return new CompilationProjectContext(
                target,
                self.ProjectDirectory,
                self.ProjectFilePath,
                self.Version.GetNormalizedVersionString(),
                self.AssemblyFileVersion,
                self.EmbedInteropTypes,
                new CompilationFiles(
                    self.Files.PreprocessSourceFiles,
                    self.Files.SourceFiles),
                self.GetCompilerOptions(target.TargetFramework, target.Configuration));
        }
    }
}
