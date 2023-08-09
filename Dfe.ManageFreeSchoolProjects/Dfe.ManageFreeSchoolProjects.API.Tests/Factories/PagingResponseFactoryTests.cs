using Dfe.ManageFreeSchoolProjects.API.ResponseModels;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using NSubstitute;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Factories
{
    public class PagingResponseFactoryTests
    {
        [Fact]
        public void CreatingPagingResponse_WithRecordCountLessThanCount_Should_ReturnPagingResponseWithNullNextPageUrl()
        {
            const int recordCount = 2;
            const int count = 50;
            const int page = 1;

            var expectedPagingResponse = new PagingResponse
            {
                Page = page,
                RecordCount = recordCount,
                NextPageUrl = null
            };

            var result = PagingResponseFactory.Create(page, count, recordCount, null);
            
            result.Should().BeEquivalentTo(expectedPagingResponse);
        }
        
        [Fact]
        public void CreatingPagingResponse_WithRecordGreaterThanViewedRecords_Should_ReturnPagingResponseWithNextPageUrlPointingToNextPage()
        {
            const int recordCount = 2;
            const int count = 1;
            const int page = 1;
            var expectedNextPageUrl = $@"/controller-name/?page={page + 1}&count={count}";

            var expectedPagingResponse = new PagingResponse
            {
                Page = page,
                RecordCount = recordCount,
                NextPageUrl = expectedNextPageUrl
            };

            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Scheme = "https";
            httpRequest.Path = "/controller-name/";
            httpRequest.Query = new QueryCollection();

            var result = PagingResponseFactory.Create(page, count, recordCount, httpRequest);
            
            result.Should().BeEquivalentTo(expectedPagingResponse);
        }

        [Fact]
        public void CreatingPagingResponse_WithRecordCountEqualToCountAndQueryParameters_Should_ReturnPagingResponseWithNextPageUrlPointingToNextPageIncludingQueryParameters()
        {
            const int recordCount = 2;
            const int count = 1;
            const int page = 1;

            var query = new Dictionary<string, StringValues>
            {
                {"queryParameter", "queryValue"}
            };

            var queryCollection = new QueryCollection(query);

            var expectedNextPageUrl = $@"/controller-name/?queryParameter=queryValue&page={page + 1}&count={count}";

            var expectedPagingResponse = new PagingResponse
            {
                Page = page,
                RecordCount = recordCount,
                NextPageUrl = expectedNextPageUrl
            };

            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Scheme = "https";
            httpRequest.Path = "/controller-name/";
            httpRequest.Query = queryCollection;

            var result = PagingResponseFactory.Create(page, count, recordCount, httpRequest);

            result.Should().BeEquivalentTo(expectedPagingResponse);
        }
    }
}