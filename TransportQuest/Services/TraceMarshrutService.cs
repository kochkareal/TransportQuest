using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TransportQuest.Services
{
    public class TraceMarshrutService
    {
        private readonly HttpClient _httpClient;

        public TraceMarshrutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double DistanceKm, double DurationMinutes)> GetRouteAsync(string point1, string point2)
        {
            string coord1Lat = point1.Split(',', ' ')[0];
            string coord1Lon = point1.Split(',', ' ')[2];

            string coord2Lat = point2.Split(',', ' ')[0];
            string coord2Lon = point2.Split(',', ' ')[2];
            try
            {
                // Формируем URL для запроса к OSRM API
                string url = $"http://router.project-osrm.org/route/v1/driving/{coord1Lon},{coord1Lat};{coord2Lon},{coord2Lat}?overview=false";

                // Отправляем HTTP-запрос к OSRM API
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Читаем ответ в формате JSON
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Десериализуем JSON-ответ
                var routeResponse = JsonConvert.DeserializeObject<RouteResponse>(jsonResponse);

                if (routeResponse != null && routeResponse.Routes.Count > 0)
                {
                    // Извлекаем данные о расстоянии и времени
                    double distanceMeters = routeResponse.Routes[0].Distance;
                    double durationSeconds = routeResponse.Routes[0].Duration;

                    // Преобразуем в километры и минуты
                    double distanceKm = distanceMeters / 1000.0;
                    double durationMinutes = durationSeconds / 60.0;

                    return (distanceKm, durationMinutes);
                }
                else
                {
                    throw new Exception("Не удалось получить маршрут.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении маршрута: {ex.Message}");
            }
        }

        // Классы для десериализации JSON ответа
        public class RouteResponse
        {
            public List<Route> Routes { get; set; }
        }

        public class Route
        {
            public double Distance { get; set; }  // Расстояние в метрах
            public double Duration { get; set; }  // Время в секундах
        }
    }
}
