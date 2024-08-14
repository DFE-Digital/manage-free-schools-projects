﻿using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectPaymentsApiTests : ApiTestsBase
    {
        public ProjectPaymentsApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        private static Fixture _fixture = new();

        [Fact]
        public async Task Get_ProjectPayments_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);


        }

        //Get project payments project not found
        [Fact]
        public async Task Get_ProjectPayments_Returns_404()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = "abcdef";

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //Update project payment
        [Fact]
        public async Task Update_ProjectPayments_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var request = new Payment()
            {
                PaymentIndex = 2,
                PaymentScheduleAmount = 444,
                PaymentScheduleDate = payments.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue,
                PaymentActualAmount = ParseDecimal(payments.ProjectDevelopmentGrantFundingAmountOf2ndPayment),
                PaymentActualDate = payments.ProjectDevelopmentGrantFundingDateOf2ndActualPayment,
            };

            var patchProjectPaymentsResponse = await _client.PutAsync($"/api/v1/client/projects/{projectId}/payments/update", request.ConvertToJson());
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var projectPayments = await getProjectPaymentsResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectPayments>>();
            var payment = projectPayments.Data.Payments.First(p => p.PaymentIndex == request.PaymentIndex);

            payment.PaymentActualDate.Should()
                   .Be(request.PaymentActualDate);
            payment.PaymentActualAmount.Should()
                   .Be(request.PaymentActualAmount);
            payment.PaymentScheduleAmount.Should()
                   .Be(request.PaymentScheduleAmount);
            payment.PaymentScheduleDate.Should()
                   .Be(request.PaymentScheduleDate);

        }

        //Update project payment index out of range
        [Fact]
        public async Task Update_ProjectPayments_ForPaymentIndexOutOfRange_Returns_404()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();
           
            var request = new Payment()
            {

                PaymentIndex = 3,
                PaymentScheduleAmount = ParseDecimal(payments.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue),
                PaymentScheduleDate = payments.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue,
                PaymentActualAmount = ParseDecimal(payments.ProjectDevelopmentGrantFundingAmountOf2ndPayment),
                PaymentActualDate = payments.ProjectDevelopmentGrantFundingDateOf2ndActualPayment,
        };

            var patchProjectPaymentsResponse = await _client.PutAsync($"/api/v1/client/projects/{projectId}/payments/update", request.ConvertToJson());
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        //Add project payment

        [Fact]
        public async Task Add_ProjectPayments_Returns_200()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var request = new Payment()
            {
                PaymentScheduleAmount = _fixture.Create<decimal>(),
                PaymentScheduleDate = new DateTime().AddDays(40),
                PaymentActualAmount = _fixture.Create<decimal>(),
                PaymentActualDate = new DateTime().AddDays(40)
            };

            var patchProjectPaymentsResponse = await _client.PostAsync($"/api/v1/client/projects/{projectId}/payments/add", request.ConvertToJson());
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var projectPayments = await getProjectPaymentsResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectPayments>>();
            var payment = projectPayments.Data.Payments
                .Last(p => p.PaymentActualDate is not null
                        || p.PaymentActualAmount is not null
                        || p.PaymentScheduleAmount is not null
                        || p.PaymentScheduleDate is not null
                    );

            payment.PaymentActualDate.Should()
                   .Be(request.PaymentActualDate);
            payment.PaymentActualAmount.Should()
                   .Be(request.PaymentActualAmount);
            payment.PaymentScheduleAmount.Should()
                   .Be(request.PaymentScheduleAmount);
            payment.PaymentScheduleDate.Should()
                   .Be(request.PaymentScheduleDate);

        }
        //Add project payment index out of range

        [Fact]
        public async Task Add_ProjectPayments_WhenPaymentsFull_Returns_404()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildAllProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var request = new Payment()
            {
                PaymentScheduleAmount = _fixture.Create<decimal>(),
                PaymentScheduleDate = new DateTime().AddDays(40),
                PaymentActualAmount = _fixture.Create<decimal>(),
                PaymentActualDate = new DateTime().AddDays(40),
            };

            var patchProjectPaymentsResponse = await _client.PostAsync($"/api/v1/client/projects/{projectId}/payments/add", request.ConvertToJson());
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        //Delete project payment index not found

        [Fact]
        public async Task Delete_ProjectPayments_Returns_200()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var paymentIndexToDelete = 2;

            var getProjectPaymentsBeforeDeleteResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsBeforeDeleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var countOfPaymentsBeforeDelete = GetCountOfPaymentsForProject(projectId);

            var patchProjectPaymentsResponse = await _client.DeleteAsync($"/api/v1/client/projects/{projectId}/payments/delete/{paymentIndexToDelete}");
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var countOfPaymentsAfterDelete = GetCountOfPaymentsForProject(projectId);

            countOfPaymentsBeforeDelete.Should().NotBe(countOfPaymentsAfterDelete);
        }
        [Fact]
        public async Task Delete_ProjectPayments_PaymentNotFound_Returns_404()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildProjectPayments(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var paymentIndexToDelete = 6;

            var patchProjectPaymentsResponse = await _client.DeleteAsync($"/api/v1/client/projects/{projectId}/payments/delete/{paymentIndexToDelete}");
            patchProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return decimal.Parse(value);
        }

        private async Task<int?> GetCountOfPaymentsForProject(string projectId)
        {
            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var projectPayments = await getProjectPaymentsResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectPayments>>();
            var countOfPayments = projectPayments.Data.Payments
                .Last(p => p.PaymentActualDate is not null
                        || p.PaymentActualAmount is not null
                        || p.PaymentScheduleAmount is not null
                        || p.PaymentScheduleDate is not null
                    ).PaymentIndex;

            return countOfPayments;
        }
    }
}
