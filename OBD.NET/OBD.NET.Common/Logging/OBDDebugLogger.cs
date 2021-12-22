using System;
using System.Diagnostics;

namespace OBD.NET.Common.Logging
{
  
    public class OBDDebugLogger : IOBDLogger
    {
        public OBDLogLevel LogLevel { get; set; }


        public OBDDebugLogger(OBDLogLevel level = OBDLogLevel.None)
        {
            this.LogLevel = level;
        }

        public void WriteLine(string text, OBDLogLevel level)
        {
            if (LogLevel == OBDLogLevel.None) return;

            if(LogLevel == OBDLogLevel.Data)
                if (text.Substring(0,2)=="22" | text.Substring(0, 2) == "62")
                {
                    
                    Debug.WriteLine($"{DateTime.Now:G}; {text};{text.Substring(2,text.Length-2)}");

                }

            if ((int)level <= (int)LogLevel)
                Debug.WriteLine($"{DateTime.Now:G} -  {level} -  {text}");
        }

      
    }
}
