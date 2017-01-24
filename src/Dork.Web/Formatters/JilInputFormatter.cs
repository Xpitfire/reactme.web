using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Dork.Web.Formatters
{
    public class JilInputFormatter : IInputFormatter
    {
        public bool CanRead(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var contentType = context.HttpContext.Request.ContentType;
            if (contentType == null
                || contentType == "application/json")
                return true;
            return false;
        }
        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var request = context.HttpContext.Request;
            if (request.ContentLength == 0)
            {
                if (context.ModelType.GetTypeInfo().IsValueType)
                    return InputFormatterResult.SuccessAsync(Activator.CreateInstance(context.ModelType));
                return InputFormatterResult.SuccessAsync(null);
            }
            // var encoding = Encoding.UTF8;//do we need to get this from the request im not sure yet
            var opts = new Options(includeInherited: true, serializationNameFormat: SerializationNameFormat.CamelCase, dateFormat: DateTimeFormat.ISO8601);
            using (var reader = new StreamReader(context.HttpContext.Request.Body))
            {
                string data = reader.ReadToEnd();
                var model = Jil.JSON.Deserialize(data, context.ModelType, opts);
                var result = InputFormatterResult.SuccessAsync(model);
                return result;
            }
        }
    }
}
