using System.Net;
using DotNetFiddle.Infrastructure;
using DotNetFiddle.NancyFx.NancyUtils;
using NUnit.Framework;

namespace DotNetFiddle.NancyFx.Tests
{
	[TestFixture]
	public class NancyFxCodeHelperTest
	{
		private NancyFxCodeHelper _codeHelper;

        private const string _sampleModule = @"
public class SampleModule : Nancy.NancyModule
{
    public SampleModule()
    {
        Get[""/""] = _ => ""Hello World!"";
    }
}";

		[SetUp]
		public void SetUp()
		{
			_codeHelper = new NancyFxCodeHelper();
		} 

		[Test]
		public void Run_Works()
		{
            var codeBlock = new NancyFxCodeBlock
            {
                Module = _sampleModule
            };

		    var opts = new NancyFxRunOpts {CodeBlock = codeBlock};

		    string htmlCode;
		    RunResult result;
            using (var container = new ContainerUtils.DomainContainer())
            {
                result = container.Execute(opts, typeof(NancyFxCodeHelper));
                Assert.IsTrue(result.IsSuccess);

                using (WebClient client = new WebClient())
                {
                    string url = NancySelfHostingHelper.Instance.GenerateHostBaseUrl(opts.HostIndex);
                    htmlCode = client.DownloadString(url);
                }
            }

            Assert.AreEqual("Hello World!", htmlCode);
		}
	}
}
