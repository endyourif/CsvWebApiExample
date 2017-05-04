using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CsvWebApiExample
{
    public class CsvStreamWriter
    {
        public static void WriteStream(Type type, object contents, Stream stream)
        {
            Type itemType = type.GetGenericArguments()[0];

            using (StringWriter stringWriter = new StringWriter())
            {
                // Write the list of property names on the first line
                stringWriter.WriteLine(string.Join<string>(
                        ",", itemType.GetProperties().Select(x => x.Name)
                    )
                );

                // Loop all objects and write their values
                foreach (var obj in (IEnumerable<object>)contents)
                {
                    var values = obj.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(obj, null)
                        }
                    );

                    string valueLine = string.Empty;

                    foreach (var value in values)
                    {
                        if (value.Value != null)
                        {
                            string val = value.Value.ToString();

                            //If any double quotes, escape them
                            if (val.Contains("\""))
                                val = val.Replace("\"", @"""""");

                            //Check if the value contains special characters
                            if (val.Contains(",") || val.Contains("'") || val.Contains("\""))
                                val = string.Concat("\"", val, "\"");

                            //Replace any \r or \n special characters from a new line with a space
                            if (val.Contains("\r"))
                                val = val.Replace("\r", " ");
                            if (val.Contains("\n"))
                                val = val.Replace("\n", " ");

                            valueLine = string.Concat(valueLine, val, ",");
                        }
                        else
                        {
                            valueLine = string.Concat(valueLine, ",");
                        }
                    }

                    stringWriter.WriteLine(valueLine.TrimEnd(','));
                }

                using (var streamWriter = new StreamWriter(stream))
                    streamWriter.Write(stringWriter.ToString());
            }
        }
    }
}