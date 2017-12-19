using CardGame.Db.Contexts;

namespace CardGame.Db.UnitOfWork
{
    public class MainUnitOfWork : CommonUnitOfWork<MainDbContext>
    {
        public MainUnitOfWork(MainDbContext context) : base(context)
        {
        }
    }
}