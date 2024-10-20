using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TransportQuest.Views.Pages;
using TransportQuest.Models;
using System.Linq;

namespace TransportQuest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page pageFirms;
        private Page pageCars;
        private Page pageWays;
        private Page pageSettings;
        List<Firm> FirmsList = new List<Firm>();
        public MainWindow()
        {
            InitializeComponent();
            pageFirms = new Firms(FirmsList);
            pageCars = new Cars();
            pageWays = new Ways();
            pageSettings = new Settings();
            MainFrame.Content = pageFirms;
        }
        private void ChangePage_Click(object sender, RoutedEventArgs e)

        {
            var button = sender as Button;
            var buttonName = button.Name.ToString();
            switch (buttonName)
            {
                case "FirmsBtn": MainFrame.NavigationService.Navigate(pageFirms); break;
                case "CarsBtn": MainFrame.NavigationService.Navigate(pageCars); break;
                case "WaysBtn": MainFrame.NavigationService.Navigate(pageWays); break;
                case "SettingsBtn":MessageBox.Show(FirmsList.Count.ToString()); break; //MainFrame.NavigationService.Navigate(pageSettings); break;
            }
        }
    }

}
