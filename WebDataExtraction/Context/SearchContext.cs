using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebDataExtraction.Entity;

namespace WebDataExtraction.Context
{
    public class SearchContext: DbContext
    {
        public SearchContext(): base("Name=ConnString")
        {
            
        }

        public SearchContext(string connString) : base(connString)
        {

        }

        public DbSet<RestaurentData> RestaurentDatas { get; set; }
        public DbSet<SearchData> SearchDatas { get; set; }

    }
}