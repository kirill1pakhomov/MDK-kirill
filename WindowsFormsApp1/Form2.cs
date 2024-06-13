using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void OpenForm<T>() where T : Form, new()
        {
            FormFactory.CreateForm<T>().ShowDialog();
            this.Close();
        }

        private void yandex_Click(object sender, EventArgs e)
        {
            OpenForm<yandex>();
        }

        private void mailru_Click(object sender, EventArgs e)
        {
            OpenForm<dropbox>();
        }

        private void google_Click(object sender, EventArgs e)
        {
            OpenForm<onedrive>();
        }

        private void cloudmailru_Click(object sender, EventArgs e)
        {
            OpenForm<mailru>();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }

    public static class FormFactory
    {
        public static T CreateForm<T>() where T : Form, new()
        {
           
            return new T();
        }
    }
}
