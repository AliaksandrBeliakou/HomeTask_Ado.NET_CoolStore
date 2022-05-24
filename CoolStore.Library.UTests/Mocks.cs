using CoolStore.Library.SqlData.Interfaces;
using System.Data;

namespace CoolStore.Library.UTests
{
    internal static class Mocks
    {
        public static Mock<IDataReader> ReaderWhitoutProduct
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.Setup(m => m.Read()).Returns(false);
                return moqObject;
            }
        }
        public static Mock<IDataReader> ReaderOfSingleProduct
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.SetupSequence(m => m.Read()).Returns(true).Returns(false);
                moqObject.Setup(m => m.GetInt32(0)).Returns(1);
                moqObject.Setup(m => m.GetString(1)).Returns("Super computer");
                moqObject.Setup(m => m.GetString(2)).Returns("Calculate really fast");
                moqObject.Setup(m => m.GetInt32(3)).Returns(100000);
                moqObject.Setup(m => m.GetInt32(4)).Returns(2000);
                moqObject.Setup(m => m.GetInt32(5)).Returns(400);
                moqObject.Setup(m => m.GetInt32(6)).Returns(700);
                return moqObject;
            }
        }

        public static Mock<IDbCommand> GetCommandWithReader(IDataReader mockReader)
        {
            var moqObject = new Mock<IDbCommand>();
            moqObject.Setup(m => m.ExecuteReader()).Returns(mockReader);
            return moqObject;
        }

        public static Mock<IDbConnection> Connection
        {
            get
            {
                return new Mock<IDbConnection>();
            }
        }

        public static Mock<IConnectedDbActorsFactory> GetConnectedDbActorsFactory(IDataReader mockReader)
        {
            var moqObject = new Mock<IConnectedDbActorsFactory>();
            moqObject.Setup(m => m.GetConnection(It.IsAny<string>())).Returns(Connection.Object);
            moqObject.Setup(m => m.GetCommand(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IEnumerable<IDbDataParameter>>()))
                .Returns(GetCommandWithReader(mockReader).Object);
            moqObject.Setup(m => m.GetDataReader(It.IsAny<IDbCommand>())).Returns(mockReader);
            return moqObject;
        }
    }
}
