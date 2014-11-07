using System;
using System.Collections.Generic;
using DotNetFiddle.Infrastructure;
using DotNetFiddle.NancyFx.NancyUtils;

namespace DotNetFiddle.NancyFx
{
	[Serializable]
	public class NancyFxRunOpts : RunOptsBase
	{
		public NancyFxRunOpts()
		{
			CodeBlock = new NancyFxCodeBlock();
			HttpMethod = "GET";
		    NuGetDllReferences = new List<string>(NuGetDllfererencesHelper.NancyFxNuGetDllfererences);
		    HostIndex = NancySelfHostingHelper.Instance.GetNextHostIndex();
		}

		public string HttpMethod { get; set; }

		public string PostBody { get; set; }

		public string ContentType { get; set; }

		public string Controller { get; set; }

		public string Action { get; set; }

		public string QueryString { get; set; }

        /// <summary>
        /// This parameter used for generation base Url during Nancy Self Hosting process.
        /// </summary>
        public int HostIndex { get; set; }

		public NancyFxPostBackOpts GetPostOpts()
		{
			return new NancyFxPostBackOpts
			{
				Language = Language,
				NuGetDllReferences = new List<string>(NuGetDllReferences),
				CodeBlock = new NancyFxCodeBlock((NancyFxCodeBlock)CodeBlock)
			};
		}
	}

}
