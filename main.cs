using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class Program 
{
	public static int Volume 
	{
		set => Program.Shell(Config.SetVolumeCommand.Replace("%value%", value.ToString()));
		get => int.Parse(Program.Shell(Config.GetVolumeCommand));
	}

	public static bool Mute 
	{
		set => Program.Shell(Config.SetMuteCommand.Replace("%value%", value.ToString()));
		get => bool.Parse(Program.Shell(Config.GetMuteCommand));
	}

	public static string DefaultSink 
	{
		set => Program.Shell(Config.SetDefaultSinkCommand.Replace("%value%", value));
		get => Program.Shell(Config.GetDefaultSinkCommand);
	}

	public static Sink[] GetAllSinks() => 
		Program.Shell(Config.GetSinksCommand)
		.Split('\n')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => new Sink(x.Split('\x1d')))
		.ToArray()
	;

	private static readonly string SHELL = Environment.GetEnvironmentVariable("SHELL");
	public static string Shell(string command) => Shell(command, out int code);
	public static string Shell(string command, out int code)
	{
		ProcessStartInfo info = new ProcessStartInfo();
		info.FileName = SHELL;
		info.Arguments = $"-c \"{command}\"";
		info.UseShellExecute = false;
		info.RedirectStandardOutput = true;
		info.RedirectStandardError = false;

		Process proc = new Process();
		proc.StartInfo = info;
		proc.Start();
		proc.WaitForExit();
		code = proc.ExitCode;

		return proc.StandardOutput.ReadToEnd();
	}

	private static int Main(string[] args) 
	{
		if (args.Length == 0 || (args.Length == 1 && (args[0] == "-h" || args[0] == "--help" || args[0] == "help")))
		{
			Console.WriteLine("Usage:\n\tsound-menu <command> [options]");
			Console.WriteLine("\nCommands:");
			Console.WriteLine("\twindow                     Opens a gtk3 gui window.");
			Console.WriteLine("\tdefaults                   Creates default configuration and style files.");
			Console.WriteLine("\nOptions:");
			Console.WriteLine("\t--config-dir <dir>         Directory which contains configuration and style files.");
			return 0;
		}

		Config.Initialize(ref args);
		if (args.Length == 0) { Console.WriteLine("No commands provided."); return 1; }

		switch (args[0]) 
		{
			case "window":
				Window.Run();
				break;
			case "defaults":
				Config.CreateDefaults();
				break;
			default:
				Console.WriteLine("Unknown command.");
				return 1;
		}

		return 0;
	}
}