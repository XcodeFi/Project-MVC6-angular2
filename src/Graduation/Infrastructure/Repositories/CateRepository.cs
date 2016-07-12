using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class CateRepository:EntityBaseRepository<Category>,ICateRepository
    {
        public CateRepository(GraduationDbContext context) : base(context) { }
    }
}
