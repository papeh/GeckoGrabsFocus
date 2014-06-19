using System;
using System.IO;
using System.Windows.Forms;
using Gecko;

namespace GeckoGrabsFocus
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			// Initialize XULRunner - required to use the geckofx WebBrowser Control (GeckoWebBrowser)
			string xulRunnerLocation;
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				xulRunnerLocation = XULRunnerLocator.GetXULRunnerLocation();
				if (String.IsNullOrEmpty(xulRunnerLocation))
					throw new ApplicationException("The XULRunner library is missing or has the wrong version");
				string librarySearchPath = Environment.GetEnvironmentVariable("LD_LIBRARY_PATH") ?? String.Empty;
				if (!librarySearchPath.Contains(xulRunnerLocation))
					throw new ApplicationException("LD_LIBRARY_PATH must contain " + xulRunnerLocation);
			}
			else
			{
				xulRunnerLocation = Path.GetFullPath("xulrunner");
				if (!Directory.Exists(xulRunnerLocation))
					throw new ApplicationException("XULRunner needs to be installed to " + xulRunnerLocation);
			}

			Xpcom.Initialize(xulRunnerLocation);
			GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;

			InitializeComponent();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			webBrowser1.LoadContent(textBox1.Text, "file:///c:/MayNotExist/doesnotmatter.html", "application/xhtml+xml");
			webBrowser1.Refresh();
		}
	}
}
