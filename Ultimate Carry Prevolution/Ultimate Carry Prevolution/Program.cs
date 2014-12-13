using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;

namespace Ultimate_Carry_Prevolution
{
	class Program
	{

		

		private static Sprite _xSprite;
		private static Texture _xTexture;
		private static bool _donateclicked;

		// ReSharper disable once UnusedParameter.Local
		static void Main(string[] args)
		{
			Events.Game.OnGameStart += OnGameStart;

			_xSprite = new Sprite(Drawing.Direct3DDevice);
			_xTexture = Texture.FromMemory(
		   Drawing.Direct3DDevice,
		   (byte[])new ImageConverter().ConvertTo(GetImageFromUrl("http://counter2.allfreecounter.com/private/freecounterstat.php?c=8feef6050837e39b4982008fbb21c766"), typeof(byte[])), 266, 38, 0,
		   Usage.None, Format.A1, Pool.Managed, Filter.Default, Filter.Default, 0);
			
			Game.OnWndProc += Game_OnWndProc;
			Drawing.OnDraw += Draw_Credits;
			Drawing.OnPreReset += DrawOnPreReset;
			Drawing.OnPostReset += DrawOnPostReset;
			AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
			AppDomain.CurrentDomain.ProcessExit += OnDomainUnload;
			
		}

		private static void Game_OnWndProc(WndEventArgs args)
		{
			if((args.Msg == (uint)WindowsMessages.WM_KEYUP || args.Msg == (uint)WindowsMessages.WM_KEYDOWN) && args.WParam == 32 && !_donateclicked)
			{
				_donateclicked = true;
				Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=SHPHSQ2LNX8BE");
			}
		}
		private static Image GetImageFromUrl(string url)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			var httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
			var stream = httpWebReponse.GetResponseStream();
			// ReSharper disable once AssignNullToNotNullAttribute
			return Image.FromStream(stream);
		}
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			_xSprite.Dispose();
		}

		private static void DrawOnPostReset(EventArgs args)
		{
			_xSprite.OnResetDevice();
		}

		private static void DrawOnPreReset(EventArgs args)
		{
			_xSprite.OnLostDevice();
		}
		private static void Draw_Credits(EventArgs args)
		{
			Drawing.DrawText(10, 10, Color.White, "Ultimate Carry Prevolution");
			Drawing.DrawText(10, 30, Color.White, "Codet and Developed by");
			Drawing.DrawText(10, 50, Color.White, "Lexxes & xSalice");

			Drawing.DrawText(10, 100, Color.White, "How Much time Loded?");

			_xSprite.Begin();
			_xSprite.Draw(_xTexture, new ColorBGRA(255, 255, 255, 255), null, null, new Vector3(10, 120, 0));
			_xSprite.End();

			Drawing.DrawText(10, 200, Color.White, "Current Donation State : 5$");
			Drawing.DrawText(10, 220, Color.White, "Next Goal : 60$");
			Drawing.DrawText(10, 240, Color.White, "Reason: xSalice got Banned and need new");
			Drawing.DrawText(10, 260, Color.White, "Account, lvl 30 all champions");

			Drawing.DrawText(10, 300, Color.White, "Total Amount of Donation : 5$");

			Drawing.DrawText(10, 340, Color.White, "Paypal : team-xslx@hotmail.com");
			Drawing.DrawText(10, 360, Color.White, "Forum: xSLx Signatur - Donate");

			Drawing.DrawText(10, 400, Color.White, "or just click > Space < while in Loadscreen");
		}
		private static void OnGameStart(EventArgs args)
		{
			try
			{
				Game.OnWndProc -= Game_OnWndProc;
				Drawing.OnDraw -= Draw_Credits;
				Drawing.OnDraw -= Draw_Credits;
				Drawing.OnPreReset -= DrawOnPreReset;
				Drawing.OnPostReset -= DrawOnPostReset;
				AppDomain.CurrentDomain.DomainUnload -= OnDomainUnload;
				AppDomain.CurrentDomain.ProcessExit -= OnDomainUnload;
				_xSprite.Dispose();
			}
			catch(Exception)
			{

			}
			LoadUC();
		}

		private static void LoadUC()
		{
			// ReSharper disable once ObjectCreationAsStatement
			new Loader();
		}
	}
}
