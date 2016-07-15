using Graduation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Infrastructure.Repositories.Abstract
{
    public class ViewRepository : EntityBaseRepository<View>, IViewRepository
    {
        public ViewRepository(GraduationDbContext context)
            : base(context)
        { }
    }
}
