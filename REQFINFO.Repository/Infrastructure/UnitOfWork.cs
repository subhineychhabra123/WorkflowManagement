using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using System.Data.Entity;
using System.Transactions;

namespace REQFINFO.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        private readonly GIGEntities _db;
        public UnitOfWork()
        {
            _db = new GIGEntities();

        }
        public void Dispose()
        {

        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }



    }
}
