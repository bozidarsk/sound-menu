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

	private static readonly string DefaultConfig = "WindowWidth=300\nWindowHeight=50\nWindowMarginTop=10\nWindowMarginRight=10\nWindowMarginBottom=0\nWindowMarginLeft=0\nWindowAnchor[]=Top,Right\nVolumeMute=\nVolumeUnmute=\n\n# format must be 'SYSTEMNAME\\x1dFRIENDLYNAME\\n...'\nGetSinksCommand=pactl --format=json list sinks | sed -E 's/index/\\n/g' | grep name | sed -E 's/\\\":[0-9]+,\\\"state\\\":\\\"[^\\\"]+\\\",\\\"name\\\":\\\"([^\\\"]+)\\\",\\\"description\\\":\\\"([^\\\"]+)\\\".+/\\1\\x1d\\2/'\n\nSetDefaultSinkCommand=pactl set-default-sink '%value%'\nGetDefaultSinkCommand=pactl get-default-sink | tr -d '\\n'\n\nSetVolumeCommand=pactl set-sink-volume @DEFAULT_SINK@ '%value%'%\nGetVolumeCommand=pactl get-sink-volume @DEFAULT_SINK@ | tr -d '\\n' | sed -E 's/.+\\s+([0-9]+)%.+/\\1/'\n\nSetMuteCommand=pactl set-sink-mute @DEFAULT_SINK@ '%value%'\nGetMuteCommand=pactl get-sink-mute @DEFAULT_SINK@ | sed -E 's/Mute: yes/true/' | sed -E 's/Mute: no/false/'";
	private static readonly string DefaultCss = "* {\n    font-family: \"Source Code Pro Bold\";\n    font-weight: bold;\n    transition: 0.2s;\n}\n\n#window {\n    border: 2px solid @theme_selected_bg_color;\n    border-radius: 8px;\n    background-color: @theme_base_color;\n}\n\n#box {\n    padding: 10px;\n}\n\n#dropdown {\n}\n\n#dropdown cellview {\n    margin-top: 2px;\n}\n\n#box-controlls {\n    margin-top: 5px;\n}\n\n#mute {\n    margin-right: 5px;\n}\n\n#mute label {\n    font-family: \"Font Awesome 6 Sharp\";\n    font-size: 15px;\n}\n\n#slider value {\n    margin-left: 5px;\n    color: @theme_text_color;\n}";
	private static readonly string DefaultConfigDir = Environment.GetEnvironmentVariable("HOME") + "/.config/sound-menu";

	private static readonly Option[] OptionsDefinition = 
	{
		new Option("--config-dir", 'c', true, null),
	};
}