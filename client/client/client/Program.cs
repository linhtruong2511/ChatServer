using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client.gui;
namespace client
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        [STAThread]
        static async Task Main(string[] args)
        {
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Gui maingui = new Gui();
            maingui.Hide();
            Application.Run(new AuthenticationPortal(maingui));
        }
    }
}
