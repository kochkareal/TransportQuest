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
        public FirmsPanel()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
