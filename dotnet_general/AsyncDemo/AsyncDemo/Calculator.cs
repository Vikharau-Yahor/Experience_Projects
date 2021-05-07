using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo
{
	public class Calculator
	{
		private int initialNumber;

		public Calculator(int setupNumber)
		{
			initialNumber = setupNumber;
		}

		public async Task<int> Multiply(int multiplicatorNumber)
		{
			initialNumber = initialNumber * multiplicatorNumber;
			await LongAction().ConfigureAwait(true); // in console application SynContext doesn't matter
			var result = 3;
			result = initialNumber;
			await LongAction2();
			var a = 5 + 3;
			a = await LongActionResult();
			result += a;
			return result;
		}

		public async Task LongAction()
		{
			await Task.Delay(2000);
		}

		public async Task LongAction2()
		{
			await Task.Delay(2000);
		}

		public async Task<int> LongActionResult()
		{
			await Task.Delay(2000);
			return 33;
		}
	}
}
