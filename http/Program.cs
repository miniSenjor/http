using System;
using System.Net;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        // Создаем экземпляр HttpListener
        HttpListener listener = new HttpListener();

        // Устанавливаем URI, к которому будет подключаться сервер
        listener.Prefixes.Add("http://localhost:8080/");

        // START THE LISTENER
        listener.Start();
        Console.WriteLine("Сервер запущен. Ожидание запросов...");

        while (true)
        {
            // Проверка на наличие входящего запроса
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // Вывод информации о запросе в консоль
            Console.WriteLine($"Получен запрос: {request.HttpMethod} {request.Url}");

            // Подготовка ответа
            string responseString = "<html><body><h1>Hello, World!</h1></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            // Отправка ответа клиенту
            using (var output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }

            // Закрытие ответа
            response.Close();
        }
    }
}
