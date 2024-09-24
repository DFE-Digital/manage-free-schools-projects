namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public record HeaderType<T> where T : IBulkEditDto
    {
            public string Name { get; set; }

            public IValidationCommand<T> Type { get; set; }

            public Func<T, string> GetFromDto { get; set; }
    }

}
