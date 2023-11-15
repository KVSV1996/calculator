
namespace Calculator
{
    public class FileComunication : IComunicationWithFile
    {

        public void WriteText(string path, string[] output)
        {
            File.AppendAllLines(path, output);
        }

        public string[] ReadText(string message)
        {
            return File.ReadAllLines(message);
        }
    }
}
