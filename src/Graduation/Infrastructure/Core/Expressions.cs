using Graduation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Graduation.Infrastructure.Core
{
    public class Expressions
    {
        public static Expression<Func<Category, object>>[] LoadCateNavigations()
        {
            Expression<Func<Category, object>>[] _navigations = {
                         a => a.Cards,
                         a=>a.CateParent
             };

            return _navigations;
        }
    }
}
