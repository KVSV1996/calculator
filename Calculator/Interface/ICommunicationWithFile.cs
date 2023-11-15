
namespace Calculator
{
    public interface IComunicationWithFile
    {
        string[] ReadText(string message);
        void WriteText(string path, string[] output);
    }
}
