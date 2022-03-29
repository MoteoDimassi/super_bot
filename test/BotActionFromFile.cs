namespace test
{
    internal class BotActionFromFile : IRepo
    {
        void IRepo.Aphorisms()
        {
            string str = File.ReadAllText(@"..\..\..\AppData\Aphorisms.txt");
            Console.WriteLine(Answer(str));
        }

        void IRepo.Hello()
        {
            string str = File.ReadAllText(@"..\..\..\AppData\Hello.txt");
            Console.WriteLine(Answer(str));
        }

        void IRepo.Joke()
        {
            string str = File.ReadAllText(@"..\..\..\AppData\Jokes.txt");
            Console.WriteLine(Answer(str));
        }

        void IRepo.MyName()
        {
            Console.WriteLine("Шарпиком кличут");
        }

        void IRepo.TimeNow()
        {
            string str = File.ReadAllText(@"..\..\..\AppData\TimeNow.txt");
            Console.WriteLine(Answer(str));
        }

        string Answer(string str)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < str.Length - 4; i++)
            {
                if (str.Substring(i, 3) == ">>>")
                {
                    list.Add(i + 3);
                }
            }
            Random rand = new Random();
            int index = rand.Next(0, list.Count-1);
            return str.Substring(list[index]+1, list[index + 1] - list[index] - 4);
        }

        void IRepo.Bye()
        {
            string str = File.ReadAllText(@"..\..\..\AppData\Bye.txt");
            Console.WriteLine(Answer(str)); ;
        }
    }
}
