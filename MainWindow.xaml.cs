using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiscordRpcDemo;
using System.Windows.Threading;
using System.Threading;
namespace infinity_rpc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(WaitingEvent);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }
        public void WaitingEvent(object Source, EventArgs e) {
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("", ref this.handlers, true, null);
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("", ref this.handlers, true, null);
            //This is the active process
            String process = GetActiveWindowTitle();
            // We want to know the name of application here
            String result = process.Substring(process.LastIndexOf('-') + 1);

            String[] resultArr = { "Google Chrome", "Explorer", "Edge" };
            if (result == "Google Chrome") {
                this.presence.details = $"Browsing {process}";
                this.presence.state = $"Playing{result}";
                this.presence.largeImageKey = "google";                
            }
            if (result == process)
            {
                result = "Explorer";
                this.presence.details = $"Browsing Folder {process}";
                this.presence.state = "Playing Explorer";
                this.presence.largeImageKey = "explorer";
            }
            this.presence.smallImageKey = "infinity-rpc-1";
            if (resultArr.Contains(result))
            {
                DiscordRpc.UpdatePresence(ref this.presence);
            }
            
        }
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            String Title = GetActiveWindowTitle();
            MessageBox.Show($"{Title}");
        }
    }

}
