using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.Tests.Utils;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
	public class ProjectTaskApiTests : ApiTestsBase
	{
		public ProjectTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
		{
		}

		[Fact]
		public async Task Get_ProjectByTask_NoDependentDataCreated_Returns_200()
		{
			// Ensures that if the child tables for the tasks are not populated, the api still works
			var project = DatabaseModelBuilder.BuildProject();
			var projectId = project.ProjectStatusProjectId;

			using var context = _testFixture.GetContext();
			context.Kpi.Add(project);

			var tasks = TasksStub.BuildListOfTasks(project.Rid);
			context.Tasks.AddRange(tasks);

			await context.SaveChangesAsync();

			var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/tasks/school");
			getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Get_ProjectByTask_DoesNotExist_Returns_404()
		{
			var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/NotExist/tasks/school");
			getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Patch_SchoolTask_Returns_201()
		{
			var project = DatabaseModelBuilder.BuildProject();
			var projectId = project.ProjectStatusProjectId;

			using var context = _testFixture.GetContext();
			context.Kpi.Add(project);

			var tasks = TasksStub.BuildListOfTasks(project.Rid);
			context.Tasks.AddRange(tasks);

			await context.SaveChangesAsync();

			var request = new UpdateProjectByTaskRequest()
			{
				School = new SchoolTask()
				{
					CurrentFreeSchoolName = "Test High School",
					SchoolType = SchoolType.Mainstream,
					AgeRange = "11-18",
					SchoolPhase = SchoolPhase.Primary,
					Nursery = ClassType.Nursery.No,
					SixthForm = ClassType.SixthForm.No,
					FaithStatus = FaithStatus.NotSet,
					FaithType = FaithType.Other,
					Gender = Gender.Mixed
				}
			};

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.School.ToString());

            projectResponse.School.CurrentFreeSchoolName.Should().Be("Test High School");
            projectResponse.School.SchoolType.Should().Be(SchoolType.Mainstream);
            projectResponse.School.SchoolPhase.Should().Be(SchoolPhase.Primary);
            projectResponse.School.AgeRange.Should().Be("11-18");
            projectResponse.School.Nursery.Should().Be(ClassType.Nursery.No);
            projectResponse.School.SixthForm.Should().Be(ClassType.SixthForm.No);
			projectResponse.SchoolName.Should().Be("Test High School");
        }

		[Fact]
		public async Task Patch_ConstituencyTask_Returns_201()
		{
			var project = DatabaseModelBuilder.BuildProject();
			var projectId = project.ProjectStatusProjectId;

			using var context = _testFixture.GetContext();
			context.Kpi.Add(project);

			var tasks = TasksStub.BuildListOfTasks(project.Rid);
			context.Tasks.AddRange(tasks);

			await context.SaveChangesAsync();

			const string Battersea = "Battersea";
			const string TeddyBones = "RT Hon Theodore Bones";
			const string MRL = "Monster Raving Loony";
			var request = new UpdateProjectByTaskRequest()
			{
				Constituency = new ConstituencyTask()
				{
					Name = Battersea,
					MPName = TeddyBones,
					Party = MRL,
				}
			};

			var projectResponse = await UpdateProjectTask(projectId, request, TaskName.Constituency.ToString());

			projectResponse.Constituency.Name.Should().Be(Battersea);
			projectResponse.Constituency.MPName.Should().Be(TeddyBones);
			projectResponse.Constituency.Party.Should().Be(MRL);
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

		[Fact]
		public async Task Patch_DatesTask_Returns_201()
		{
			var project = DatabaseModelBuilder.BuildProject();
			var projectId = project.ProjectStatusProjectId;

			using var context = _testFixture.GetContext();
			context.Kpi.Add(project);
			await context.SaveChangesAsync();

			var DateTenDaysInFuture = new DateTime().AddDays(10);
			var DateNineDaysInFuture = new DateTime().AddDays(9);

			var request = new UpdateProjectByTaskRequest()
			{
				Dates = new DatesTask()
				{
					DateOfEntryIntoPreopening = DateTenDaysInFuture,
					ProvisionalOpeningDateAgreedWithTrust = DateNineDaysInFuture,
                    OpeningAcademicYear = "2023 2024",
				}
			};

			var projectResponse = await UpdateProjectTask(projectId, request, TaskName.Dates.ToString());

			projectResponse.Dates.DateOfEntryIntoPreopening.Should().Be(DateTenDaysInFuture);
			projectResponse.Dates.ProvisionalOpeningDateAgreedWithTrust.Should().Be(DateNineDaysInFuture);
			projectResponse.Dates.OpeningAcademicYear.Should().Be("2023 2024");
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

		[Fact]
		public async Task Patch_LocalAuthorityAndRegionTask_Returns_201()
		{
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

			var request = new UpdateProjectByTaskRequest()
			{
				RegionAndLocalAuthorityTask = new()
				{
                    LocalAuthority = "LocalAuthority",
                    Region = "Region"
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.RegionAndLocalAuthority.ToString());

			projectResponse.RegionAndLocalAuthority.LocalAuthority.Should().Be("LocalAuthority");
			projectResponse.RegionAndLocalAuthority.Region.Should().Be("Region");
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

        [Fact]
        public async Task Patch_NewRiskAppraisalMeetingTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                RiskAppraisalMeeting = new RiskAppraisalMeetingTask()
                {
					ForecastDate = DateTenDaysInFuture,
					ActualDate = DateNineDaysInFuture,
					CommentsOnDecisionToApprove = "CommentsOnDecisionToApprove",
					InitialRiskAppraisalMeetingCompleted= true,
					ReasonNotApplicable= "ReasonNotApplicable"
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.RiskAppraisalMeeting.ToString());

            projectResponse.RiskAppraisalMeeting.ForecastDate.Should().Be(DateTenDaysInFuture);
            projectResponse.RiskAppraisalMeeting.ActualDate.Should().Be(DateNineDaysInFuture);
            projectResponse.RiskAppraisalMeeting.CommentsOnDecisionToApprove.Should().Be("CommentsOnDecisionToApprove");
            projectResponse.RiskAppraisalMeeting.InitialRiskAppraisalMeetingCompleted.Should().Be(true);
            projectResponse.RiskAppraisalMeeting.ReasonNotApplicable.Should().Be("ReasonNotApplicable");
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

        [Fact]
        public async Task Patch_ExistingRiskAppraisalMeetingTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var riskAppraisalMeetingTask = DatabaseModelBuilder.BuildRiskAppraisalMeetingTask(project.Rid);
            context.RiskAppraisalMeetingTask.Add(riskAppraisalMeetingTask);

            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                RiskAppraisalMeeting = new RiskAppraisalMeetingTask()
                {
                    ForecastDate = DateTenDaysInFuture,
                    ActualDate = DateNineDaysInFuture,
                    CommentsOnDecisionToApprove = "CommentsOnDecisionToApprove",
                    InitialRiskAppraisalMeetingCompleted = true,
                    ReasonNotApplicable = "ReasonNotApplicable"
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.RiskAppraisalMeeting.ToString());

            projectResponse.RiskAppraisalMeeting.ForecastDate.Should().Be(DateTenDaysInFuture);
            projectResponse.RiskAppraisalMeeting.ActualDate.Should().Be(DateNineDaysInFuture);
            projectResponse.RiskAppraisalMeeting.CommentsOnDecisionToApprove.Should().Be("CommentsOnDecisionToApprove");
            projectResponse.RiskAppraisalMeeting.InitialRiskAppraisalMeetingCompleted.Should().Be(true);
            projectResponse.RiskAppraisalMeeting.ReasonNotApplicable.Should().Be("ReasonNotApplicable");

			context.RiskAppraisalMeetingTask.Count(r => r.RID == project.Rid).Should().Be(1);
        }

		[Fact]
		public async Task Patch_TrustTask_Returns_201()
		{
            var project = DatabaseModelBuilder.BuildProject();
			var trust = DatabaseModelBuilder.BuildTrust();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
			context.Trust.Add(trust);
            await context.SaveChangesAsync();

			var request = new UpdateProjectByTaskRequest()
			{
				Trust = new TrustTask()
				{
					TRN = trust.TrustRef
				}
			};

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.Trust.ToString());

			projectResponse.Trust.TRN.Should().Be(trust.TrustRef);
			projectResponse.Trust.TrustName.Should().Be(trust.TrustsTrustName);
			projectResponse.Trust.TrustType.Should().Be(ProjectMapper.ToTrustType(trust.TrustsTrustType));
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

        [Fact]
        public async Task Patch_NewArticlesOfAssociation_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                ArticlesOfAssociation = new ArticlesOfAssociationTask()
                {
                    ForecastDate = DateTenDaysInFuture,
                    ActualDate = DateNineDaysInFuture,
                    CommentsOnDecision = "CommentsOnDecisionToApprove",
                    ChairHaveSubmittedConfirmation = true,
                    CheckedSubmittedArticlesMatch = true,
                    ArrangementsMatchGovernancePlans = true,
                    SharepointLink = "https://sharepoint/completed"
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.ArticlesOfAssociation.ToString());

            projectResponse.ArticlesOfAssociation.ForecastDate.Should().Be(DateTenDaysInFuture);
            projectResponse.ArticlesOfAssociation.ActualDate.Should().Be(DateNineDaysInFuture);
            projectResponse.ArticlesOfAssociation.CommentsOnDecision.Should().Be("CommentsOnDecisionToApprove");
            projectResponse.ArticlesOfAssociation.ChairHaveSubmittedConfirmation.Should().Be(true);
            projectResponse.ArticlesOfAssociation.CheckedSubmittedArticlesMatch.Should().Be(true);
            projectResponse.ArticlesOfAssociation.ArrangementsMatchGovernancePlans.Should().Be(true);
            projectResponse.ArticlesOfAssociation.SharepointLink.Should().Be("https://sharepoint/completed");
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

        [Fact]
        public async Task Patch_ExistingNewArticlesOfAssociation_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var articlesOfAssociationTask = DatabaseModelBuilder.BuildArticlesOfAssociationTask(project.Rid);
            context.Milestones.Add(articlesOfAssociationTask);

            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                ArticlesOfAssociation = new ArticlesOfAssociationTask()
                {
                    ForecastDate = DateTenDaysInFuture,
                    ActualDate = DateNineDaysInFuture,
                    CommentsOnDecision = "CommentsOnDecisionToApprove",
                    ChairHaveSubmittedConfirmation = true,
                    CheckedSubmittedArticlesMatch = true,
                    ArrangementsMatchGovernancePlans = true,
                    SharepointLink = "https://sharepoint/completed"
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.ArticlesOfAssociation.ToString());

            projectResponse.ArticlesOfAssociation.ForecastDate.Should().Be(DateTenDaysInFuture);
            projectResponse.ArticlesOfAssociation.ActualDate.Should().Be(DateNineDaysInFuture);
            projectResponse.ArticlesOfAssociation.CommentsOnDecision.Should().Be("CommentsOnDecisionToApprove");
            projectResponse.ArticlesOfAssociation.ChairHaveSubmittedConfirmation.Should().Be(true);
            projectResponse.ArticlesOfAssociation.CheckedSubmittedArticlesMatch.Should().Be(true);
            projectResponse.ArticlesOfAssociation.ArrangementsMatchGovernancePlans.Should().Be(true);
            projectResponse.ArticlesOfAssociation.SharepointLink.Should().Be("https://sharepoint/completed");
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
        }

        [Fact]
        public async Task Patch_NewKickOffMeeting_Returns_201()
        {
	        var project = DatabaseModelBuilder.BuildProject();
	        var projectId = project.ProjectStatusProjectId;

	        using var context = _testFixture.GetContext();
	        context.Kpi.Add(project);
	        await context.SaveChangesAsync();
	        
	        var dateNineDaysInFuture = new DateTime().AddDays(9);

	        var request = new UpdateProjectByTaskRequest()
	        {
		        KickOffMeeting = new KickOffMeetingTask()
		        {
			        FundingArrangementAgreed = true,
			        RealisticYearOfOpening = "2049/2050",
			        FundingArrangementDetailsAgreed = "text",
			        ProvisionalOpeningDate = dateNineDaysInFuture,
			        SharepointLink = "https://sharepoint/completed"
		        }
	        };
	        
	        var projectResponse = await UpdateProjectTask(projectId, request, TaskName.KickOffMeeting.ToString());

	        projectResponse.KickOffMeeting.SharepointLink.Should().Be(request.KickOffMeeting.SharepointLink);
	        projectResponse.KickOffMeeting.RealisticYearOfOpening.Should().Be(request.KickOffMeeting.RealisticYearOfOpening);
	        projectResponse.KickOffMeeting.ProvisionalOpeningDate.Should().Be(request.KickOffMeeting.ProvisionalOpeningDate);
	        projectResponse.KickOffMeeting.FundingArrangementAgreed.Should().Be(request.KickOffMeeting.FundingArrangementAgreed);
	        projectResponse.KickOffMeeting.FundingArrangementDetailsAgreed.Should().Be(request.KickOffMeeting.FundingArrangementDetailsAgreed);
        }
        
		[Fact]
        public async Task Patch_ExistingKickOffMeeting_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var kickOffMeetingTask = DatabaseModelBuilder.BuildKickOffMeetingTask(project.Rid);
            context.Milestones.Add(kickOffMeetingTask);

            await context.SaveChangesAsync();
            
            var dateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
	            KickOffMeeting = new KickOffMeetingTask()
	            {
		            FundingArrangementAgreed = true,
		            RealisticYearOfOpening = "2049/2050",
		            FundingArrangementDetailsAgreed = "text",
		            ProvisionalOpeningDate = dateNineDaysInFuture,
		            SharepointLink = "https://sharepoint/completed"
	            }
            };

            var projectResponse = await UpdateProjectTask(projectId, request, TaskName.KickOffMeeting.ToString());

            projectResponse.KickOffMeeting.SharepointLink.Should().Be(request.KickOffMeeting.SharepointLink);
            projectResponse.KickOffMeeting.RealisticYearOfOpening.Should().Be(request.KickOffMeeting.RealisticYearOfOpening);
            projectResponse.KickOffMeeting.ProvisionalOpeningDate.Should().Be(request.KickOffMeeting.ProvisionalOpeningDate);
            projectResponse.KickOffMeeting.FundingArrangementAgreed.Should().Be(request.KickOffMeeting.FundingArrangementAgreed);
            projectResponse.KickOffMeeting.FundingArrangementDetailsAgreed.Should().Be(request.KickOffMeeting.FundingArrangementDetailsAgreed);
        }

        [Fact]
		public async Task Patch_Task_NoProjectExists_Returns_404()
		{
			var request = new UpdateProjectByTaskRequest()
			{
			};

			var updateTaskResponse = await _client.PatchAsync($"/api/v1/client/projects/NotExist/tasks", request.ConvertToJson());
			updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		private async Task<GetProjectByTaskResponse> UpdateProjectTask(string projectId, UpdateProjectByTaskRequest request, string taskName)
		{
			var updateTaskResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/tasks", request.ConvertToJson());
			updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/tasks/{taskName}");
			getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await getProjectByTaskResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetProjectByTaskResponse>>();

			return result.Data;
		}
	}
}
