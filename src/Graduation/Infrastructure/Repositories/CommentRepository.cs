using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class CommentRepository: EntityBaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(GraduationDbContext context) : base(context) { }
    }
}
