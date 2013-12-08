using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;

namespace MusicCompany.Infrastructure.SchemaExport
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("**************************");
			Console.WriteLine("Schema Export Tool");
			Console.WriteLine("**************************");
			Console.WriteLine("");
			try
			{
				CreateSchema(@"..\..\MusicCompany.Core.xml", @"..\..\MusicCompany.Core.sql");
				CreateSchema(@"..\..\MusicCompany.Common.xml", @"..\..\MusicCompany.Common.sql");
			}
			catch ( Exception ex )
			{
				Console.WriteLine(ex);
			}
			Console.ReadLine();
		}

		private static void CreateSchema(string configFile, string outputFile)
		{
			Console.WriteLine("Loading Configuration from {0}", configFile);
			Configuration config = new Configuration();
			config.Configure(configFile);
			config.BuildSessionFactory();
			NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(config);
			schemaExport.SetOutputFile(outputFile);
			Console.WriteLine("Exporting Schema to {0}...", outputFile);
			schemaExport.Create(false, false);
			Console.WriteLine("Operation completed successfully");
		}
	}
}
