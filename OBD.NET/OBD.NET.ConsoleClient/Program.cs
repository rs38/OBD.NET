using System;
using System.Threading;
using System.Threading.Tasks;
using OBD.NET.Common.Devices;
using OBD.NET.Common.Extensions;
using OBD.NET.Common.Logging;
using OBD.NET.Common.OBDData;
using OBD.NET.Desktop.Communication;
using OBD.NET.Desktop.Logging;

namespace OBD.NET.ConsoleClient
{
    public class Program
    {
        

        public static async Task Main(string[] args)
        {
            string comPort = "COM9";

            using (SerialConnection connection = new SerialConnection(comPort))

               

            using (ELM327 dev = new ELM327(connection, new OBDConsoleLogger(OBDLogLevel.Data)))
            {
                //dev.SubscribeDataReceived<EngineRPM>((sender, data) => Console.WriteLine("EngineRPM: " + data.Data.Rpm));
                //dev.SubscribeDataReceived<VehicleSpeed>((sender, data) => Console.WriteLine("VehicleSpeed: " + data.Data.Speed));
                //dev.SubscribeDataReceived<IOBDData>((sender, data) => Console.WriteLine($"PID {data.Data.PID.ToHexString()}: {data.Data}"));

                dev.Initialize();
                Thread.Sleep(500);


                dev.SendATCommand("AT SP7");  // Set protokoll to 7 - ISO 15765-4 CAN (29 bit ID, 500 kbaud)
                dev.SendATCommand("AT BI");  // Bypass the initialization sequence to make sure it stays at protokoll 7  //     dev.SendATCommand("AT CAF0"); // Turns off CAN automatic formating so that PCI bytes are not inserted in the messages, or expected in the response
                                             //  dev.SendATCommand( "AT L0");// Linefeeds off
                dev.SendATCommand("AT DP");  // xxxxxxxxx
                dev.SendATCommand("AT ST16");
                dev.SendATCommand("atcp17");
                Thread.Sleep(200);
                dev.SendATCommand("ATSH17FC007B");
                Thread.Sleep(200);
                dev.SendATCommand("atcra17fe007b");

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1000);
                    var data =  dev.RequestDataAsync("1802").Result;
                  
                    //dev.RequestData("1801"); //benutzt den 0x22 ReadByIdentifier
                    
                }
                Console.ReadLine();
            }

      
      

            //Async example
            //MainAsync(comPort).Wait();

            //Console.ReadLine();
        }

    
      
        public static async Task MainAsync(string comPort)
        {
            using (SerialConnection connection = new SerialConnection(comPort))
            using (ELM327 dev = new ELM327(connection, new OBDConsoleLogger(OBDLogLevel.Debug)))
            {
                dev.Initialize();
                EngineRPM data = await dev.RequestDataAsync<EngineRPM>();
                Console.WriteLine("Data: " + data.Rpm);
                data = await dev.RequestDataAsync<EngineRPM>();
                Console.WriteLine("Data: " + data.Rpm);
                VehicleSpeed data2 = await dev.RequestDataAsync<VehicleSpeed>();
                Console.WriteLine("Data: " + data2.Speed);
                data = await dev.RequestDataAsync<EngineRPM>();
                Console.WriteLine("Data: " + data.Rpm);
            }
        }
    }
}
