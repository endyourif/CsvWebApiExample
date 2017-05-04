using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace CsvWebApiExample
{
    public class CsvFormatter : BufferedMediaTypeFormatter
    {
        public CsvFormatter(MediaTypeMapping mediaTypeMapping)
        {
            MediaTypeMappings.Add(mediaTypeMapping);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (null == type)
                return false;

            return IsTypeOfIEnumerable(type);
        }

        public override void WriteToStream(Type type, object value, Stream stream, HttpContent content)
        {
            CsvStreamWriter.WriteStream(type, value, stream);
        }

        private bool IsTypeOfIEnumerable(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
                if (interfaceType == typeof(IEnumerable))
                    return true;

            return false;
        }
    }
}