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
                moqObject.Setup(m => m.GetInt32(0)).Returns(Stabs.Product1.Id);
                moqObject.Setup(m => m.GetString(1)).Returns(Stabs.Product1.Name);
                moqObject.Setup(m => m.GetString(2)).Returns(Stabs.Product1.Description);
                moqObject.Setup(m => m.GetInt32(3)).Returns(Stabs.Product1.Weight);
                moqObject.Setup(m => m.GetInt32(4)).Returns(Stabs.Product1.Height);
                moqObject.Setup(m => m.GetInt32(5)).Returns(Stabs.Product1.Width);
                moqObject.Setup(m => m.GetInt32(6)).Returns(Stabs.Product1.Length);
                return moqObject;
            }
        }


        public static Mock<IDataReader> ReaderOfProductList
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.SetupSequence(m => m.Read()).Returns(true).Returns(true).Returns(true).Returns(false);
                moqObject.SetupSequence(m => m.GetInt32(0)).Returns(Stabs.Product1.Id).Returns(Stabs.Product2.Id).Returns(Stabs.Product3.Id);
                moqObject.SetupSequence(m => m.GetString(1)).Returns(Stabs.Product1.Name).Returns(Stabs.Product2.Name).Returns(Stabs.Product3.Name);
                moqObject.SetupSequence(m => m.GetString(2)).Returns(Stabs.Product1.Description).Returns(Stabs.Product2.Description).Returns(Stabs.Product3.Description);
                moqObject.SetupSequence(m => m.GetInt32(3)).Returns(Stabs.Product1.Weight).Returns(Stabs.Product2.Weight).Returns(Stabs.Product3.Weight);
                moqObject.SetupSequence(m => m.GetInt32(4)).Returns(Stabs.Product1.Height).Returns(Stabs.Product2.Height).Returns(Stabs.Product3.Height);
                moqObject.SetupSequence(m => m.GetInt32(5)).Returns(Stabs.Product1.Width).Returns(Stabs.Product2.Width).Returns(Stabs.Product3.Width);
                moqObject.SetupSequence(m => m.GetInt32(6)).Returns(Stabs.Product1.Length).Returns(Stabs.Product2.Length).Returns(Stabs.Product3.Length);
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
