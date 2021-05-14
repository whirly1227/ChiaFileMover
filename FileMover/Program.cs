using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace FileMover
{
	class Program
	{
		static string SourceDirectory = "G:\\";
		static string MoveToDirectory = "I:\\";
		static int DelayTime = 10;
		//static int FileMoveTime = 1;
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				SourceDirectory = args[0];
				if (args.Length > 1) MoveToDirectory = args[1];
				if (args.Length > 2) DelayTime = int.Parse(args[2]);
				//if (args.Length > 3) FileMoveTime = int.Parse(args[3]);
			}

			Timer timer = new();
			timer.Elapsed += Timer_Elapsed;
			timer.Interval = DelayTime * 60000;
			timer.AutoReset = true;
#if DEBUG
			timer.Interval = 1000;
			timer.AutoReset = false;
#endif
			Task.Run(() => timer.Start());
			Console.WriteLine("Files will be checked every " + DelayTime + " minutes");
			Console.ReadLine();


			Console.WriteLine("Press Enter to Close");
			Console.ReadLine();
		}

		private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			foreach (string file in Directory.GetFiles(SourceDirectory))
			{
				FileInfo fileInfo = new(file);
				if (fileInfo.Extension != ".plot") continue;
				File.Move(file, MoveToDirectory + fileInfo.Name); // Moved this line because only '.plot' files are being moved.

				//if (File.GetCreationTime(file).AddHours(FileMoveTime) < DateTime.Now)
				//{
				//	File.Move(file, MoveToDirectory + fileInfo.Name);
				//}
			}
		}
	}
}
