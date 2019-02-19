using Roznamah.Model.Entities;
using Roznamah.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Services.Interfaces
{
    public interface IShopService
    {
        IEnumerable<Shop> GetAllShops();
        Shop GetShop(int shopId);
    }
}
