//using DotNetFiddle.Infrastructure;
//using NUnit.Framework;

//namespace DotNetFiddle.NancyFx.Tests
//{
//    [TestFixture]
//    public class RunContainerTest
//    {

//        /// <summary>
//        /// Execute code block for CSharp Console code helper
//        /// </summary>
//        private RunResult ExecuteNancyFxCode(NancyFxCodeBlock codeBlock)
//        {
//            var opts = new NancyFxRunOpts {CodeBlock = codeBlock};

//            return ContainerUtils.ExecuteCode(opts, typeof(NancyFxCodeHelper));
//            return null;
//        }

//        private const string HelloWorldCode = @"
//using System;
//					
//public class Program
//{
//    public void Main()
//    {
//		Console.WriteLine(""Hello World"");
//    }
//}";

//        [Test]
//        public void ExecuteCode_WhenCSharpConsole_ThenWorks()
//        {

//            string moduleCode = @"public class SampleModule : Nancy.NancyModule
//{
//    public SampleModule()
//    {
//        Get[""/""] = _ => ""Hello World!"";
//    }
//}";

//            var codeBlock = new NancyFxCodeBlock
//            {
//                Module = moduleCode
//            };

//            var result = ExecuteNancyFxCode(codeBlock);

//            Assert.IsTrue(result.IsSuccess);
//            Assert.AreEqual("Hello World", result.ConsoleOutput);
//        }

//    }
//}