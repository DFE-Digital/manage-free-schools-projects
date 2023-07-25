using AutoFixture.Idioms;
using AutoFixture;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Tests.RequestModels.Project
{
    public class GetAllProjectsRequest
    {
        [Fact]
        public void All_Constructors_Guard_Against_Null()
        {
            // Arrange
            var requestTypes = GetRequestTypes();
            var fixture = new Fixture();

            foreach (var requestType in requestTypes)
            {
                using (var scope = new AssertionScope())
                {
                    // Act & Assert
                    var assertion = fixture.Create<GuardClauseAssertion>();

                    assertion.Verify(requestType.GetConstructors());
                }
            }
        }

        [Fact]
        public void Properties_Are_Initialized_By_Constructor()
        {
            // Arrange
            var requestTypes = GetRequestTypes();
            var fixture = new Fixture();

            foreach (var requestType in requestTypes)
            {
                using (var scope = new AssertionScope())
                {
                    // Act & Assert
                    var assertion = fixture.Create<ConstructorInitializedMemberAssertion>();
                    assertion.Verify(requestType.GetConstructors());
                }
            }
        }

        [Fact]
        public void Property_Setters_Work_As_Expected()
        {
            // Arrange
            var requestTypes = GetRequestTypes();
            var fixture = new Fixture();

            foreach (var requestType in requestTypes)
            {
                using (var scope = new AssertionScope())
                {
                    // Act & Assert
                    var assertion = fixture.Create<WritablePropertyAssertion>();

                    assertion.Verify(requestType.GetProperties());
                }
            }
        }

        private TypeInfo[] GetRequestTypes()
        {
            return typeof(GetAllProjectsRequest).Assembly
                .DefinedTypes
                .Where(x =>
                    x.IsClass &&
                    x.Namespace != null
                    && x.Namespace.StartsWith(typeof(GetAllProjectsRequest).Namespace))
                .ToArray();
        }
    }
}
