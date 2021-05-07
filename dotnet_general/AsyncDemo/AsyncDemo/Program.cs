using System;
using System.Web;
namespace AsyncDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			var mainResult = DoTransform();
			var message = "Result is: ";
			Console.WriteLine(message + mainResult);
			Console.ReadKey();
		}

		private static string DoTransform()
		{
			var str = "DoTransformPart ";
			var str2 = HttpUtility.HtmlDecode("sdtsd;lt");
			var multiplicator = new Calculator(2);
			var calculationResult = multiplicator.Multiply(3).ConfigureAwait(true).GetAwaiter().GetResult();
			var transformedResult = str + calculationResult.ToString();
			return transformedResult;
			return str;
		}
	}
}
