namespace Dfe.ManageFreeSchoolProjects.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

         public List<Kpi> Projects { get; set; } = new();
    }
}
