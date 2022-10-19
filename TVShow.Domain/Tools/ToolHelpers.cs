using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.Tools
{
    public static class ToolHelpers
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> q, string sortField, bool ascending)
        {
            try
            {
                var param = Expression.Parameter(typeof(T), "p");
                var prop = Expression.Property(param, sortField);
                var exp = Expression.Lambda(prop, param);
                var method = ascending ? "OrderBy" : "OrderByDescending";
                var types = new Type[] { q.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
                return q.Provider.CreateQuery<T>(mce);
            }
            catch
            {
                return q;
            }
        }

    }
}
