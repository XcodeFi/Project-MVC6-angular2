using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Infrastructure.Repositories
{
    public class CardRepository: EntityBaseRepository<Card>, ICardRepository
    {
        public CardRepository(GraduationDbContext context)
            : base(context)
        { }
    }
}
