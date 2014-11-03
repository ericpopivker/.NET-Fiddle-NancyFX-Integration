using DotNetFiddle.Infrastructure;

namespace DotNetFiddle.NancyFx
{
	public class NancyFxCodeBlock : CodeBlock
	{
		public string Module { get; set; }

		public string View { get; set; }

		public string Controller { get; set; }

		public NancyFxCodeBlock()
		{
		}

		public NancyFxCodeBlock(NancyFxCodeBlock copy)
		{
			if (copy == null)
				return;

			Module = copy.Module;
			View = copy.View;
			Controller = copy.Controller;
		}
		public override CodeBlock Clone()
		{
			return new NancyFxCodeBlock(this);
		}
	}
}