using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class BlogRepository:EntityBaseRepository<Blog>,IBlogRepository
    {
        public BlogRepository(GraduationDbContext context)
            : base(context)
        { }
    }
}
