using Dfe.ManageFreeSchoolProjects.Data.Models;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using Dfe.ManageFreeSchoolProjects.Data.Tests.TestData;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.Data.Tests.DbGateways;

public class TestProjectDbGateway : DatabaseTestFixture
{
	protected readonly TestDataFactory _testDataFactory = new ();
	
	public Project GenerateTestProject()
	{
		var project = _testDataFactory.BuildProject();

		return AddProject(project);
	}

	public Project AddProject(Project project)
	{
		using var context = CreateContext();
		context.Projects.Add(project);
		context.SaveChanges();
		
		return GetProject(project.Id);
	}
			
	public Project UpdateProject(Project project)
	{
		using var context = CreateContext();
		context.Projects.Update(project);
		context.SaveChanges();
		return project;
	}
	
	public Project GetProject(int id)
	{
		using var context = CreateContext();
		
		return context.Projects
            .Where(x => x.Id == id)
			.Single();
	}
	
	public Project GetDefaultProjectStatus()
	{
		using var context = CreateContext();
		return context.Projects.First();
	}

	public Project GetDefaultProjectRating()
	{
		using var context = CreateContext();
		return context.Projects.First();
	}
	
	public Project GetDifferentProjectRating(int currentId)
	{
		using var context = CreateContext();
		return context.Projects.First(x => x.Id != currentId);
	}
}