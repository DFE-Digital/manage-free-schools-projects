using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Helpers
{
    public class DatabaseModelBuilder
    {
        private static Fixture _fixture = new Fixture();

        public static Kpi BuildProject()
        {
            var result = new Kpi();

            // Fields we use
            result.Rid = _fixture.Create<string>().Substring(0, 10);

            result.AprilIndicator = _fixture.Create<string>().Substring(0, 9);
            result.Wave = _fixture.Create<string>().Substring(0, 15);
            result.UpperStatus = _fixture.Create<string>().Substring(0, 10);
            result.FsType = _fixture.Create<string>().Substring(0, 13);
            result.FsType1 = _fixture.Create<string>().Substring(0, 15);
            result.MatUnitProjects = _fixture.Create<string>().Substring(0, 31);
            result.SponsorUnitProjects = _fixture.Create<string>();

            return result;
        }
    }
}
