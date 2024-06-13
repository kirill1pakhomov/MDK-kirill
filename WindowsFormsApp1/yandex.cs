using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class yandex : Form
    {
        private bool isPageLoading = false;
        public yandex()
        {
            InitializeComponent();
            InitializeChromium();
            this.Load += MainForm_Load;

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
            StartAuthentication();
        }

        private void StartAuthentication()
        {
            string clientId = "c65207649d9e4dad9813b023f4edb212";
            string redirectUri = "https://disk.yandex.ru/client/disk";
            string authUrl = $"https://oauth.yandex.ru/authorize?response_type=token&client_id={clientId}&redirect_uri={redirectUri}";
            chromiumWebBrowser1.Load(authUrl);
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Url.StartsWith("https://oauth.yandex.ru/verification_code"))
            {
                string token = ExtractAccessToken(e.Url);
                if (!string.IsNullOrEmpty(token))
                {
                    LoadYandexDisk(token);
                }
            }
        }

        private string ExtractAccessToken(string url)
        {
            var tokenIndex = url.IndexOf("access_token=");
            if (tokenIndex != -1)
            {
                var token = url.Substring(tokenIndex + "access_token=".Length);
                return token.Split('&')[0]; // Разделяйте дополнительные параметры
            }
            return string.Empty;
        }

        private void LoadYandexDisk(string accessToken)
        {
            string apiRequest = "https://cloud-api.yandex.net/v1/disk/resources/files?oauth_token=" + accessToken;
            chromiumWebBrowser1.Load(apiRequest);
        }


        private void chromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            isPageLoading = e.IsLoading;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void chromiumWebBrowser1_LoadingStateChanged_1(object sender, LoadingStateChangedEventArgs e)
        {

        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            
                Cef.Shutdown();
              
        }


        private void yandex_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
