using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;
using System;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class FinanceTaskApiTests : ApiTestsBase
    {
        public FinanceTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewFinancePlanTask_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var dateAgreed = DateTime.Now.Date.AddDays(-5);
            var rpaStartDate = DateTime.Now.Date.AddDays(10);

            var comments = "Some comments";

            var request = new UpdateProjectByTaskRequest()
            {
                FinancePlan = new FinancePlanTask()
                {
                    FinancePlanAgreed = YesNo.Yes,
                    LocalAuthorityAgreedPupilNumbers = YesNoNotApplicable.Yes,
                    DateAgreed = dateAgreed,
                    PlanSavedInWorkplacesFolder = YesNo.Yes,
                    TrustWillOptIntoRpa = YesNo.Yes,
                    RpaCoverType = "RpaCoverType",
                    RpaStartDate = rpaStartDate,
                    UnderwrittenPlacesPrimaryYear1 = 1,
                    UnderwrittenPlacesPrimaryYear2 = 2,
                    UnderwrittenPlacesPrimaryYear3 = 3,
                    UnderwrittenPlacesPrimaryYear4 = 4,
                    UnderwrittenPlacesPrimaryYear5 = 5,
                    UnderwrittenPlacesPrimaryYear6 = 6,
                    UnderwrittenPlacesPrimaryYear7 = 7,
                    UnderwrittenPlacesSecondaryYear1 = 8,
                    UnderwrittenPlacesSecondaryYear2 = 9,
                    UnderwrittenPlacesSecondaryYear3 = 10,
                    UnderwrittenPlacesSecondaryYear4 = 11,
                    UnderwrittenPlacesSecondaryYear5 = 12,
                    UnderwrittenPlacesSixteenToNineteenYear1 = 13,
                    UnderwrittenPlacesSixteenToNineteenYear2 = 14,
                    UnderwrittenPlacesSixteenToNineteenYear3 = 15,
                    ConfirmationFromLocalAuthoritySavedInWorkplacesFolder = true,
                    CommentsAboutUnderwrittenPlaces = comments

                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.FinancePlan.ToString());

            projectResponse.FinancePlan.FinancePlanAgreed.Should().Be(YesNo.Yes);
            projectResponse.FinancePlan.LocalAuthorityAgreedPupilNumbers.Should().Be(YesNoNotApplicable.Yes);
            projectResponse.FinancePlan.DateAgreed.Should().Be(dateAgreed);
            projectResponse.FinancePlan.PlanSavedInWorkplacesFolder.Should().Be(YesNo.Yes);
            projectResponse.FinancePlan.TrustWillOptIntoRpa.Should().Be(YesNo.Yes);
            projectResponse.FinancePlan.RpaCoverType.Should().Be("RpaCoverType");
            projectResponse.FinancePlan.RpaStartDate.Should().Be(rpaStartDate);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear1.Should().Be(1);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear2.Should().Be(2);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear3.Should().Be(3);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear4.Should().Be(4);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear5.Should().Be(5);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear6.Should().Be(6);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear7.Should().Be(7);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear1.Should().Be(8);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear2.Should().Be(9);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear3.Should().Be(10);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear4.Should().Be(11);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear5.Should().Be(12);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear1.Should().Be(13);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear2.Should().Be(14);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear3.Should().Be(15);
            projectResponse.FinancePlan.ConfirmationFromLocalAuthoritySavedInWorkplacesFolder.Should().Be(true);
            projectResponse.FinancePlan.CommentsAboutUnderwrittenPlaces.Should().Be(comments);
        }

        [Fact]
        public async Task Patch_ExistingFinancePlanTask_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var milestone = DatabaseModelBuilder.BuildMilestone(project.Rid);
            context.Milestones.Add(milestone);

            await context.SaveChangesAsync();

            var dateAgreed = DateTime.Now.Date.AddDays(-5);
            var rpaStartDate = DateTime.Now.Date.AddDays(10);

            var comments = "Some comments";

            var request = new UpdateProjectByTaskRequest()
            {
                FinancePlan = new FinancePlanTask()
                {
                    FinancePlanAgreed = YesNo.No,
                    LocalAuthorityAgreedPupilNumbers = YesNoNotApplicable.NotApplicable,
                    DateAgreed = dateAgreed,
                    PlanSavedInWorkplacesFolder = YesNo.No,
                    TrustWillOptIntoRpa = YesNo.No,
                    RpaCoverType = "a new RpaCoverType",
                    RpaStartDate = rpaStartDate,
                    UnderwrittenPlacesPrimaryYear1 = 1,
                    UnderwrittenPlacesPrimaryYear2 = 2,
                    UnderwrittenPlacesPrimaryYear3 = 3,
                    UnderwrittenPlacesPrimaryYear4 = 4,
                    UnderwrittenPlacesPrimaryYear5 = 5,
                    UnderwrittenPlacesPrimaryYear6 = 6,
                    UnderwrittenPlacesPrimaryYear7 = 7,
                    UnderwrittenPlacesSecondaryYear1 = 8,
                    UnderwrittenPlacesSecondaryYear2 = 9,
                    UnderwrittenPlacesSecondaryYear3 = 10,
                    UnderwrittenPlacesSecondaryYear4 = 11,
                    UnderwrittenPlacesSecondaryYear5 = 12,
                    UnderwrittenPlacesSixteenToNineteenYear1 = 13,
                    UnderwrittenPlacesSixteenToNineteenYear2 = 14,
                    UnderwrittenPlacesSixteenToNineteenYear3 = 15,
                    ConfirmationFromLocalAuthoritySavedInWorkplacesFolder = true,
                    CommentsAboutUnderwrittenPlaces = comments
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.FinancePlan.ToString());

            projectResponse.FinancePlan.FinancePlanAgreed.Should().Be(YesNo.No);
            projectResponse.FinancePlan.LocalAuthorityAgreedPupilNumbers.Should().Be(YesNoNotApplicable.NotApplicable);
            projectResponse.FinancePlan.DateAgreed.Should().Be(dateAgreed);
            projectResponse.FinancePlan.PlanSavedInWorkplacesFolder.Should().Be(YesNo.No);
            projectResponse.FinancePlan.TrustWillOptIntoRpa.Should().Be(YesNo.No);
            projectResponse.FinancePlan.RpaCoverType.Should().Be("a new RpaCoverType");
            projectResponse.FinancePlan.RpaStartDate.Should().Be(rpaStartDate);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear1.Should().Be(1);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear2.Should().Be(2);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear3.Should().Be(3);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear4.Should().Be(4);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear5.Should().Be(5);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear6.Should().Be(6);
            projectResponse.FinancePlan.UnderwrittenPlacesPrimaryYear7.Should().Be(7);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear1.Should().Be(8);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear2.Should().Be(9);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear3.Should().Be(10);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear4.Should().Be(11);
            projectResponse.FinancePlan.UnderwrittenPlacesSecondaryYear5.Should().Be(12);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear1.Should().Be(13);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear2.Should().Be(14);
            projectResponse.FinancePlan.UnderwrittenPlacesSixteenToNineteenYear3.Should().Be(15);
            projectResponse.FinancePlan.ConfirmationFromLocalAuthoritySavedInWorkplacesFolder.Should().Be(true);
            projectResponse.FinancePlan.CommentsAboutUnderwrittenPlaces.Should().Be(comments);
        }
    }
}
