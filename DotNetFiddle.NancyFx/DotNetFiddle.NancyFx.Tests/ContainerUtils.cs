using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetFiddle.Infrastructure;
using DotNetFiddle.Infrastructure.Worker;
using DotNetFiddle.RunContainer;

namespace DotNetFiddle.NancyFx.Tests
{
    public static class ContainerUtils
    {
        private static Dictionary<AssemblyName, string> GetMappings()
        {
            var result = new Dictionary<AssemblyName, string>();

            Action<Assembly> addAssembly = (a) => result.Add(a.GetName(), a.ManifestModule.FullyQualifiedName);

            addAssembly(typeof(ContainerUtils).Assembly);
            addAssembly(typeof(NancyFxCodeHelper).Assembly);
            addAssembly(typeof(SandboxHelper).Assembly);

            return result;
        }

        public static RunResult ExecuteCode(RunOptsBase opts, Type codeHelperType)
        {
            AppDomain sandboxDomain = SandboxHelper.CreateSandboxDomain(true, GetMappings());

            try
            {
                return ExecuteCode(opts, codeHelperType, sandboxDomain);

            }
            finally
            {
                SandboxHelper.UnloadDomain(sandboxDomain);
            }
        }

        private static RunResult ExecuteCode(RunOptsBase opts, Type codeHelperType, AppDomain sandboxDomain)
        {
            Container container = new Container();

            container.InitWorkerSettings(
                WorkerConfiguration.Current.ID,
                WorkerConfiguration.Current.SandboxFolder,
                codeHelperType);

            var runResult = container.Run(opts);

            return runResult;
        }


        public class DomainContainer : IDisposable
        {
            AppDomain sandboxDomain = SandboxHelper.CreateSandboxDomain(true, GetMappings());

            public RunResult Execute(RunOptsBase opts, Type codeHelperType)
            {
                return ExecuteCode(opts, codeHelperType, sandboxDomain);
            }

            // Flag: Has Dispose already been called?
            bool disposed = false;

            // Public implementation of Dispose pattern callable by consumers.
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            // Protected implementation of Dispose pattern.
            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    // Free any other managed objects here.
                    SandboxHelper.UnloadDomain(sandboxDomain);
                }

                // Free any unmanaged objects here.

                disposed = true;
            }
        }
    }
}