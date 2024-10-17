using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TransportQuest.Views.Panels;

namespace TransportQuest.Views.Pages
{

    /// <summary>
    /// Логика взаимодействия для Firms.xaml
    /// </summary>
    public partial class Firms : Page
    {
        public Firms()
        {
            InitializeComponent();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var openPanel = new FirmsPanel();
            openPanel.ShowDialog();
                
            // Создаем контейнер Grid
            Grid grid = new Grid
            {
                Margin = new Thickness(10)
            };

            Border border = new Border
            {
                Background = Brushes.DarkCyan,     // Фоновый цвет
                Padding = new Thickness(10),        // внутренний отступ
                CornerRadius = new CornerRadius(10)
            }; // скругление


            // Добавляем заголовок внутри блока
            TextBlock textBlock = new TextBlock
            {
                FontSize = 17,
                Foreground = Brushes.White,
                Text = openPanel.FirmName.Text + "\n" + openPanel.FirmCoord.Text + "\n" + openPanel.FirmValues.Text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            border.Child = textBlock;

            // Добавляем заголовок в контейнер Grid
            grid.Children.Add(border);
            wrapPanel.Children.Add(grid);
        }
    }
}
