namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IHeaderDataInteraction<T> where T : IBulkEditDto
    {
        public string GetFromDto(T dto);

        public T ApplyToDto(string value, T dto);

        public string FormatValue(string value);
    }

    public record HeaderType<T> where T : IBulkEditDto
    {
            public string Name { get; set; }

            public IValidationCommand<T> Type { get; set; }

            public IHeaderDataInteraction<T> DataInteraction { get; set; }
    }

}
