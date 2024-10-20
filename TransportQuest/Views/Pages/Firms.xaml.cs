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
                Text = openPanel.FirmName.Text + "\n" + openPanel.FirmCoord.Text + "\n" + openPanel.FirmParam1.Text + "\n" + openPanel.FirmParam2.Text + "\n" + openPanel.FirmParam3.Text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center

            };
            // Создаем контекстное меню
            ContextMenu contextMenu = new ContextMenu();

            // Пункт "Редактировать"
            MenuItem editItem = new MenuItem { Header = "Редактировать" };
            editItem.Click += (s, args) => EditFirm(openPanel.FirmName.Text);

            // Пункт "Удалить"
            MenuItem deleteItem = new MenuItem { Header = "Удалить" };
            deleteItem.Click += (s, args) => DeleteFirm(grid, openPanel.FirmName.Text); // Удаляем сам Grid

            // Добавляем пункты в контекстное меню
            contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);

            // Привязываем контекстное меню к TextBlock
            textBlock.ContextMenu = contextMenu;

            // Вставляем TextBlock в Border
            border.Child = textBlock;


            // Добавляем заголовок в контейнер Grid
            grid.Children.Add(border);
            wrapPanel.Children.Add(grid);
        }

        // Метод для редактирования фирмы
        private void EditFirm(string firmName)
        {
            MessageBox.Show($"Редактировать фирму: {firmName}");
            // Здесь можно открыть панель редактирования или внести изменения
        }

        // Метод для удаления фирмы
        private void DeleteFirm(Grid grid, string firmName)
        {
            wrapPanel.Children.Remove(grid); // Удаляем Grid из WrapPanel
            MessageBox.Show($"Фирма {firmName} удалена.");
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
