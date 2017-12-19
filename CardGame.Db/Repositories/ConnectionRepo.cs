using CardGame.Db.Contexts;
using CardGame.Db.Entities;
using CardGame.Db.UnitOfWork;

namespace CardGame.Db.Repositories
{
    public class ConnectionRepo : BaseCrudRepo<Connection>
    {
        public ConnectionRepo(CommonUnitOfWork<MainDbContext> uow) : base(uow)
        {
        }
    }
}
