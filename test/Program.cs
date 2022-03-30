using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using test;

internal static class Program
{
    private delegate void MyFunc();

    private static bool flag = true;

    private static void Main()
    {
        IRepo repo = new BotActionFromFile();

        var dict = new Dictionary<string, MyFunc>()
            {
                {"привет", repo.Hello },
                {"здравствуй", repo.Hello },
                {"здравствуйте", repo.Hello },
                {"добрый день", repo.Hello },
                {"добрый вечер", repo.Hello },
                {"доброе утро", repo.Hello },
                {"доброй ночи", repo.Hello },
                {"как тебя зовут?", repo.MyName },
                {"анекдот", repo.Joke },
                {"который сейчас час?", repo.TimeNow },
                {"сколько времени?", repo.TimeNow },
                {"который час?", repo.TimeNow },

            };

        repo.Hello();

        ConcurrentQueue<MyFunc> cq = new ConcurrentQueue<MyFunc>();

        Thread checkQueue = new Thread(new ParameterizedThreadStart(StartReading));
        checkQueue.Start(cq);

        string str = Console.ReadLine().ToLower();
        while (str != "пока" && str != "до свидания")
        {
            if (dict.ContainsKey(str))
            {
                cq.Enqueue(dict[str]);
            }
            else if (str.Contains("анекдот"))
            {
                cq.Enqueue(dict["анекдот"]);
            }
            else if (str.Contains("учись"))
            {
                Learn();
            }
            else
            {
                cq.Enqueue(repo.Aphorisms);
            }
            Console.WriteLine(cq.Count + " в очереди");
            str = Console.ReadLine().ToLower();
        }
        flag = false;
        repo.Bye();
    }
    private static void StartReading(object object1)
    {
        var cq = (ConcurrentQueue<MyFunc>)object1;

        while (flag)
        {

            Task.Delay(5000).Wait();
            if (cq != null)
            {
                if (cq.TryDequeue(out MyFunc obj))
                {
                    obj();
                }
            }
        }
    }
   
    static void Learn()
    {
        string str = Console.ReadLine();
        var reg1 = new Regex(@"привет", RegexOptions.IgnoreCase);
        var reg2 = new Regex(@"анекдот", RegexOptions.IgnoreCase);
        var reg3 = new Regex(@"жизн(\w*)", RegexOptions.IgnoreCase);
        var reg4 = new Regex(@"пока", RegexOptions.IgnoreCase);
        Dictionary<Regex, Action<string?>> record = new Dictionary<Regex, Action<string?>>()
        {
            {reg1, (text) => File.AppendAllText(@"..\..\..\AppData\Hello.txt", "\n"+str+"\n>>>")},
            {reg2, (text) => File.AppendAllText(@"..\..\..\AppData\Jokes.txt", "\n"+str+"\n>>>")},
            {reg3, (text) => File.AppendAllText(@"..\..\..\AppData\Aphorisms.txt", "\n"+str+"\n>>>")},
            {reg4, (text) => File.AppendAllText(@"..\..\..\AppData\Bye.txt", "\n"+str+"\n>>>")}
        };

       
        while (str != "стоп")
        {
            foreach(var item in record.Keys)
            {
                if (item.IsMatch(str))
                {
                    record[item](str);
                }
            }
            str = Console.ReadLine().ToLower();
        }
    }
}