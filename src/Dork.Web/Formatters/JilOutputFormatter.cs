using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Dork.Web.Formatters
{
    public class JilOutputFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return true;
        }
        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
            {
                Jil.JSON.Serialize(context.Object, writer, new Options(serializationNameFormat: SerializationNameFormat.CamelCase, includeInherited: true));
                await writer.FlushAsync();
            }
        }
    }
}
