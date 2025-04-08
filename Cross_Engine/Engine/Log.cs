using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossEngine.Engine
{
    class Log
    {
        static StringBuilder errorLog = new StringBuilder();

        public static void PrintDirLocation()
        {
            Print("Location: " + Directory.GetCurrentDirectory());
        }

        public static void Print(string text)
        {
            Console.WriteLine(text);
        }

        public static void Write(string text)
        {
            Print(text);
        }

        public static void Write(int text)
        {
            Write("" + text);
        }

        public static void Write(float text)
        {
            Write("" + text);
        }

        public static void InfoPrintErrorLog(string text)
        {
            errorLog.Append(DateTime.Now.ToString() + ": " + text + Environment.NewLine);
        }
        public static void Error(string errText)
        {
            errorLog.Append(DateTime.Now.ToString() + ": " + errText + Environment.NewLine);
            Console.WriteLine(errText);
        }

        public static string GetErrorLog()
        {
            return errorLog.ToString();
        }

        public static void ClearErrorLog()
        {
            errorLog.Clear();
        }

        public static void ThrowException(string text)
        {
            errorLog.Append(DateTime.Now.ToString() + ": Fatal exception: " + text + Environment.NewLine);
            throw new Exception(text);
        }
    }
}
