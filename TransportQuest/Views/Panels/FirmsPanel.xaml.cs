using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using TransportQuest.Views.Pages;

namespace TransportQuest.Views.Panels
{
    /// <summary>
    /// Логика взаимодействия для FirmsPanel.xaml
    /// </summary>
    public partial class FirmsPanel : Window
    {
        public bool accept;
        public FirmsPanel()
        {
            InitializeComponent();
            accept = false;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            accept = false;
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (FirmName.Text != string.Empty
                && FirmCoord.Text != string.Empty
                && FirmParam1.Text != string.Empty
                && FirmParam2.Text != string.Empty
                && FirmParam3.Text != string.Empty) { accept = true; this.Close(); }
            else { MessageBox.Show("Заполните все поля"); }

        }
    }
}
