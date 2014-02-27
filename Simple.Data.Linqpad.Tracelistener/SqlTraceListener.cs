using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Simple.Data.Linqpad.Tracelistener
{
    public class SqlTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            try
            {
                var ass = AppDomain.CurrentDomain.GetAssemblies()
                    .First(x => x.GetName().Name.Equals("LINQPad"));

                var writer = ass.GetType("LINQPad.Util")
                    .GetProperty("SqlOutputWriter", BindingFlags.Public | BindingFlags.Static)
                    .GetGetMethod()
                    .Invoke(null, null) as TextWriter;

                message = PrettifyMessage(message);

                writer.Write(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Assemblies Loaded (requires LINQPad to be here somewhere!");
                Console.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.FullName).Aggregate(string.Empty, (s, s1) => s + "\r\n" + s1));
                Console.Write(ex.Message);
            }

            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            try
            {
                var ass = AppDomain.CurrentDomain.GetAssemblies()
                    .First(x => x.GetName().Name.Equals("LINQPad"));

                var writer = ass.GetType("LINQPad.Util")
                    .GetProperty("SqlOutputWriter", BindingFlags.Public | BindingFlags.Static)
                    .GetGetMethod()
                    .Invoke(null, null) as TextWriter;

                message = PrettifyMessage(message);

                writer.Write(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Assemblies Loaded (requires LINQPad to be here somewhere!");
                Console.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.FullName).Aggregate(string.Empty, (s, s1) => s + "\r\n" + s1));
                Console.Write(ex.Message);
            }

            Console.Write(message);
        }

        private static string PrettifyMessage(string message)
        {
            message = message.Replace(",", "\r\n,");
            message = message.Replace("from", "\r\nfrom");
            message = message.Replace(" JOIN", "\r\nJOIN");
            message = message.Replace(" WHERE", "\r\nWHERE");
            return message;
        }

    }
}
