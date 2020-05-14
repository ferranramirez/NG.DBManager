﻿using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        IRepository<T> Repository<T>() where T : class;
        ITourRepository Tour { get; }
    }
}
