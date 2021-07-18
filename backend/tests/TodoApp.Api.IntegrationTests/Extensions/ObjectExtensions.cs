using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.Api.IntegrationTests.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToQueryString(this object obj)
        {
            var result = new List<string>();
            var props = obj.GetType().GetProperties().Where(p => p.GetValue(obj, null) != null);
            foreach (var p in props)
            {
                var value = p.GetValue(obj, null);

                result.Add(string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(value.ToString())));
            }

            return string.Join("&", result.ToArray());
        }
    }
}
