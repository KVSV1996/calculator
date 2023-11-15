using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Moq;

namespace Calculator.Test
{
    [TestClass]
    public class BracketsCalculatorTest
    {
        private readonly ProgramManager _programManager;
        private readonly Mock<ICommunication> _communicationMock = new();
        private readonly Mock<IComunicationWithFile> _fileComunicationMock = new();
        public BracketsCalculatorTest()
        {
            this._programManager = new ProgramManager(this._communicationMock.Object, this._fileComunicationMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            File.WriteAllText("TestFile.txt", "");            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            File.Delete("TestFile.txt");
            File.Delete("Output.txt");
        }

        [DataRow("1+2*(3+2)", "11")]
        [DataRow("(2+2)*-2+15", "7")]
        [DataRow("2*(3*(5-3))", "12")]
        [DataRow("(5+3)*3-(88/8-7)", "20")]
        [DataRow("10", "10")]
        [DataRow("(5/2)+(100/2*(3+-5))", "-97,5")]
        [DataRow("(51+9*3)/6", "13")]
        [DataRow("((2*(50/5)+8)-4)/6", "4")]
        [DataRow("3", "3")]
        [DataRow("(1)", "1")]
        [DataRow("1+(1)+1", "3")]
        [DataRow("(-5)", "-5")]
        

        [TestMethod]
        public void ValidForFile(string input, string output)
        {
            TestInitialize();

            this._communicationMock.Setup(x => x.Read())
               .Returns("");
            this._fileComunicationMock.Setup(r => r.ReadText("TestFile.txt"))
                .Returns(new string[] { input });           

            string[] path = { "TestFile.txt" };
            string[] ansver = { input + " = " + output };
            this._programManager.StartProgram(path);

            this._fileComunicationMock.Verify(p => p.WriteText("Output.txt", ansver));
            this._communicationMock.Verify(p => p.Write(Constants.FileWritten));            

            TestCleanup();
        }

        [DataRow("6-6/(2-2)", Constants.ExceptionThroughZero)]
        [DataRow("2//2+2*2-1", Constants.DefectiveExample)]
        [DataRow("(1+1)+1=3", Constants.DefectiveExample)]
        [DataRow("1+2*(3+2))", Constants.DefectiveExample)]
        [DataRow("", Constants.Errore)]
        [DataRow("20O0", Constants.DefectiveExample)]

        [TestMethod]
        public void InvalidForFile(string input, string output)
        {
            TestInitialize();

            this._communicationMock.Setup(x => x.Read())
               .Returns("");

            this._fileComunicationMock.Setup(r => r.ReadText("TestFile.txt"))
                .Returns(new string[] { input });

            string[] path = { "TestFile.txt" };
            string[] ansver = { input + " = " + output };

            this._programManager.StartProgram(path);

            this._fileComunicationMock.Verify(p => p.WriteText("Output.txt", ansver));
            this._communicationMock.Verify(p => p.Write(Constants.FileWritten));

            TestCleanup();
        }
    }
}
