using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetFiddle.Infrastructure;

namespace DotNetFiddle.NancyFx
{

	[Serializable]
	public class NancyFxValidateResult
	{
		public bool IsSuccess { get; set; }
		public List<ValidationError> ModuleErrors { get; set; }
		public List<ValidationError> ViewErrors { get; set; }
		public List<ValidationError> ModelErrors { get; set; }
	}
}
