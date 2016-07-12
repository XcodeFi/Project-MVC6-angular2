using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Entities;

namespace Graduation.Infrastructure.Repositories
{
    public class CateBlogRepository:EntityBaseRepository<CategoryBlog>, ICateBlogRepository
    {
        public CateBlogRepository(GraduationDbContext context) : base(context) { }
    }
}
