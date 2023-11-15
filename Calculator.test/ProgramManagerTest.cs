using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Calculator.Test
{
    [TestClass]
    public class ProgramManagerTest
    {        
        private readonly ProgramManager _programManager;
        private readonly Mock<ICommunication> _communicationMock = new();
        private readonly Mock<IComunicationWithFile> _fileComunicationMock = new();
        private readonly BracketsCalculatorTest bracketsCalculatorTest = new();
        public ProgramManagerTest()
        {
            this._programManager = new ProgramManager(this._communicationMock.Object, this._fileComunicationMock.Object);
        }
        
        
        [TestMethod]
        public void ConsoleCalculatorTest()
        {
            this._communicationMock.Setup(x => x.Read())
               .Returns("2*11");
            this._communicationMock.Setup(x => x.ReadKey())
            .Returns(ConsoleKey.Spacebar);

            string[] array = new string[] { };
            this._programManager.StartProgram(array);

            this._communicationMock.Verify(p => p.Write(Constants.EnterExample));
            this._communicationMock.Verify(p => p.Write("22"));
            this._communicationMock.Verify(p => p.Write(Constants.BreakSentence));

            string[] ansver = {};
            this._fileComunicationMock.Verify(p => p.WriteText("Output.txt", ansver), Times.Never);
            this._communicationMock.Verify(x => x.Write(Constants.FileWritten), Times.Never);


        }

        [TestMethod]
        public void BrecetsCalculatorTest()
        {
            bracketsCalculatorTest.TestInitialize();

            this._communicationMock.Setup(x => x.Read())
               .Returns("");
            this._fileComunicationMock.Setup(r => r.ReadText("TestFile.txt"))
                .Returns(new string[] { "1+2*(3+2)" });

            string[] path = { "TestFile.txt" };
            string[] ansver = { "1+2*(3+2) = 11" };
            this._programManager.StartProgram(path);

            this._fileComunicationMock.Verify(p => p.WriteText("Output.txt", ansver));
            this._communicationMock.Verify(p => p.Write(Constants.FileWritten));

            this._communicationMock.Verify(x => x.Write(Constants.EnterExample), Times.Never);
            this._communicationMock.Verify(x => x.Write(Constants.BreakSentence), Times.Never);

            bracketsCalculatorTest.TestCleanup();
        }
    }
}
