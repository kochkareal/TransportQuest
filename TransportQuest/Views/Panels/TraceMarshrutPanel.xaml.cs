using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TransportQuest.Models;
using TransportQuest.Services;

namespace TransportQuest.Views.Panels
{
    /// <summary>
    /// Логика взаимодействия для TraceMarshrutPanel.xaml
    /// </summary>
    public partial class TraceMarshrutPanel : Window
    {
        public TraceMarshrutPanel()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            AcceptBTN.IsEnabled = false;
            LoadingProgressBar.IsIndeterminate = true;

            // Получаем выбранные фирмы
            Firm firm1 = (Firm)point1.SelectedItem;
            Firm firm2 = (Firm)point2.SelectedItem;

            if (firm1 != null && firm2 != null)
            {
                // Получаем координаты фирм
                string coordsFirm1 = firm1.Coord;  // Координаты первой фирмы
                string coordsFirm2 = firm2.Coord;  // Координаты второй фирмы
                // Создаем экземпляр сервиса, если он не был создан ранее
                var httpClient = new HttpClient(); // Убедитесь, что у вас есть подходящий HttpClient
                var traceMarshrutService = new TraceMarshrutService(httpClient);

                try
                {
                    // Вызов метода для получения маршрута
                    var (distanceKm1, durationMinutes1) = await traceMarshrutService.GetRouteAsync(coordsFirm1, coordsFirm2);

                    // Выводим результат
                    MessageBox.Show($"Расстояние: {distanceKm1:F2} км\nВремя: {durationMinutes1:F2} минут", "Длительность маршрута");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка");
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите обе точки для расчета маршрута.");
            }

            LoadingProgressBar.IsIndeterminate = false;
            AcceptBTN.IsEnabled = true;
        }


        // Метод для загрузки фирм в ComboBox
        public void LoadFirms(List<Firm> firmsList, Firm selectedFirm)
        {
            // Заполняем ComboBox для Точки 1 и Точки 2
            point1.ItemsSource = firmsList;
            point2.ItemsSource = firmsList;

            // Устанавливаем отображаемый текст для ComboBox
            point1.DisplayMemberPath = "Name";
            point2.DisplayMemberPath = "Name";

            // Выбираем выбранную фирму как точку 1
            point1.SelectedItem = selectedFirm;
        }

    }
}
