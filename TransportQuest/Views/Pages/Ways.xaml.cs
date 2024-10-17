using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransportQuest.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Ways.xaml
    /// </summary>
    public partial class Ways : Page
    {
        public Ways()
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
