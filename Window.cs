using System;
using System.Linq;
using Gtk;

/*

**************** Window (window) *****************
*                                                *
*  ************** VBox (box) ******************  *
*  *                                          *  *
*  *  ComboBox (dropdown)                     *  *
*  *                                          *  *
*  *  **** HBox (box-controlls) ************  *  *
*  *  *                                    *  *  *
*  *  *  Button (mute)    HScale (slider)  *  *  *
*  *  *                                    *  *  *
*  *  **************************************  *  *
*  *                                          *  *
*  ********************************************  *
*                                                *
**************************************************

*/

public static class Window 
{
	private static Gtk.ComboBoxText dropdown;
	private static Gtk.Button mute;
	private static Gtk.HScale slider;
	public static void Update() 
	{
		dropdown.RemoveAll();
		mute.Label = Program.Mute ? Config.VolumeMute : Config.VolumeUnmute;
		slider.Value = Program.Volume;

		Sink[] sinks = Program.GetAllSinks();
		string defaultSink = Program.DefaultSink;

		for (int i = 0; i < sinks.Length; i++) 
		{
			dropdown.AppendText(sinks[i].FriendlyName);
			if (defaultSink == sinks[i].SystemName) { dropdown.Active = i; }
		}
	}

	public static void Run() 
	{
		Application.Init();

		CssProvider provider = new CssProvider();
		provider.LoadFromData(Config.Css);
		StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);

		Gtk.Window window = new Gtk.Window("sound-menu");
		window.Name = "window";
		Gtk.VBox box = new Gtk.VBox(false, 0);
		box.Name = "box";
		dropdown = new Gtk.ComboBoxText();
		dropdown.Name = "dropdown";
		Gtk.HBox box_controlls = new Gtk.HBox(false, 0);
		box_controlls.Name = "box-controlls";
		mute = new Gtk.Button("");
		mute.Name = "mute";
		slider = new Gtk.HScale(0, 100, 5);
		slider.Name = "slider";

		window.Add(box);
		box.PackStart(dropdown, false, false, 0);
		box.PackStart(box_controlls, false, false, 0);
		box_controlls.PackStart(mute, false, false, 0);
		box_controlls.PackStart(slider, true, true, 0);

		dropdown.Changed += (object sender, EventArgs e) => Console.WriteLine(dropdown.ActiveText);

		mute.WidthRequest = 10;
		mute.HeightRequest = 10;
		mute.StyleContext.AddClass("circular");
		mute.Clicked += (object sender, EventArgs e) => 
		{
			bool isMute = !Program.Mute;
			Program.Mute = isMute;
			mute.Label = isMute ? Config.VolumeMute : Config.VolumeUnmute;
		};

		slider.DrawValue = true;
		slider.ShowFillLevel = true;
		slider.ValuePos = PositionType.Right;
		slider.FormatValue += (object sender, FormatValueArgs e) => e.RetVal = e.Value.ToString() + "%";
		slider.ValueChanged += (object sender, EventArgs e) => Program.Volume = (int)slider.Value;

		LayerShell.InitWindow(window);
		LayerShell.SetLayer(window, LayerShell.Layer.Overlay);
		LayerShell.SetKeyboardInteractivity(window, true);
		LayerShell.SetKeyboardMode(window, LayerShell.KeyboardMode.None);
		LayerShell.SetMargin(window, LayerShell.Edge.Top, Config.WindowMarginTop);
		LayerShell.SetMargin(window, LayerShell.Edge.Right, Config.WindowMarginRight);
		LayerShell.SetMargin(window, LayerShell.Edge.Bottom, Config.WindowMarginBottom);
		LayerShell.SetMargin(window, LayerShell.Edge.Left, Config.WindowMarginLeft);
		for (int i = 0; Config.WindowAnchor != null && i < Config.WindowAnchor.Length; i++) { LayerShell.SetAnchor(window, Config.WindowAnchor[i], true); }

		window.Resizable = false;
		window.KeepAbove = true;
		window.FocusOutEvent += (object sender, FocusOutEventArgs e) => Application.Quit();
		window.SetDefaultSize(Config.WindowWidth, Config.WindowHeight);
		window.ShowAll();

		Window.Update();

		Application.Run();
	}
}