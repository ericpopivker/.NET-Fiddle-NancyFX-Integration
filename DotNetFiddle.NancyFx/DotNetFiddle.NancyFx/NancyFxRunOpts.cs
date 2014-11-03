using System;
using System.Collections.Generic;
using DotNetFiddle.Infrastructure;

namespace DotNetFiddle.NancyFx
{
	[Serializable]
	public class NancyFxRunOpts : RunOptsBase
	{
		public NancyFxRunOpts()
		{
			CodeBlock = new NancyFxCodeBlock();
			HttpMethod = "GET";
		}

		public string HttpMethod { get; set; }

		public string PostBody { get; set; }

		public string ContentType { get; set; }

		public string Controller { get; set; }

		public string Action { get; set; }

		public string QueryString { get; set; }

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
