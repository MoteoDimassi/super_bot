using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class BotActionFromConsole: IRepo
    {
        void IRepo.Hello()
        {

            Console.WriteLine("Здравствуй, дорогой друг!");
        }

        void IRepo.MyName()
        {
            Console.WriteLine("Моё имя Шарпик");
        }
        static async internal Task<string> JokeParser()
        {
            Random random = new Random();

            var config = Configuration.Default.WithDefaultLoader();
            var address = $"http://anekdotov.net/anekdot/one/random{random.Next(1, 194)}.html";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var text = document.QuerySelector(".txt2imgtd a").Text();

            return text;
        }
        void IRepo.Joke()
        {
            var str = JokeParser().Result; ;

            Console.WriteLine(str);
        }
        static async internal Task<string> AphorismsParser()
        {
            Console.WriteLine("Даже не знаю, что сказать в такой ситуации... Вот вам житейская мудрость:");
            Random rand = new Random();
            var convig = Configuration.Default.WithDefaultLoader();
            var address = "https://all-aforizmy.ru/citaty/korotkie-tsitaty-pro-zhizn-so-smyslom-300-tsitat/";
            var document = await BrowsingContext.New(convig).OpenAsync(address);
            var text = document.QuerySelectorAll(".su-note div");
            return text[rand.Next(0, 300)].Text();

        }

       void IRepo.Aphorisms()
        {
            string str = AphorismsParser().Result;
            Console.WriteLine(str);
        }


        void IRepo.TimeNow()
        {
            DateTime time = DateTime.Now;
            string timeString = time.ToString($"Местное время {time:t}");
            Console.WriteLine(timeString);
        }
    }
}
