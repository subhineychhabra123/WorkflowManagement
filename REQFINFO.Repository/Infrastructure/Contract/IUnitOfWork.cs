﻿using System;
using System.Data.Entity;


namespace REQFINFO.Repository.Infrastructure.Contract
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        DbContext Db { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();


    }
}
