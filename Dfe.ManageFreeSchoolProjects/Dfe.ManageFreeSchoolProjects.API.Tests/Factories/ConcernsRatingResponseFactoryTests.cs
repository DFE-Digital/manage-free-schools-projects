using Dfe.ManageFreeSchoolProjects.API.Factories;
using Dfe.ManageFreeSchoolProjects.API.ResponseModels;
using Dfe.ManageFreeSchoolProjects.Data.Models;
using System;
using FluentAssertions;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Factories
{
    public class ConcernsRatingResponseFactoryTests
    {
        [Fact]
        public void ReturnsConcernsRatingResponse_WhenGivenAnConcernsRating()
        {
            var concernsRating = new ConcernsRating
            {
                Id = 5,
                Name = "Test concerns rating",
                CreatedAt = new DateTime(2021, 10,07),
                UpdatedAt = new DateTime(2021, 10,07)
            };

            var expected = new ConcernsRatingResponse
            {
                Name = concernsRating.Name,
                CreatedAt = concernsRating.CreatedAt,
                UpdatedAt = concernsRating.UpdatedAt,
                Id = concernsRating.Id
            };

            var result = ConcernsRatingResponseFactory.Create(concernsRating);
            result.Should().BeEquivalentTo(expected);
        }
    }
}