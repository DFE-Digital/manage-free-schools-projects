using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System;
using System.Diagnostics.Contracts;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Helpers
{
    public class DatabaseModelBuilder
    {
        private static Fixture _fixture = new Fixture();

        public static Kpi BuildProject()
        {
            var result = BuildProjectMandatoryFieldsOnly();

            result.ProjectStatusProjectId = CreateProjectId();
            result.ProjectStatusCurrentFreeSchoolName = _fixture.Create<string>();
            result.TrustName = _fixture.Create<string>();

            result.RatProvisionalOpeningDateAgreedWithTrust = _fixture.Create<DateTime>();

            result.ProjectStatusCurrentFreeSchoolName = _fixture.Create<string>();
            result.ProjectStatusProjectStatus = _fixture.Create<string>();
            result.ProjectStatusFreeSchoolsApplicationNumber = _fixture.Create<string>().Substring(0, 9);
            result.ProjectStatusUrnWhenGivenOne = _fixture.Create<string>();
            result.ProjectStatusFreeSchoolApplicationWave = _fixture.Create<string>();
            result.ProjectStatusRealisticYearOfOpening = _fixture.Create<string>();
            result.ProjectStatusDateOfEntryIntoPreOpening = _fixture.Create<DateTime>();
            result.ProjectStatusProvisionalOpeningDateAgreedWithTrust = _fixture.Create<DateTime>();
            result.ProjectStatusActualOpeningDate = _fixture.Create<DateTime>();
            result.ProjectStatusTrustsPreferredYearOfOpening = _fixture.Create<string>();

            result.LocalAuthority = _fixture.Create<string>();
            result.SchoolDetailsGeographicalRegion = _fixture.Create<string>();
            result.SchoolDetailsConstituency = _fixture.Create<string>();
            result.SchoolDetailsConstituencyMp = _fixture.Create<string>();
            result.SchoolDetailsNumberOfFormsOfEntry = _fixture.Create<string>();
            result.SchoolDetailsSchoolTypeMainstreamApEtc = _fixture.Create<string>();
            result.SchoolDetailsSchoolPhasePrimarySecondary = _fixture.Create<string>();
            result.SchoolDetailsAgeRange = _fixture.Create<string>();
            result.SchoolDetailsGender = _fixture.Create<string>();
            result.SchoolDetailsNursery = _fixture.Create<string>();
            result.SchoolDetailsSixthForm = _fixture.Create<string>();
            result.SchoolDetailsIndependentConverter = _fixture.Create<string>();
            result.SchoolDetailsSpecialistResourceProvision = _fixture.Create<string>();
            result.SchoolDetailsFaithStatus = _fixture.Create<string>();
            result.SchoolDetailsFaithType = _fixture.Create<string>();
            result.SchoolDetailsTrustId = _fixture.Create<string>().Substring(0, 4);
            result.SchoolDetailsTrustName = _fixture.Create<string>();
            result.SchoolDetailsTrustType = _fixture.Create<string>();

            return result;
        }

        public static Kpi BuildProjectMandatoryFieldsOnly()
        {
            var result = new Kpi();

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

        public static string CreateProjectId()
        {
            return _fixture.Create<string>().Substring(0, 24);

        }
    }
}
