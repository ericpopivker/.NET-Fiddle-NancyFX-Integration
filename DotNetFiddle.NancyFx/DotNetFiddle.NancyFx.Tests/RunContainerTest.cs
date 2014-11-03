using DotNetFiddle.CSharpConsole;
using DotNetFiddle.Infrastructure;
using NUnit.Framework;

namespace DotNetFiddle.NancyFx.Tests
{
	[TestFixture]
	public class RunContainerTest
	{

		/// <summary>
		/// Execute code block for CSharp Console code helper
		/// </summary>
		private RunResult ExecuteNancyFxCode(NancyFxCodeBlock codeBlock)
		{
			//var opts = new ConsoleOrScriptRunOpts()
			//{
			//	CodeBlock = new ConsoleOrScriptCodeBlock { CodeBlock = codeBlock },
			//	Language = Language.CSharp,
			//	ProjectType = ProjectType.Console
			//};

			//return ContainerUtils.ExecuteCode(opts, typeof(CSharpConsoleCodeHelper));
			return null;
		}

//		private const string HelloWorldCode = @"
//using System;
//					
//public class Program
//{
//    public void Main()
//    {
//		Console.WriteLine(""Hello World"");
//    }
//}";

//		[Test]
//		public void ExecuteCode_WhenCSharpConsole_ThenWorks()
//		{
//			var result = ExecuteCSharpConsole(HelloWorldCode);

//			Assert.IsTrue(result.IsSuccess);
//			Assert.AreEqual("Hello World", result.ConsoleOutput);
//		}

	}
}