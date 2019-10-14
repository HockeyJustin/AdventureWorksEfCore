using AdventureworksQueries.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Dapper;

namespace AdventureworksQueries
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("EF vs Dapper!");

			// A query just so we discount startup times....
			QueryWithEF();
			QueryWithDapper();

			long totalSecondsEF = 0;
			long totalSecondsDp = 0;
			long iterationCount = 0;

			while(true)
			{
				iterationCount++;

				Stopwatch st = new Stopwatch();

				// ENTITY FRAMEWORK
				st.Start();
				QueryWithEF();
				st.Stop();

				totalSecondsEF = totalSecondsEF + st.ElapsedMilliseconds;
				//Console.WriteLine($"Entity in: {st.ElapsedMilliseconds}ms");
				st.Reset();


				// DAPPER
				st.Start();
				QueryWithDapper();
				st.Stop();

				totalSecondsDp = totalSecondsDp + st.ElapsedMilliseconds;
				//Console.WriteLine($"Dapper in: {st.ElapsedMilliseconds}ms");

				var efAverage = totalSecondsEF / iterationCount;
				var dpAverage = totalSecondsDp / iterationCount;

				Console.WriteLine($"Average EF: {efAverage}, Average Dapper: {dpAverage}");


			}
		}

		private static void QueryWithEF()
		{
			using (var context = new AdventureWorksContext())
			{
				var all = context.Product.ToList();
			}
		}

		private static void QueryWithDapper()
		{
			using (IDbConnection conn = new SqlConnection(AdventureWorksContext.ConnectionString))
			{
				string sQuery = "SELECT * FROM [SalesLT].[Product]";
				conn.Open();
				var result = conn.Query(sQuery).ToList();
			}
		}
	}
}
