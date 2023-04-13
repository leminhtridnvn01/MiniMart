﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniMart.Domain.Interfaces;
using System.Data;

namespace MiniMart.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        private IsolationLevel? _isolationLevel;

        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransaction()
        {
            if (_transaction == null)
            {
                if (_isolationLevel.HasValue)
                {
                    _transaction = await _dbContext.Database.BeginTransactionAsync(_isolationLevel.Value);
                }
                else
                {
                    _transaction = await _dbContext.Database.BeginTransactionAsync();
                }
            }
        }

        public async Task<bool> CommitTransaction()
        {
            var result = await _dbContext.SaveEntitiesAsync();

            if (_transaction == null) return false;
            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();
            _transaction = null;

            return result;
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _dbContext.SaveEntitiesAsync();
        }

        public void Dispose()
        {
            if (_dbContext == null) return;

            _dbContext.Dispose();
        }
    }
}
