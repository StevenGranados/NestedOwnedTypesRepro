using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NestedOwnedTypesRepro;

var DbContext = new AppDbContext();
DbContext.Database.EnsureCreated();
var mapper = new Mapper(new MapperConfiguration(cfg =>
{
    cfg.CreateMap<AEntity, A>();
    cfg.CreateMap<BEntity, B>();
    cfg.CreateMap<CEntity, C>();
}));
var a = new AEntity { Name = "First", Bs = new BEntity[] { new BEntity { Name = "First", Data="Data", Cs = new CEntity[] { new CEntity { Name = "First" } } } } };
DbContext.As.Add(a);
DbContext.SaveChanges();
var queryres = DbContext.As.ToList();
var mapres = mapper.Map<List<A>>(queryres);
var projectres = (mapper as IMapper).ProjectTo<A>(DbContext.As).ToList();

class AppDbContext : DbContext
{
    public DbSet<AEntity> As { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conn = new SqliteConnection("DataSource=:memory:");
        conn.Open();
        optionsBuilder.UseSqlite(conn);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AEntity>(e =>
        {
            e.HasKey(a => a.Name);
            e.OwnsMany(pve => pve.Bs, BOwnershipBuilder =>
            {
                BOwnershipBuilder.HasKey(b => new { b.AName, b.Name });
                BOwnershipBuilder.WithOwner(b => b.A).HasForeignKey(b => b.AName);
                BOwnershipBuilder.OwnsMany(b => b.Cs, COwnershipBuilder =>
                {
                    COwnershipBuilder.HasKey(c => new { c.AName, c.BName, c.Name });
                    COwnershipBuilder.WithOwner(c => c.B).HasForeignKey(c => new { c.AName, c.BName });
                });
            });
        });
    }
}


