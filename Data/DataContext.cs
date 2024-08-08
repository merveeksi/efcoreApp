using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }
    public DbSet<Kurs> Kurslar { get; set; } = null!;
    
    public DbSet<Ogrenci> Ogrenciler { get; set; } = null!;
    
    public DbSet<KursKayit> KursKayitlar { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KursKayit>()
            .HasKey(kk => new { kk.OgrenciId, kk.KursId });
        modelBuilder.Entity<KursKayit>()
            .HasOne(kk => kk.Ogrenci)
            .WithMany(o => o.KursKayitlar)
            .HasForeignKey(kk => kk.OgrenciId);
        modelBuilder.Entity <KursKayit>()
            .HasOne(kk => kk.Kurs)
            .WithMany(o => k.KursKayitlar)
            .HasForeignKey(kk => kk.KursId);
    }
}

//Code-first => entty, dbcontext => database (sqlite)
//database-first => sql server