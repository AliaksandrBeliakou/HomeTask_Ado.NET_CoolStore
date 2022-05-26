using CoolStore.Library.SqlData.Interfaces;
using System.Data;

namespace CoolStore.Library.UTests.Helpers
{
    internal static class MockBuilder
    {
        public static Mock<IDataReader> ReaderWhitoutData
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
                moqObject.Setup(m => m.GetInt32(0)).Returns(StabBuilder.Product1.Id);
                moqObject.Setup(m => m.GetString(1)).Returns(StabBuilder.Product1.Name);
                moqObject.Setup(m => m.GetString(2)).Returns(StabBuilder.Product1.Description);
                moqObject.Setup(m => m.GetInt32(3)).Returns(StabBuilder.Product1.Weight);
                moqObject.Setup(m => m.GetInt32(4)).Returns(StabBuilder.Product1.Height);
                moqObject.Setup(m => m.GetInt32(5)).Returns(StabBuilder.Product1.Width);
                moqObject.Setup(m => m.GetInt32(6)).Returns(StabBuilder.Product1.Length);
                return moqObject;
            }
        }


        public static Mock<IDataReader> ReaderOfProductList
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.SetupSequence(m => m.Read()).Returns(true).Returns(true).Returns(true).Returns(false);
                moqObject.SetupSequence(m => m.GetInt32(0)).Returns(StabBuilder.Product1.Id).Returns(StabBuilder.Product2.Id).Returns(StabBuilder.Product3.Id);
                moqObject.SetupSequence(m => m.GetString(1)).Returns(StabBuilder.Product1.Name).Returns(StabBuilder.Product2.Name).Returns(StabBuilder.Product3.Name);
                moqObject.SetupSequence(m => m.GetString(2)).Returns(StabBuilder.Product1.Description).Returns(StabBuilder.Product2.Description).Returns(StabBuilder.Product3.Description);
                moqObject.SetupSequence(m => m.GetInt32(3)).Returns(StabBuilder.Product1.Weight).Returns(StabBuilder.Product2.Weight).Returns(StabBuilder.Product3.Weight);
                moqObject.SetupSequence(m => m.GetInt32(4)).Returns(StabBuilder.Product1.Height).Returns(StabBuilder.Product2.Height).Returns(StabBuilder.Product3.Height);
                moqObject.SetupSequence(m => m.GetInt32(5)).Returns(StabBuilder.Product1.Width).Returns(StabBuilder.Product2.Width).Returns(StabBuilder.Product3.Width);
                moqObject.SetupSequence(m => m.GetInt32(6)).Returns(StabBuilder.Product1.Length).Returns(StabBuilder.Product2.Length).Returns(StabBuilder.Product3.Length);
                return moqObject;
            }
        }

        public static Mock<IDataReader> ReaderOfSingleOrder
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.SetupSequence(m => m.Read()).Returns(true).Returns(false);
                moqObject.Setup(m => m.GetInt32(0)).Returns(StabBuilder.Order1.Id);
                moqObject.Setup(m => m.GetString(1)).Returns(StabBuilder.Order1.Status.ToString());
                moqObject.Setup(m => m.GetDateTime(2)).Returns(StabBuilder.Order1.CreateDate);
                moqObject.Setup(m => m.GetDateTime(3)).Returns(StabBuilder.Order1.UpdateDate);
                moqObject.Setup(m => m.GetInt32(4)).Returns(StabBuilder.Order1.ProductId);
                return moqObject;
            }
        }


        public static Mock<IDataReader> ReaderOfOrderList
        {
            get
            {
                var moqObject = new Mock<IDataReader>();
                moqObject.SetupSequence(m => m.Read()).Returns(true).Returns(true).Returns(false);
                moqObject.SetupSequence(m => m.GetInt32(0)).Returns(StabBuilder.Order1.Id).Returns(StabBuilder.Order2.Id);
                moqObject.SetupSequence(m => m.GetString(1)).Returns(StabBuilder.Order1.Status.ToString()).Returns(StabBuilder.Order2.Status.ToString());
                moqObject.SetupSequence(m => m.GetDateTime(2)).Returns(StabBuilder.Order1.CreateDate).Returns(StabBuilder.Order2.CreateDate);
                moqObject.SetupSequence(m => m.GetDateTime(3)).Returns(StabBuilder.Order1.UpdateDate).Returns(StabBuilder.Order2.UpdateDate);
                moqObject.SetupSequence(m => m.GetInt32(4)).Returns(StabBuilder.Order1.ProductId).Returns(StabBuilder.Order2.ProductId);
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

        public static Mock<IConnectedDbActorsFactory> GetConnectedDbActorsFactory(IDbCommand command)
        {
            var moqObject = new Mock<IConnectedDbActorsFactory>();
            moqObject.Setup(m => m.GetConnection(It.IsAny<string>())).Returns(Connection.Object);
            moqObject.Setup(m => m.GetCommand(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IEnumerable<IDbDataParameter>>()))
                .Returns(command);
            moqObject.Setup(m => m.GetDataReader(It.IsAny<IDbCommand>())).Returns(command.ExecuteReader());
            return moqObject;
        }
    }
}
