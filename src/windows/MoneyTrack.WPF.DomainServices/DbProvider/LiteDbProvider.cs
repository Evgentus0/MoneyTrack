using LiteDB;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.Infrastructure.Settings;

namespace MoneyTrack.WPF.DomainServices.DbProvider
{
    public class LiteDbProvider
    {
        private readonly AppSettings _settings;

        public LiteDbProvider(AppSettings settings)
        {
            _settings = settings;
        }

        public LiteDatabase GetDb()
        {
            var bsonMapper = new BsonMapper();
            bsonMapper.Entity<Transaction>().DbRef(x => x.Account, typeof(Account).Name);
            bsonMapper.Entity<Transaction>().DbRef(x => x.Category, typeof(Account).Name);

            return new LiteDatabase(_settings.LiteDBConnection);
        }
    }
}
