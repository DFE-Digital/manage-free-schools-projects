using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using Dfe.ManageFreeSchoolProjects.Data.Tests.DbGateways;
using Dfe.ManageFreeSchoolProjects.Data.Tests.Helpers;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dfe.ManageFreeSchoolProjects.Data.Tests.Tests;

[TestFixture]
public class ProjectTests : DatabaseTestFixture
{
	private readonly RandomGenerator _randomGenerator = new ();
	private readonly TestProjectDbGateway _gateway = new ();

	[Test]
	public void CreateNewCase_CreatesProject()
	{
		// act
		var createdCase = _gateway.GenerateTestProject();
		
		// assert
		createdCase.Id.Should().BeGreaterThan(0);

		var results = GetProjects(createdCase.Id);

		results.Count.Should().Be(1);
	}

    private static Project BuildProject(IDataRecord record)
    => new Project { Id = record.GetInt32(0), CreatedAt = record.GetDateTime(1), UpdatedAt = record.GetDateTime(2), CreatedBy = record.GetString(3) };

    private List<Project> GetProjects(int id)
    {
        using var context = CreateContext();
        using var command = context.Database.GetDbConnection().CreateCommand();

        command.CommandText =
            "SELECT Id, CreatedAt, UpdatedAt, CreatedBy FROM [openFreeSchool].[Projects]";
        command.Parameters.Add(new SqlParameter("Id", id));

        context.Database.OpenConnection();
        using var result = command.ExecuteReader();

        var projects = new List<Project>();
        while (result.Read())
        {
            projects.Add(BuildProject(result));
        }

        return projects;
    }
}