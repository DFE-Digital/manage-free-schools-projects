namespace Dfe.ManageFreeSchoolProjects.API.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string value)
        {
            int.TryParse(value, out var result);

            return result;
        }
    }
}
