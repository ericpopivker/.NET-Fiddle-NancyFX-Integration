using DotNetFiddle.CSharpConsole;
using NUnit.Framework;

namespace DotNetFiddle.NancyFx.Tests
{
	[TestFixture]
	public class NancyFxCodeHelperTest
	{
		private NancyFxCodeHelper _codeHelper;

		[SetUp]
		public void SetUp()
		{
			_codeHelper = new NancyFxCodeHelper(new CSharpConsoleCodeHelper());
		} 

		[Test]
		public void Run_Works()
		{
			string moduleCode = @"public class SampleModule : Nancy.NancyModule
{
    public SampleModule()
    {
        Get[""/""] = _ => ""Hello World!"";
    }
}";

			var codeBlock = new NancyFxCodeBlock
			{
				Module = moduleCode
			};

			var result = _codeHelper.Run(new NancyFxRunOpts{CodeBlock = codeBlock});

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("Hello World!", result.WebPageHtmlOutput);
		}


	}
}
