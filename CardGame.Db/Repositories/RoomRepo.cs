using System;
using System.Collections.Generic;
using CardGame.Db.Entities;
using CardGame.Db.Interfaces;
using CardGame.Db.UnitOfWork;

namespace CardGame.Db.Repositories
{
    public class RoomRepo: BaseCrudRepo<Room>, IRoomRepo
    {
        #region Constructor

        public RoomRepo(MainUnitOfWork uow): base(uow)
        {
        }

        #endregion
    }
}
