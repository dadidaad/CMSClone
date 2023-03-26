using CMSClone.Server.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace CMSClone.Server.Repositories.Extensions
{
    public static class CourseRepositoryExtension
    {
        public static IQueryable<Course> Search(this IQueryable<Course> courses, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return courses;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return courses.Where(p => p.CourseCode.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Course> Sort(this IQueryable<Course> courses, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return courses.OrderBy(e => e.CourseCode);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Course).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return courses.OrderBy(e => e.CourseCode);

            return courses.OrderBy(orderQuery);
        }
    }
}
