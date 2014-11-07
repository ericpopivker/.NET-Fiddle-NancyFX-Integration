using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFiddle.NancyFx.NancyUtils
{
    public sealed class NancySelfHostingHelper
    {
        public static readonly NancySelfHostingHelper Instance = new NancySelfHostingHelper();

        private NancySelfHostingHelper()
        {
        }

        //In order not to complicate code, we will use simple Id for different sessions.
        private int _hostIndex = 0;

        //Note. In real application we will read this value from config.
        private readonly string DEFAULT_URL_PATTERN = @"http://localhost:8889/nancy{0}/";
        private readonly string SELF_HOSTING = @"
namespace NancyHostApplication
{{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Hosting.Self;
    using Nancy.TinyIoc;
    using Nancy.ViewEngines;

    public class Program
    {{
        public static void Main()
        {{
            using (var nancyHost = new NancyHost(new Uri({0}),  new CustomBootstrapper()))
            {{
                nancyHost.Start();

                Console.ReadLine();
            }}
        }}
    }}

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {{
        protected override NancyInternalConfiguration InternalConfiguration
        {{
            get
            {{
                return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);
            }}
        }}

        void OnConfigurationBuilder(NancyInternalConfiguration x)
        {{
            x.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }}

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {{
            base.ConfigureApplicationContainer(container);
            ResourceViewLocationProvider.RootNamespaces.Add(
              Assembly.GetAssembly(typeof(CustomBootstrapper)), ""NancyHostApplication"");
        }}
    }}
}}";

        /// <summary>
        ///  based on the hostIndex.
        /// </summary>
        public string GenerateHostBaseUrl(int hostIndex)
        {
            return string.Format(DEFAULT_URL_PATTERN, hostIndex);
        }

        /// <summary>
        /// Generate code for Nancy host application based on the hostIndex.
        /// </summary>
        public string GenerateNancySelfHostingCode(int hostIndex)
        {
            var hostBaseUrl = "\"" + GenerateHostBaseUrl(hostIndex) + "\"";
            return string.Format(SELF_HOSTING, hostBaseUrl);
        }

        public int GetNextHostIndex()
        {
            return _hostIndex++;
        }
    }
}
