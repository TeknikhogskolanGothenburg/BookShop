using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class ShopBookRepository : GenericRepository<BookContext, ShopBook>
    {
    }
}
