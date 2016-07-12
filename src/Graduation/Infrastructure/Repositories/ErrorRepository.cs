using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class LoggingRepository:EntityBaseRepository<Error>,ILoggingRepository
    {
        public LoggingRepository(GraduationDbContext context) : base(context) { }
    }
}
