using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class ContactRepository:EntityBaseRepository<Contact>,IContactRepository
    {
        public ContactRepository(GraduationDbContext context) : base(context) { }
    }
}
