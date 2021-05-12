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
		static int DelayTime = 60000 * 10;
		static int FileMoveTime = 3;
		static void Main(string[] args)
		{
			if (args[0] != null) SourceDirectory = args[0];
			if (args[1] != null) MoveToDirectory = args[1];
			if (args[2] != null) DelayTime = int.Parse(args[2]);
			if (args[3] != null) FileMoveTime = int.Parse(args[3]);

			Timer timer = new();
			timer.Elapsed += Timer_Elapsed;
			timer.Interval = DelayTime;
			timer.AutoReset = true;
			Task.Run(() => timer.Start());
			Console.WriteLine("Files will be checked every 10 minutes");
			Console.ReadLine();


			Console.WriteLine("Press Enter to Close");
			Console.ReadLine();
		}

		private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			foreach (string file in Directory.GetFiles(SourceDirectory))
			{
				FileInfo fileInfo = new(file);

				if (File.GetCreationTime(file).AddHours(FileMoveTime) < DateTime.Now)
				{
					File.Move(file, MoveToDirectory + fileInfo.Name);
				}
			}
		}
	}
}
