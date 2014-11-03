using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using DotNetFiddle.Infrastructure;
using StackExchange.Profiling;

namespace DotNetFiddle.NancyFx
{
	public class NancyFxCodeHelper
	{
		private CodeHelper _languageCodeHelper;  //C# or Vb.NET. For ex. CSharpCodeHelper

		//private static string[] _assembliesForBuild;  //Was used in .NET Before

		static NancyFxCodeHelper()
		{
		
		}

		public NancyFxCodeHelper(CodeHelper languageCodeHelper)
		{
			_languageCodeHelper = languageCodeHelper;
		}

		public Language Language
		{
			get { return _languageCodeHelper.Language; }
		}

		public ProjectType ProjectType
		{
			get { return ProjectType.Mvc; }
		}



		//Good examples is here: http://www.jhovgaard.com/from-aspnet-mvc-to-nancy-part-2/
		private const string _viewCsSample = @"";

		private const string _modelCsSample = @"";

		private const string _controllerCsSample = @"public class SampleModule : Nancy.NancyModule
{
    public SampleModule()
    {
        Get[""/""] = _ => ""Hello World!"";
    }
}";
		private const string _modelVbSample = @"";

		private const string _viewVbSample = @"";

		private const string _controllerVbSample = @"";



		public CodeBlock GetSampleCodeBlock()
		{
			switch (Language)
			{
				case Language.CSharp:
					return new NancyFxCodeBlock
					{
						Module = _modelCsSample,
						View = _viewCsSample,
						Controller = _controllerCsSample
					};
				case Language.VbNet:
					return new NancyFxCodeBlock
					{
						Module = _modelVbSample,
						View = _viewVbSample,
						Controller = _controllerVbSample
					};
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	

		public string GetSecurityLevel1Attribute()
		{
			return _languageCodeHelper.GetSecurityLevel1Attribute();
		}



		public RunResult Run(NancyFxRunOpts opts)
		{
			throw new NotImplementedException();
		}

	

		public virtual NancyFxValidateResult Validate(NancyFxRunOpts opts)
		{
			var result = new NancyFxValidateResult
			{
				IsSuccess = true,
				ModuleErrors = new List<ValidationError>(),
				ModelErrors = new List<ValidationError>(),
				ViewErrors = new List<ValidationError>(),
			};


			throw new NotImplementedException();
		}



		public List<AutoCompleteItem> GetAutoCompleteItems(NancyFxCodeBlock codeBlock, NancyFxFileType nancyFxFileType, int? pos = null)
		{
			switch (nancyFxFileType)
			{
				case NancyFxFileType.Module:
					return _languageCodeHelper.GetAutoCompleteItems(codeBlock.Module, pos);
				case NancyFxFileType.View:
					return new List<AutoCompleteItem>();
				case NancyFxFileType.Controller:
					{
						var aggregateCode = codeBlock.Controller + codeBlock.Module;
						return _languageCodeHelper.GetAutoCompleteItems(aggregateCode, pos);
					}
				default:
					throw new ArgumentOutOfRangeException("nancyFxFileType");
			}
		}

	}
}
