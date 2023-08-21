﻿using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Helpers
{
    public class DatabaseModelBuilder
    {
        private static Fixture _fixture = new Fixture();

        public static Kpi BuildProject()
        {
            var result = BuildProjectMandatoryFieldsOnly();

            // Fields we use
            result.Rid = _fixture.Create<string>().Substring(0, 10);
            result.ProjectStatusProjectId = _fixture.Create<string>().Substring(0, 24);
            result.ProjectStatusCurrentFreeSchoolName = _fixture.Create<string>();
            result.TrustName = _fixture.Create<string>();
            result.LocalAuthority = _fixture.Create<string>();
            result.RatProvisionalOpeningDateAgreedWithTrust = _fixture.Create<DateTime>();
            result.SchoolDetailsGeographicalRegion = _fixture.Create<string>();

            return result;
        }

        public static Kpi BuildProjectMandatoryFieldsOnly()
        {
            var result = new Kpi();

            // Fields that are mandatory
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
