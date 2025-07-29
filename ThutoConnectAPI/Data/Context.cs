using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThutoConnectAPI.Models;

namespace ThutoConnectAPI.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) :base(options)
        {
            
        }
    }
}
