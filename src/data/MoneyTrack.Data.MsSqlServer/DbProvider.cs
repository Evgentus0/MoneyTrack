using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Data.MsSqlServer.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MoneyTrack.Data.MsSqlServer
{
    public class DbProvider : IDbProvider
    {
        private readonly MoneyTrackContext _context;
        private readonly IMapper _mapper;

        public DbProvider(MoneyTrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollectionAdapter<Transaction, int> Transactions 
            => new CollectionAdapter<Transaction, Entites.Transaction, int>(_context.Transactions, _mapper, _context);

        public ICollectionAdapter<Category, int> Categories
            => new CollectionAdapter<Category, Entites.Category, int>(_context.Categories, _mapper, _context);

        public ICollectionAdapter<Account, int> Accounts 
            => new CollectionAdapter<Account, Entites.Account, int>(_context.Accounts, _mapper, _context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();

        }
    }
}
