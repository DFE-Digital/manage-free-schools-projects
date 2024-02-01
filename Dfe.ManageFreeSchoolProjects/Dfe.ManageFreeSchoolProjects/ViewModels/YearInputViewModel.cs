namespace Dfe.ManageFreeSchoolProjects.ViewModels
{
    public class YearInputViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Year { get; set; }
        public string Label { get; set; }
        public bool HeadingLabel { get; set; }
        public string Hint { get; set; }
        public string ErrorMessage { get; set; }
        
        public bool YearInvalid { get; set; }
    }
}