using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetFiddle.Infrastructure;


namespace DotNetFiddle.NancyFx
{

	public class NancyFxPostBackOpts
	{
		public const string SessionDataId = "NANCY_FX_POSTBACK_OPTS";

		public Language Language { get; set; }
		public List<string> NuGetDllReferences { get; set; }
		public NancyFxCodeBlock CodeBlock { get; set; }
	}
}