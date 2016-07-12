using Graduation.Entities;
using Graduation.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Infrastructure.Repositories
{
    public class SliderRepository:EntityBaseRepository<Slider>,ISliderRepository
    {
        public SliderRepository(GraduationDbContext context)
            : base(context)
        { }
}
}
