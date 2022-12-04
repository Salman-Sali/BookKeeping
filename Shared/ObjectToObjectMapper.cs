using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bk.Shared
{
    public static class ObjectToObjectMapper<TRequest, TResponse>
    {
        private static List<PropertyInfo>? responseProperties;
        public static List<PropertyInfo>? ResponseProperties {
            get {
                responseProperties = new List<PropertyInfo>();
                foreach (var item in typeof(TResponse).GetProperties())
                {
                    responseProperties.Add(item);
                }
                return responseProperties;
            }
        }

        private static List<PropertyInfo>? requestProperties;
        public static List<PropertyInfo>? RequestProperties
        {
            get
            {
                requestProperties = new List<PropertyInfo>();
                foreach (var item in typeof(TRequest).GetProperties())
                {
                    requestProperties.Add(item);
                }
                return requestProperties;
            }
        }

        public static TResponse Map(TRequest data)
        {
            var response = Activator.CreateInstance<TResponse>();
            foreach (var item in RequestProperties ?? Enumerable.Empty<PropertyInfo>())
            {
                if (ResponseProperties.Contains(item))
                {
                    ResponseProperties.Where(x=>x == item).First().SetValue(response, item.GetValue(data));
                }
            }
            return response;
        }

        public static TResponse Map(TRequest request, TResponse response)
        {
            foreach (var item in RequestProperties ?? Enumerable.Empty<PropertyInfo>())
            {
                var similarResponseProperty = ResponseProperties.Where(x=>x.Name == item.Name && x.PropertyType == item.PropertyType).FirstOrDefault();
                if (similarResponseProperty != null)
                {
                    similarResponseProperty.SetValue(response, item.GetValue(request));
                }
            }
            return response;
        }

        public static int CountUnmappableProperties()
        {
            int count = 0;

            foreach (var item in RequestProperties)
            {
                if (!ResponseProperties.Contains(item))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
