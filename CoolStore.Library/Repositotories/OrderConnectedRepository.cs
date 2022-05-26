using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData;
using CoolStore.Library.SqlData.Interfaces;

namespace CoolStore.Library.Repositotories
{
    public class OrderConnectedRepository : IOrderRepository
    {
        private readonly SqlExecuter executer;

        public OrderConnectedRepository(string connectionString, IConnectedDbActorsFactory factory)
        {
            this.executer = new SqlExecuter(
                connectionString ?? throw new ArgumentNullException(nameof(connectionString)),
                factory ?? throw new ArgumentNullException(nameof(factory)));
        }

        public void Create(Order product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order product)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order product)
        {
            throw new NotImplementedException();
        }
    }
}
