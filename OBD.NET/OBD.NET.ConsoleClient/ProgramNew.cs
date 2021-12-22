using System;
using System.Threading;
using System.Threading.Tasks;
using OBD.NET.Common.Devices;
using OBD.NET.Common.Extensions;
using OBD.NET.Common.Logging;
using OBD.NET.Common.OBDData;
using OBD.NET.Desktop.Communication;
using OBD.NET.Common.Communication.EventArgs;
using OBD.NET.Desktop.Logging;
using System.Text;

namespace OBD.NET.ConsoleClient
{
    public class ProgramNew
    {


		public static void Main(string[] args)
		{


			using (SerialConnection conn = new OBD.NET.Desktop.Communication.SerialConnection("COM6"))
			{
				conn.Connect();
				if (conn.IsOpen)
				{
					var cmd = "ATZ";
					var data = Encoding.ASCII.GetBytes(cmd);

					conn.DataReceived += readdata;

					conn.Write(data);

					cmd = "ATDP";
					 data = Encoding.ASCII.GetBytes(cmd);

				}

			}

			Thread.Sleep(4000);
		}

		static void readdata(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data.ToHexString());
		}

		}
	}
