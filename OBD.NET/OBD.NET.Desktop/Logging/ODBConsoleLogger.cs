using System;
using OBD.NET.Common.Logging;

namespace OBD.NET.Desktop.Logging
{
    /// <summary>
    /// Simple console logger
    /// </summary>
    /// <seealso cref="IOBDLogger" />
    public class OBDConsoleLogger : IOBDLogger
    {
      

        public OBDLogLevel LogLevel { get; set; }



        public OBDConsoleLogger(OBDLogLevel level = OBDLogLevel.None)
        {
            this.LogLevel = level;
        }


        public void WriteLine(string text, OBDLogLevel level)
        {
            if (LogLevel == OBDLogLevel.None) return;

            int i = 0;
            double f = 0;
            if (!(level == OBDLogLevel.Data & text.Length > 1)) return;

            if (text.Substring(0, 2) == "61" | text.Substring(0, 2) == "62")
            {
                text = text.Replace(" ", "");
                var cmd = text.Substring(2, 4);
                if (cmd == "1802") //A
                {
                    var res = text.Substring(6, text.Length - 6); //3 Bytes
                    i = Convert.ToInt32(res, 16);
                    f = (i / 100.0) - 1500;

                }

                if (cmd == "1801") //V
                {
                    var res = text.Substring(6, text.Length - 6); //2 Bytes
                    i = Convert.ToInt32(res, 16);
                    f = (i / 10.0);
                }

                Console.WriteLine($"{DateTime.Now:G};{text};{cmd};{f:00.00}");

            }

            else if (text.Substring(0, 2) == "22")
                return;

            else if             ((int)level <= (int)LogLevel)
                Console.WriteLine($"{DateTime.Now:G} -  {level} -  {text}");
        }

    }
}
