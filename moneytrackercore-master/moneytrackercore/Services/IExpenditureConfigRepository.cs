﻿using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public interface IExpenditureConfigRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        IEnumerable<ExpenditureConfig> GetAllExpenditureConfig();
        ExpenditureConfig GetExpenditureConfig(int id);
    }
}
