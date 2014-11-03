using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DotNetFiddle.Infrastructure
{
	[DataContract, KnownType(typeof(ConsoleOrScriptCodeBlock))]
	[Serializable]
	public abstract class CodeBlock
	{
		public abstract CodeBlock Clone();
	}

	[DataContract]
	[Serializable]
	public class ConsoleOrScriptCodeBlock : CodeBlock
	{
		public ConsoleOrScriptCodeBlock()
		{

		}

		public ConsoleOrScriptCodeBlock(ConsoleOrScriptCodeBlock copy)
		{
			CodeBlock = copy.CodeBlock;
		}

		public override CodeBlock Clone()
		{
			return new ConsoleOrScriptCodeBlock(this);
		}

		[DataMember]
		public string CodeBlock { get; set; }
	}
}
