using System.Collections.Concurrent;
using test;

static class Program
{
    private delegate void MyFunc();

    static bool flag = true;
    static void Main()
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
                {"который час?", repo.TimeNow }
            };

        repo.Hello();

        ConcurrentQueue<MyFunc> cq = new ConcurrentQueue<MyFunc>();

        Thread checkQueue = new Thread(new ParameterizedThreadStart(StartReading));
        checkQueue.Start(cq);

        string str = Console.ReadLine();
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
            else
            {
                cq.Enqueue(repo.Aphorisms);
            }
            Console.WriteLine(cq.Count + " в очереди");
            str = Console.ReadLine();
        }
        flag = false;
        Console.WriteLine("До новых встреч, дорогой собеседник!");
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
}
