using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Abstract;
using Ezine.Infrastructure;

namespace Ezine.Repository
{
    public class EzineRepository : IEzine
    {
        EFDbContext db = new EFDbContext();


    }
}