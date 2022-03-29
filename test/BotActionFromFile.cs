namespace test
{
    internal class BotActionFromFile : IRepo
    {
        void IRepo.Aphorisms()
        {
            Console.WriteLine(File.ReadAllText(@"C:\Users\d.djioev\Desktop\test\test\AppData\Aphorisms.txt"));
        }

        void IRepo.Hello()
        {
            Console.WriteLine(File.ReadAllText(@"C:\Users\d.djioev\Desktop\test\test\AppData\Hello.txt"));
        }

        void IRepo.Joke()
        {
            Console.WriteLine(File.ReadAllText(@"C:\Users\d.djioev\Desktop\test\test\AppData\Jokes.txt"));
        }

        void IRepo.MyName()
        {
            Console.WriteLine("Шарпиком кличут");
        }

        void IRepo.TimeNow()
        {
            Console.WriteLine(File.ReadAllText(@"C:\Users\d.djioev\Desktop\test\test\AppData\TimeNow.txt"));
        }
    }
}
