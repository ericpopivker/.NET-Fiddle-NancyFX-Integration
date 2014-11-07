using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DotNetFiddle.CSharpConsole;
using DotNetFiddle.Infrastructure;
using DotNetFiddle.NancyFx.NancyUtils;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;

namespace DotNetFiddle.NancyFx
{

	public class NancyFxCodeHelper : CSharpCodeHelper
	{
	    public NancyFxCodeHelper()
	    {
            NuGetDllReferences.AddRange(NuGetDllfererencesHelper.NancyFxNuGetDllfererences);
	    }

	    public override Language Language
		{
			get { return Language.CSharp; }
		}

		public override ProjectType ProjectType
		{
			get { return ProjectType.NancyFx; }
		}

		//Good examples is here: http://www.jhovgaard.com/from-aspnet-mvc-to-nancy-part-2/
		private const string _viewCsSample = @"";

		private const string _modelCsSample = @"";

		private const string _controllerCsSample = @"
public class SampleModule : Nancy.NancyModule
{
    public SampleModule()
    {
        Get[""/""] = _ => ""Hello World! Yep!"";
    }
}";
		private const string _modelVbSample = @"";

		private const string _viewVbSample = @"";

		private const string _controllerVbSample = @"";

        // http://stackoverflow.com/questions/13601412/compilation-errors-when-dealing-with-c-sharp-script-using-roslyn
        public override CommonSyntaxTree ParseSyntaxTree(string code)
        {
            var options = new ParseOptions(CompatibilityMode.None, LanguageVersion.CSharp6, true, SourceCodeKind.Regular, null);
            var tree = ParseSyntaxTree(code, options);
            return tree;
        }

	    public override string GetSampleStorageId()
	    {
            return "CsNancy";
	    }

	    public override CodeBlock GetSampleCodeBlock()
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

		public RunResult Run(NancyFxRunOpts opts)
		{
		    return base.Run(opts);
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
					return GetAutoCompleteItems(codeBlock.Module, pos);
				case NancyFxFileType.View:
					return new List<AutoCompleteItem>();
				case NancyFxFileType.Controller:
					{
						var aggregateCode = codeBlock.Controller + codeBlock.Module;
						return GetAutoCompleteItems(aggregateCode, pos);
					}
				default:
					throw new ArgumentOutOfRangeException("nancyFxFileType");
			}
		}

        protected override bool VerifyDeniedCodeBlock(RunOptsBase opts, RunResult result)
        {
            var codeBlock = ((NancyFxCodeBlock) opts.CodeBlock);
            var aggregateCode = codeBlock.Controller + codeBlock.Module;
            return VerifyDeniedCode(aggregateCode, result);
        }


        private static readonly Object SyncObj = new object();
        protected override CompilerResults CompileNancyFx(RunOptsBase opts, int? warningLevel = null, bool loadAssembyToAppDomain = true)
        {
            CompilerResults result;

            var nancyFxRunOpts = (NancyFxRunOpts)opts;
            var codeBlock = ((NancyFxCodeBlock) opts.CodeBlock);

            List<string> CodeBlocks = new List<string>()
            {
                NancySelfHostingHelper.Instance.GenerateNancySelfHostingCode(nancyFxRunOpts.HostIndex),
                codeBlock.Controller,
                codeBlock.Module,
            };


            //ToDo: Add view as embded resources to assembly???
            if (!string.IsNullOrEmpty(codeBlock.View))
            {
                lock (SyncObj)
                {
                    
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"index.html"); 
                    // Write the stream contents to a new file named "index.html".
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(codeBlock.View);
                    }

                    result = CompileCode(
                        CodeBlocks,
                        new List<string>(){path},
                        warningLevel,
                        loadAssembyToAppDomain);
                }
            }
            else
            {
                result = CompileCode(
                        CodeBlocks,
                        null,
                        warningLevel,
                        loadAssembyToAppDomain);
            }

            return result;
        }
	}
}
