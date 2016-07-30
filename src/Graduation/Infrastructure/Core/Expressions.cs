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

        public static Expression<Func<Category, object>>[] LoadCardNavigations()
        {
            Expression<Func<Category, object>>[] _navigations = {
                         a => a.Name,
                         a=>a.UrlSlug,
                         a=>a.Id
             };

            return _navigations;
        }
    }
}
