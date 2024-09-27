namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IHeaderDataInteration<T> where T : IBulkEditDto
    {
        public string GetFromDto(T dto);

        public T ApplyToDto(string value, T dto);
    }

    public record HeaderType<T> where T : IBulkEditDto
    {
            public string Name { get; set; }

            public IValidationCommand<T> Type { get; set; }

            public IHeaderDataInteration<T> DataInteration { get; set; }
    }

}
