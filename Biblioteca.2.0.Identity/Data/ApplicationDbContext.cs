using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca._2._0.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //No construtor é passado DbContextoOption para que seja possivel passar opções vindas da startUp
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
    }
}
