using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    private static string protocol = "http";
    private static string host = "localhost";
    private static string port = "8080";

    public static void Main(string[] args)
    {
        HttpListener listener = new HttpListener();

        // Устанавливаем URI, к которому будет подключаться сервер
        listener.Prefixes.Add($"{protocol}://{host}:{port}/");

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

            // Ответ сервера
            string responseString = "<html><body><h1>404 NotFound</h1></body></html>";

            if (request.HttpMethod == "GET")
            {
                // Обработка get запросов

                // Получение пользователя
                if (request.Url.AbsolutePath == "/api/users")
                {
                    string param = request.QueryString["param"];
                    if (param != null)
                    {
                        Console.WriteLine($"Получен параметр: {param}");
                        responseString = $"<html><body><h1>GET api/users: {param}</h1></body></html>";
                    }
                    else
                    {
                        Console.WriteLine("Параметр отсутствует");
                        response.StatusCode = 400; // Bad Request
                    }
                }
                else if (request.Url.AbsolutePath == "/")
                {
                    responseString = "<html><body><h1>Hello, World!</h1></body></html>";
                }
                else
                {
                    response.StatusCode = 404; // Not Found
                }
            }
            else if (request.HttpMethod == "POST")
            {
                // Обработка post запросов

                // Получение пользователя
                if (request.Url.AbsolutePath == "/api/users")
                {
                    string param = "";
                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        param = reader.ReadToEnd();
                    }
                    Console.WriteLine($"Получен параметр: {param}");

                    if (param != "")
                    {
                        Console.WriteLine($"Получен параметр: {param}");
                        responseString = $"<html><body><h1>POST api/users: {param}</h1></body></html>";
                    }
                    else
                    {
                        Console.WriteLine("Параметр отсутствует");
                        response.StatusCode = 400; // Bad Request
                    }
                }
                else
                {
                    response.StatusCode = 404; // Not Found
                }
            } else
            {
                response.StatusCode = 404; // Not Found
            }

            // Подготовка ответа
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
