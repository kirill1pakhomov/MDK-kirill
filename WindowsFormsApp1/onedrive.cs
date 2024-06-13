using CefSharp.WinForms;
using CefSharp;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class onedrive : Form
    {
        private bool isPageLoading = false;
        public onedrive()
        {
            InitializeComponent();
            InitializeChromium();
            this.Load += google_Load;
        }

        private void google_Load(object sender, EventArgs e)
        {
            
            chromiumWebBrowser1.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
            StartAuthentication();
        }

        

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Настройте CefSharp
            Cef.Initialize(settings);

            this.Controls.Add(chromiumWebBrowser1);
        }

        private void StartAuthentication()
        {
            string clientId = "5476d398-e659-42fd-b42e-601b57e7a034";
            string redirectUri = "https://onedrive.live.com/";
            string authUrl = $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={clientId}&scope=files.readwrite%20offline_access&response_type=code&redirect_uri={redirectUri}";
            chromiumWebBrowser1.Load(authUrl);
            
        }
        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Url.StartsWith("https://login.microsoftonline.com/common/oauth2/nativeclient"))
            {
                string code = ExtractCodeFromUrl(e.Url);
                if (!string.IsNullOrEmpty(code))
                {
                    GetTokenFromCode(code);
                }
            }
        }

        private string ExtractCodeFromUrl(string url)
        {
            var codeIndex = url.IndexOf("code=");
            if (codeIndex != -1)
            {
                var code = url.Substring(codeIndex + "code=".Length);
                return code.Split('&')[0];
            }
            return string.Empty;
        }
        private void GetTokenFromCode(string accessToken)
        {
            string apiRequest = "https://login.microsoftonline.com/v1/disk/resources/files?oauth_token=" + accessToken;
            chromiumWebBrowser1.Load(apiRequest);
        }

        private void OnFormClosin(object sender, FormClosedEventArgs e)
        {
            
        }

       

        private void onedrive_FormClosing_2(object sender, FormClosingEventArgs e)
        {
            if (isPageLoading)
            {
                e.Cancel = true; // Отменяем закрытие формы
            }
            else
            {
                Cef.Shutdown();
                


            }

        }

       
    }
}

       




 
