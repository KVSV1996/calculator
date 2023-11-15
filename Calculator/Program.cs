namespace Calculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            ICommunication comunication = new ConsoleComunication();
            IComunicationWithFile fileComunication = new FileComunication();
            ProgramManager programManager = new ProgramManager(comunication, fileComunication);            
            programManager.StartProgram(args);
        }
    }
}