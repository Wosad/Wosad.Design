using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using DependencySharp;
using Wosad.Common.Section.General.Gpc.Properties;
using System.IO;

namespace Wosad.Common.Section.General.Gpc
{
    public static class Initializer
    {
        public static void Initialize()
        {
            // The full path where you expect the dependency to exist on disk
            // Helper methods are included to provide easy access to the directory
            // where your assmebly is being executed from (which is where
            // you typically need to place unmanaged dependencies).
            var expectedPath = Path.Combine(AssemblyUtilities.ExecutingAssemblyPath, "gpc.dll");

            // The dependency as a byte array, stored in your library's Properties/Resources.resx file
            var dependency = Resources.gpc;

            var dependencies = new List<UnmanagedDependency>()
            {
                new UnmanagedDependency(expectedPath, dependency)
            };

            var dependencyManager = new DependencyManager();
            dependencyManager.VerifyDependenciesAndExtractIfMissing(dependencies);
        }
    }
}
