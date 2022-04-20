using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;

namespace OpenWeatherMapExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            Console.WriteLine("Welcome to the OpenWeatherMap weather center.");
            Console.WriteLine();

            //Console.WriteLine("To retrieve your current weather status, enter your API key: ");
            //var key = Console.ReadLine();
            //Console.WriteLine();

            var key = File.ReadAllText("appsettings.json");
            var api = JObject.Parse(key).GetValue("DefaultKey").ToString();

            while (true)
            {
                Console.WriteLine("Enter the city: ");
                var city = Console.ReadLine();
                Console.WriteLine($"Here are the current conditions in {city}: ");

                var wxURL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api}&units=imperial";

                var response = client.GetStringAsync(wxURL).Result;
                var formattedResponse = JObject.Parse(response).GetValue("main").ToString();
                Console.WriteLine(formattedResponse);

                Console.WriteLine();
                Console.WriteLine("-------------------------");
                Console.WriteLine();

                Console.WriteLine("Choose another city?  yes/no");
                var userInput = Console.ReadLine();
                Console.WriteLine();


                if (userInput.ToLower() == "no")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
            }         
        }
    }
}
