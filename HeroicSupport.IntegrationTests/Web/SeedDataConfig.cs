using System.IO;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.Data;
using SpecsFor.Mvc;

namespace HeroicSupport.IntegrationTests.Web
{
	public class SeedDataConfig : SpecsForMvcConfig
	{
		public SeedDataConfig()
		{
			BeforeEachTest(ResetDatabase);
		}

		private void ResetDatabase()
		{
			const string connectionString = @"Data Source=(LocalDb)\v11.0;Initial Catalog=HeroicSupportTests;Integrated Security=SSPI;AttachDBFilename={0}\SpecsForMvc.TestSite\App_Data\HeroicSupport.mdf";
			MsSqlBootstrapper.Bootstrap(string.Format(connectionString, Directory.GetCurrentDirectory()));
			MsSqlBootstrapper.CreateSchema();

			using (var session = MsSqlBootstrapper.GetSession())
			{
				var user = User.CreateNewUser("Test User 1", "test@user.com", "Password1");
				session.Save(user);

				session.Flush();
			}
		}
	}
}