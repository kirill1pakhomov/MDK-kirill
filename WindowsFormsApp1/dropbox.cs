using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;
using CefSharp.WinForms;
using CefSharp;
using System;

namespace WindowsFormsApp1
{
    public partial class dropbox : Form
    {
        private bool isPageLoading = false;
        public dropbox()
        {
            InitializeComponent();
            InitializeChromium();
            this.Load += MainForm_Load;
        }

        private void mailform_Load(object sender, EventArgs e)
        {
            
        }

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Настройте CefSharp
            Cef.Initialize(settings);

            this.Controls.Add(chromiumWebBrowser1);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            chromiumWebBrowser1.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
           
            StartDropboxAuthentication(); // Начать с авторизации Dropbox
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Url.StartsWith("https://www.dropbox.com") && e.Url.Contains("code="))
            {
                string code = ExtractCodeFromUrl(e.Url);
                LoadDropboxHome(code);
            }
        }
        private void StartDropboxAuthentication()
        {
            string clientId = "cxt3ocu89xnxyfb";
            string redirectUri = "https://www.dropbox.com/home";
            string authUrl = $"https://www.dropbox.com/oauth2/authorize?response_type=code&client_id={clientId}&redirect_uri={redirectUri}";
            chromiumWebBrowser1.Load(authUrl);
        }



        private string ExtractCodeFromUrl(string url)
        {
            var codeIndex = url.IndexOf("code=");
            if (codeIndex != -1)
            {
                var code = url.Substring(codeIndex + "code=".Length);
                return code;
            }
            return string.Empty;
        }

        private void LoadDropboxHome(string code)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isPageLoading)
            {
                e.Cancel = true; 
            }
            else
            {
                Cef.Shutdown();
                


            }
        }
    }

}