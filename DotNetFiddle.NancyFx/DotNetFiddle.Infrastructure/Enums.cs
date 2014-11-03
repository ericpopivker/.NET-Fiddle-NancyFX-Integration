using System;
using System.ComponentModel;

namespace DotNetFiddle.Infrastructure
{
	[Serializable]
	public enum Language
	{
		[Description("C#")] 
		CSharp = 1,
		[Description("VB.NET")] 
		VbNet,
		[Description("F#")]
		FSharp
	}

	[Serializable]
	public enum ProjectType
	{
		[Description("Console")]
		Console = 1,
		[Description("Script")]
		Script,
		[Description("MVC")]
		Mvc,
		[Description("NancyFX")]
		NancyFx
	}

	[Serializable]
	public enum LimitType
	{
		ExecutionTime,
		MemoryUsage,
		CodeSize,
		CodeOutput,
		DirSize
	}

	[Serializable]
	public enum NancyFxFileType
	{
		[Description("Module")]
		Module,
		[Description("View")]
		View,
		[Description("Controller")]
		Controller
	}
}
