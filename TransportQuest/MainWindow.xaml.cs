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
        Page pageFirms;
        Page pageCars;
        Page pageWays;
        Page pageSettings;
        public MainWindow()
        {
            InitializeComponent();
            pageFirms = new Firms();
            pageCars = new Cars();
            pageWays = new Ways();
            pageSettings = new Settings();
            MainFrame.Content = pageFirms;
        }

        private void  ChangePage_Click(object sender, RoutedEventArgs e)

        {
            var button = sender as Button;
            var buttonName = button.Name.ToString();
            switch (buttonName)
            {
                case "Firms": MainFrame.NavigationService.Navigate(pageFirms); break;
                case "Cars": MainFrame.NavigationService.Navigate(pageCars); break;
                case "Ways": MainFrame.NavigationService.Navigate(pageWays); break;
                case "Settings": MainFrame.NavigationService.Navigate(pageSettings); break;
            }

        }
    }
}
