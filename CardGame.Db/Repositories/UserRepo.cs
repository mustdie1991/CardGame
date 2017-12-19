using CardGame.Db.Contexts;
using CardGame.Db.Entities;
using CardGame.Db.Interfaces;
using CardGame.Db.UnitOfWork;

namespace CardGame.Db.Repositories
{
    public class UserRepo: BaseCrudRepo<User>, IUserRepo
    {
        #region Constructors

        public UserRepo(CommonUnitOfWork<MainDbContext> uow) : base(uow)
        {
        }

        #endregion
    }
}