using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemoForm
{
	public class ExternalCall
	{
		private int _longActionTime;

		public ExternalCall(int longActionTime)
		{
			_longActionTime = longActionTime;
		}

		public Task<string> LongAction()
		{
			return Task.Run(() =>
			{
				Thread.Sleep(_longActionTime);
				return "Long Action Result";
			});
		}

		public Task<string> LongActionWithError()
		{
			return Task.Run(() =>
			{
				Thread.Sleep(_longActionTime);
				throw new Exception("external error was thrown");
				return "Long Action Result";
			});
		}

	}
}
