using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer.Entites
{
    public class Category: IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
    }
}
