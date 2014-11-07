using System.Collections.Generic;
using System.IO;
using DotNetFiddle.Infrastructure;

namespace DotNetFiddle.NancyFx.NancyUtils
{
    public static class NuGetDllfererencesHelper
    {
        private static string _packagesDirectory;
        private static IEnumerable<string> _nuGetDllfererences;

        /// <summary>
        /// NuGet package directory root path
        /// </summary>
        public static string PackagesDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_packagesDirectory))
                {
                    var currentPath = Path.GetDirectoryName(SandboxHelper.GetAssemblyLocation(typeof(NancyFxCodeHelper).Assembly));
                    _packagesDirectory = Path.GetFullPath(Path.Combine(currentPath, @"..\..\..\packages"));
                }

                return _packagesDirectory;
            }
        }

        /// <summary>
        /// List of default NuGet dlls which Nancy self hosting application could use
        /// </summary>
        public static IEnumerable<string> NancyFxNuGetDllfererences
        {
            get
            {
                if (_nuGetDllfererences == null)
                {
                    var dlls = new List<string>
                    {
                        Path.Combine(PackagesDirectory, @"Nancy.0.23.2\lib\net40\Nancy.dll"),
                        Path.Combine(PackagesDirectory, @"Nancy.Hosting.Self.0.23.2\lib\net40\Nancy.Hosting.Self.dll")
                    };

                    ValidateDllPath(dlls);

                    _nuGetDllfererences = dlls;
                }

                return _nuGetDllfererences;
            }
        }

        /// <summary>
        /// Make sure that required dll exist otherwise throw exception because application could not work stable without them.
        /// </summary>
        /// <param name="dlls"></param>
        private static void ValidateDllPath(IEnumerable<string> dlls)
        {
            foreach (var path in dlls)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(string.Format("Could not found ddl: {0}", path));
                }
            }
        }
    }
}
