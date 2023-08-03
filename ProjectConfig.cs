using System;

public static partial class Config 
{
	public static int WindowWidth { private set; get; }
	public static int WindowHeight { private set; get; }
	public static int WindowMarginTop { private set; get; }
	public static int WindowMarginRight { private set; get; }
	public static int WindowMarginBottom { private set; get; }
	public static int WindowMarginLeft { private set; get; }
	public static Gtk.LayerShell.Edge[] WindowAnchor { private set; get; }
	public static string VolumeMute { private set; get; }
	public static string VolumeUnmute { private set; get; }
	public static string GetSinksCommand { private set; get; }
	public static string SetDefaultSinkCommand { private set; get; }
	public static string GetDefaultSinkCommand { private set; get; }
	public static string SetVolumeCommand { private set; get; }
	public static string GetVolumeCommand { private set; get; }
	public static string SetMuteCommand { private set; get; }
	public static string GetMuteCommand { private set; get; }
	public static string Css { private set; get; }
	public static string ConfigDir { private set; get; }

	private static readonly string DefaultConfig = "WindowMarginTop=0\nWindowMarginRight=0\nWindowMarginBottom=0\nWindowMarginLeft=0\nWindowAnchor[]=Top,Right";
	private static readonly string DefaultCss = "";
	private static readonly string DefaultConfigDir = Environment.GetEnvironmentVariable("HOME") + "/.config/sound-menu";

	private static readonly Option[] OptionsDefinition = 
	{
		new Option("--config-dir", 'c', true, null),
	};
}