using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Napa.Data.Identity
{
    // add-Migration -Context Napa.Data.Identity.DbContext -OutputDir Data\Identity\Migrations
    // remove-Migration -Context Napa.Data.Identity.DbContext
    public class DbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected DbContext() : base() { }
    }
}
