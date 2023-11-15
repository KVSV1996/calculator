
namespace Calculator
{
    public interface ICommunication
    {
        void Write(string data);        
        string Read();
        ConsoleKey ReadKey();
        
    }
}
