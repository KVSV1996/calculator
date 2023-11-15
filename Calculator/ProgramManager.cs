
namespace Calculator
{
    public class ProgramManager
    {
        private readonly ICommunication comunication;
        private IComunicationWithFile fileComunication = new FileComunication();
        BracketsCalculator workWithFile = new BracketsCalculator();
               
        public ProgramManager(ICommunication comunication, IComunicationWithFile read)
        {
            this.comunication = comunication ?? throw new ArgumentNullException(nameof(comunication));
            this.fileComunication = read ?? throw new FileNotFoundException(nameof(comunication));
        }

        public void StartProgram(string[] args)
        {
            CheckPath(args);
        }

        private void WorkInConsole()
        {
            while (true)
            {
                ConsoleCalculator console = new ConsoleCalculator();
                try
                {
                    comunication.Write(Constants.EnterExample);
                    string example = comunication.Read();
                    comunication.Write(console.Calculate(example));
                }
                catch (DivideByZeroException)
                {
                    comunication.Write(Constants.ExceptionThroughZero);
                }
                catch (FormatException)
                {
                    comunication.Write(Constants.DefectiveExample);
                }
                catch
                {
                    comunication.Write(Constants.Errore);
                }

                comunication.Write(Constants.BreakSentence);

                if (comunication.ReadKey() == ConsoleKey.Spacebar)
                {
                    break;
                }

            }
        }

        private void WorkWithFile(string path)
        {            
            fileComunication.WriteText(CreatingReversePath(path), CalculationLineFromFile(fileComunication.ReadText(path)));
            comunication.Write(Constants.FileWritten);
            comunication.Read();                        
        }

        private string[] CalculationLineFromFile(string[] array)
        {
            var ansver = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {                
                try
                {
                    ansver[i] = array[i] + " = " + workWithFile.Calculate(array[i]);
                }
                catch (DivideByZeroException)
                {
                    ansver[i] = array[i] + " = " + Constants.ExceptionThroughZero;
                }
                catch (FormatException)
                {
                    ansver[i] = array[i] + " = " + Constants.DefectiveExample;
                }
                catch
                {
                    ansver[i] = array[i] + " = " + Constants.Errore;
                }
            }
            return ansver;
        }

        private string CreatingReversePath(string path)
        {
            string nameOfFile = "Output.txt";

            int index = path.LastIndexOf('/');
            if (index != -1)
            {
                path = path.Remove(index + 1);
                return path + nameOfFile;
            }

            return nameOfFile;
        }

        private void CheckPath(string[] args)
        {            
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                     WorkWithFile(args[0]);
                }
            }
            else
            {
                WorkInConsole();
            }            
        }            
    }
}
