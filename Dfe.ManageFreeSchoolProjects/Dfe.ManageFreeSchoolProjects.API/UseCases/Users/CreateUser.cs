using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Users
{
    public interface ICreateUser
    {
        public Task Execute(CreateUserRequest request);
    }

    public class CreateUser : ICreateUser
    {
        private DbContext _context;

        public CreateUser(MfspContext context)
        {
            _context = context;
        }

        public async Task Execute(CreateUserRequest request)
        {
            var dbUser = new User()
            {
                Email = request.Email.ToLower()
            };

            _context.Add(dbUser);

            await _context.SaveChangesAsync();
        }
    }
}
