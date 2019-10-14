using System;
using System.Linq;

namespace AdventureworksQueries
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("AdventureWorks Log Filler!");


			while(true)
			{
				using(var context= new Models.AdventureWorksContext())
				{
					var all = context.Product.ToList();

					var allErrors = context.ErrorLog.ToArray();

				

					var errorNumber = (allErrors != null && allErrors.Any()) ? allErrors.Max(_ => _.ErrorNumber) + 1: 1;

					Console.WriteLine($"Error log number {errorNumber}");

					Models.ErrorLog log = new Models.ErrorLog()
					{
						ErrorTime = DateTime.Now,
						UserName = "LogSplatter",
						ErrorNumber = errorNumber,
						ErrorSeverity = 0,
						ErrorProcedure = "Main",
						ErrorLine = 99,
						ErrorMessage = "Just filling the logs and adding IOPS"

					};

					context.ErrorLog.Add(log);
					context.SaveChanges();

					var allErrorsNow = context.ErrorLog.ToArray();

					//System.Threading.Thread.Sleep(100);
				}
			}


		}
	}
}
