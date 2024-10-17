using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TransportQuest;

namespace TransportQuest.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        public Cars()
        {
            InitializeComponent();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
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
                Text = wrapPanel.Children.Count + 1 + ".",
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
