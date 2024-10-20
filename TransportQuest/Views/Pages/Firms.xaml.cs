using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TransportQuest.Views.Panels;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using TransportQuest.Models;
using TransportQuest.Services;
using System.Collections.Generic;

namespace TransportQuest.Views.Pages
{

    /// <summary>
    /// Логика взаимодействия для Firms.xaml
    /// </summary>
    public partial class Firms : Page
    {
        private List<Firm> _firmsList;
        public Firms(List<Firm> firmsList)
        {
            InitializeComponent();
            _firmsList= firmsList;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var openPanel = new FirmsPanel();
            openPanel.ShowDialog();
            if (openPanel.accept)
            {
                Firm newFirm = new Firm()
                {
                    Name = openPanel.FirmName.Text,
                    Coord = openPanel.FirmCoord.Text,
                    Param1 = openPanel.FirmParam1.Text,
                    Param2 = openPanel.FirmParam2.Text,
                    Param3 = openPanel.FirmParam3.Text
                };

                AddFirmItem(newFirm);

            }
        }

        // Метод для создания новой фирмы
        private void AddFirmItem(Firm Firm) {
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
                Text = Firm.Name + "\n" + Firm.Coord + "\n" + Firm.Param1 + "\n" + Firm.Param2 + "\n" + Firm.Param3,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center

            };
            // Создаем контекстное меню
            ContextMenu contextMenu = new ContextMenu();

            // Пункт "Редактировать"
            MenuItem editItem = new MenuItem { Header = "Редактировать" };
            editItem.Click += (s, args) => EditFirm(Firm, grid);

            // Пункт "Удалить"
            MenuItem deleteItem = new MenuItem { Header = "Удалить" };
            deleteItem.Click += (s, args) => DeleteFirm(grid, Firm.Name, Firm.Coord); // Удаляем сам Grid

            MenuItem traceMarshrut = new MenuItem { Header = "Рассчитать маршрут" };
            traceMarshrut.Click += (s, args) => TraceMarshrut(Firm, _firmsList); 

            // Добавляем пункты в контекстное меню
            contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(traceMarshrut);

            // Привязываем контекстное меню к TextBlock
            textBlock.ContextMenu = contextMenu;

            // Вставляем TextBlock в Border
            border.Child = textBlock;


            // Добавляем заголовок в контейнер Grid
            grid.Children.Add(border);
            wrapPanel.Children.Add(grid);
            _firmsList.Add(Firm);
        }

        // Метод для редактирования фирмы
        private void EditFirm(Firm firm, Grid grid)
        {
            // Открываем панель редактирования
            var firmsPanel = new FirmsPanel
            {
                FirmName = { Text = firm.Name },
                FirmCoord = { Text = firm.Coord },
                FirmParam1 = { Text = firm.Param1 },
                FirmParam2 = { Text = firm.Param2 },
                FirmParam3 = { Text = firm.Param3 }
            };

            firmsPanel.ShowDialog();

            // Если изменения были приняты
            if (firmsPanel.accept)
            {
                // Обновляем данные фирмы
                firm.Name = firmsPanel.FirmName.Text;
                firm.Coord = firmsPanel.FirmCoord.Text;
                firm.Param1 = firmsPanel.FirmParam1.Text;
                firm.Param2 = firmsPanel.FirmParam2.Text;
                firm.Param3 = firmsPanel.FirmParam3.Text;

                // Обновляем текст в TextBlock
                var textBlock = grid.Children[0] as Border;
                if (textBlock != null && textBlock.Child is TextBlock)
                {
                    var tb = (TextBlock)textBlock.Child;
                    tb.Text = firmsPanel.FirmName.Text + "\n" + firmsPanel.FirmCoord.Text + "\n" + firmsPanel.FirmParam1.Text + "\n" + firmsPanel.FirmParam2.Text + "\n" + firmsPanel.FirmParam3.Text;
                }
            }
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
                        Firm newFirm = new Firm()
                        {
                            Name = worksheet.Cell(currentRow, 1).GetValue<string>(),
                            Coord = worksheet.Cell(currentRow, 6).GetValue<string>(),
                            Param1 = worksheet.Cell(currentRow, 2).GetValue<string>(),
                            Param2 = worksheet.Cell(currentRow, 3).GetValue<string>(),
                            Param3 = worksheet.Cell(currentRow, 5).GetValue<string>()
                        };

                        AddFirmItem(newFirm);

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

        //Метод для расчета маршрута
        private void TraceMarshrut(Firm selectedFirm, List<Firm> firmsList)
        {
            var openPanel1 = new TraceMarshrutPanel();
            // Передаем список фирм в панель
            openPanel1.LoadFirms(firmsList, selectedFirm);

            openPanel1.ShowDialog();
        }
    }
}

