using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Calculator.Test
{
    [TestClass]
    public class ConsoleCalculatorTest
    {
        private readonly ProgramManager _programManager;
        private readonly Mock<ICommunication> _communicationMock = new();
        private readonly Mock<IComunicationWithFile> _fileComunicationMock = new();
        
        public ConsoleCalculatorTest()
        {
            this._programManager = new ProgramManager(this._communicationMock.Object, this._fileComunicationMock.Object);
        }

        [DataRow("1+2+3*4+5+6", "26")]
        [DataRow("-3", "-3")]
        [DataRow("-8/2", "-4")]
        [DataRow("1+2+3*-4+5+6","2")]
        [DataRow("9/2","4,5")]
        [DataRow("12--12","24")]
        [DataRow("-2+36/-9*3+3/-1", "-17")]
        [DataRow("2*11","22")]        
        [TestMethod]
        public void ValidForConsole(string input, string output)
        {
            this._communicationMock.SetupSequence(x => x.Read())
               .Returns(input);
            this._communicationMock.SetupSequence(x => x.ReadKey())
            .Returns(ConsoleKey.Spacebar);

            string[] array = new string[] { };
            this._programManager.StartProgram(array);

            this._communicationMock.Verify(p => p.Write(Constants.EnterExample));
            this._communicationMock.Verify(p => p.Write(output));
            this._communicationMock.Verify(p => p.Write(Constants.BreakSentence));
            

        }       


        [DataRow("", "Errore")]
        [DataRow(null, "Errore")]
        [DataRow("(1+1)+1", "Exception. Wrong input.")]
        [DataRow("7****8", "Exception. Wrong input.")]
        [DataRow("10O0", "Exception. Wrong input.")]
        [DataRow("1*2+x-8", "Exception. Wrong input.")]
        [DataRow("2/0", "Exception. Divide by zero")]
        [TestMethod]
        public void InvalidForConsole(string input, string output)
        {
            this._communicationMock.SetupSequence(x => x.Read())
               .Returns(input);
            this._communicationMock.SetupSequence(x => x.ReadKey())
            .Returns(ConsoleKey.Spacebar);

            string[] array = new string[] { };
            this._programManager.StartProgram(array);

            this._communicationMock.Verify(p => p.Write(Constants.EnterExample));
            this._communicationMock.Verify(p => p.Write(output));
            this._communicationMock.Verify(p => p.Write(Constants.BreakSentence));
        }
    }
}