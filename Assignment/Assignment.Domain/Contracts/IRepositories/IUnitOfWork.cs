﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Contracts.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> SaveChanges();
    }
}
