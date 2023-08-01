using Dfe.ManageFreeSchoolProjects.Data.Models;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using FizzWare.NBuilder;

namespace Dfe.ManageFreeSchoolProjects.Data.Tests.TestData;

public class TestDataFactory
{
	private readonly RandomGenerator _randomGenerator = new ();
	
	public Project BuildProject()
		=> new Project
        {
			ProjectId = _randomGenerator.NextString(4, 10),
            ApplicationNumber = _randomGenerator.NextString(4, 10),
			ApplicationWave = _randomGenerator.NextString(4, 10),
			SchoolName = _randomGenerator.NextString(4, 10),
            CreatedAt = _randomGenerator.DateTime(),
			UpdatedAt = _randomGenerator.DateTime(),
			CreatedBy = _randomGenerator.NextString(3, 10)
		};
}