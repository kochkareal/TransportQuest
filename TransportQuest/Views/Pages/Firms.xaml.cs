using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TransportQuest.Views.Panels;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using TransportQuest;

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
            AddFirmItem(openPanel.FirmName.Text, openPanel.FirmCoord.Text, openPanel.FirmParam1.Text, openPanel.FirmParam2.Text, openPanel.FirmParam3.Text);
        }

        private void AddFirmItem(string FirmName, string FirmCoord, string FirmParam1, string FirmParam2, string FirmParam3) {
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
                Text = FirmName + "\n" + FirmCoord + "\n" + FirmParam1 + "\n" + FirmParam2 + "\n" + FirmParam3,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center

            };
            // Создаем контекстное меню
            ContextMenu contextMenu = new ContextMenu();

            // Пункт "Редактировать"
            MenuItem editItem = new MenuItem { Header = "Редактировать" };
            editItem.Click += (s, args) => EditFirm(FirmName);

            // Пункт "Удалить"
            MenuItem deleteItem = new MenuItem { Header = "Удалить" };
            deleteItem.Click += (s, args) => DeleteFirm(grid, FirmName, FirmCoord); // Удаляем сам Grid

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
        private void DeleteFirm(Grid grid, string firmName, string firmCoord)
        {
            wrapPanel.Children.Remove(grid); // Удаляем Grid из WrapPanel
            MessageBox.Show($"Фирма {firmName} удалена.");
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            // Открытие диалога выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xlsm" // Фильтр для Excel файлов
            };

            // Если пользователь выбрал файл и нажал ОК
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Открываем и читаем Excel файл
                ReadExcelFile(filePath);
            }
        }

        // Метод для работы с Excel файлом (чтение и работа с ячейками)
        private void ReadExcelFile(string filePath)
        {
            try
            {
                // Открываем Excel файл
                using (var workbook = new XLWorkbook(filePath))
                {
                    // Открываем первый лист (или любой другой по индексу или имени)
                    var worksheet = workbook.Worksheet(1); // Лист с индексом 1

                    int currentRow = 2; // Номер текущей строки для проверки

                    // Цикл по строкам, пока строка не пустая
                    while (!worksheet.Row(currentRow).IsEmpty())
                    {
                        // Читаем значение из ячейки A текущей строки
                        var cellValue1 = worksheet.Cell(currentRow, 1).GetValue<string>();
                        var cellValue2 = worksheet.Cell(currentRow, 6).GetValue<string>();
                        var cellValue3 = worksheet.Cell(currentRow, 2).GetValue<string>();
                        var cellValue4 = worksheet.Cell(currentRow, 3).GetValue<string>();
                        var cellValue5 = worksheet.Cell(currentRow, 5).GetValue<string>();

                        AddFirmItem(cellValue1, cellValue2, cellValue3, cellValue4, cellValue5);

                        // Логика обработки строки

                        currentRow++; // Переход на следующую строку
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при работе с файлом Excel: {ex.Message}");
            }
        }
    }
}

