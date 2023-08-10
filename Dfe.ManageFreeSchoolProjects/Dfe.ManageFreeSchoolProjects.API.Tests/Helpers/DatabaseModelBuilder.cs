namespace Dfe.ManageFreeSchoolProjects.API.Tests.Helpers
{
    public class DatabaseModelBuilder
    {
        private static Fixture _fixture = new Fixture();

        public static string GenerateProjectId()
        {
            return _fixture.Create<string>().Substring(0, 11);
        }

        public static string GenerateCreatedBy()
        {
            return _fixture.Create<string>().Substring(0, 11);
        }
    }
}
