using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TransportQuest.Views.Pages;


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
            pageFirms = new Firms();
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
                case "SettingsBtn": MainFrame.NavigationService.Navigate(pageSettings); break;
            }
        }
    }
    public class Firm
    {
        public string Name { get; set; }
        public string Coord { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
    }
}
