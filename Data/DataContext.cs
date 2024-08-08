using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }
    public DbSet<Kurs> Kurslar { get; set; } = null!;
    
    public DbSet<Ogrenci> Ogrenciler { get; set; } = null!;

    public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
    public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();
    
    

}

//Code-first => entty, dbcontext => database (sqlite)
//database-first => sql server