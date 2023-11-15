
namespace Calculator
{
    public class ConsoleComunication : ICommunication
    {
        public void Write(string data)
        {
            Console.WriteLine(data);
        }
        
        public string Read()
        {
            return Console.ReadLine();
        }

        public ConsoleKey ReadKey()
        {
            return Console.ReadKey().Key;
        }
    }
}
