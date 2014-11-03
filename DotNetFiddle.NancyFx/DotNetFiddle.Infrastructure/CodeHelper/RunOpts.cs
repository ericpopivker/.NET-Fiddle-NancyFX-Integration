using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace DotNetFiddle.Infrastructure
{
	[DataContract, KnownType(typeof(ConsoleOrScriptRunOpts))]
	[Serializable]
	public class RunOptsBase
	{
		public RunOptsBase()
		{
			NuGetDllReferences = new List<string>();
			ConsoleInputLines = new List<string>();
		}

		[DataMember]
		public Language Language { get; set; }

		[DataMember]
		public ProjectType ProjectType { get; set; }

		[DataMember]
		public CodeBlock CodeBlock { get; set; }

		[DataMember]
		public List<string> NuGetDllReferences { get; set; }

		[DataMember]
		public List<string> ConsoleInputLines { get; set; }
	}

	[DataContract]
	[Serializable]
	public class ConsoleOrScriptRunOpts : RunOptsBase
	{
		public ConsoleOrScriptRunOpts()
		{
			CodeBlock = new ConsoleOrScriptCodeBlock();			
		}
	}
	
	

}