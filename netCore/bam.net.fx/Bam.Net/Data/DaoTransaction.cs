/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Bam.Net.Incubation;

namespace Bam.Net.Data
{
    public class DaoTransaction: IDisposable
    {
        public event EventHandler Committed;
        public event EventHandler RolledBack;
        public event EventHandler Disposed;

        List<Dao> _toDelete = new List<Dao>();
        List<Dao> _toUndo = new List<Dao>();
        List<Dao> _toUndelete = new List<Dao>();

        public DaoTransaction(Database database)
        {
            this._db = new Database(database.ServiceProvider.Clone(), database.ConnectionString, database.ConnectionName);
            Dao.BeforeCommitAny += DaoBeforeCommitAny;
            Dao.BeforeDeleteAny += DaoBeforeDeleteAny;
        }

        protected void DaoBeforeCommitAny(Database db, Dao dao)
        {
            if (db == this.Database)
            {
                if (dao.IsNew)
                {
                    _toDelete.Add(dao); // it's being inserted
                }
                else
                {
                    _toUndo.Add(dao); // it's being updated
                }
            }
        }

        protected void DaoBeforeDeleteAny(Database db, Dao dao)
        {
            if (db == this.Database)
            {
                _toUndelete.Add(dao); 
            }
        }

        Database _db;
        public Database Database
        {
            get
            {
                return this._db;
            }
        }

        protected bool WasCommitted { get; set; }

        public void Commit()
        {
            WasCommitted = true;
            OnCommitted();
        }

        public void Rollback()
        {
            WasCommitted = false;
            foreach (Dao dao in this._toDelete)
            {
                dao.Delete();
            }
            foreach (Dao dao in this._toUndelete)
            {
                dao.Undelete();
            }
            foreach (Dao dao in this._toUndo)
            {
                dao.Undo();
            }

            OnRolledback();
        }

        private void OnCommitted()
        {
            if (Committed != null)
            {
                Committed(this, new EventArgs());
            }
        }

        private void OnRolledback()
        {
            if (RolledBack != null)
            {
                RolledBack(this, new EventArgs());
            }
        }

        private void OnDisposed()
        {
            if (Disposed != null)
            {
                Disposed(this, new EventArgs());
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!WasCommitted)
            {
                this.Rollback();
            }

            Dao.BeforeDeleteAny -= DaoBeforeDeleteAny;
            Dao.BeforeCommitAny -= DaoBeforeCommitAny;
            this.OnDisposed();
        }

        #endregion
    }
}
