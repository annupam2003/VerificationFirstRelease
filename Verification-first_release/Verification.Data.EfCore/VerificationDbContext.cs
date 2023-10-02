using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;

namespace Verification.Data.EfCore
{
    public class VerificationDbContext : DbContext
    {
        public VerificationDbContext(DbContextOptions<VerificationDbContext> options) : base(options) { }
        public DbSet<StateZone> StateZone { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<AreaZone> AreaZone { get; set; }
        public DbSet<Area> Area { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateZone>(entity =>
            {
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.DateIs).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.DateIs).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });
            modelBuilder.Entity<AreaZone>(entity =>
            {
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.DateIs).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.DateIs).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}

/*
 * Add Migration
    add-migration init

* Update Database
    Update-Database

 * Deleting Database
    drop-database -context verificationdbcontext

* Remove Migration
    remove-migration

-- insert into tbl_StateZone(IsActive,DateIs,Name) values('true',getdate(),'EAST'),('false',getdate(),'WEST'),('true',getdate(),'NORTH'),('false',getdate(),'SOUTH'),('true',getdate(),'CENTER')
-- insert into tbl_State(IsActive,DateIs,Name,StateZoneId) values('true',getdate(),'Delhi',1),('false',getdate(),'Pubjab',1),('true',getdate(),'Mumbay',2),('false',getdate(),'Maharstra',2),('true',getdate(),'Kokata',3),('false',getdate(),'Bangal',3),('true',getdate(),'jamu',4),('false',getdate(),'Kashmir',4),('true',getdate(),'Asam',5),('false',getdate(),'Tripura',5);
--  insert into tbl_AreaZone(IsActive,DateIs,Name,stateId) values('true',getdate(),'EAST',1),('false',getdate(),'WEST',2),('true',getdate(),'NORTH',3),('false',getdate(),'SOUTH',4),('true',getdate(),'CENTER',5)
-- insert into tbl_Area(IsActive,DateIs,Name,StateId,AreaZoneId,Pincode) values('true',getdate(),'Laxmi Nagar',1,1,'110001'),('false',getdate(),'Mayur Vihar',1,2,'110002'),('true',getdate(),'Moti bag',1,3,'110005'),('false',getdate(),'Ashram',1,4,'110004'),('true',getdate(),'Kondli',1,5,'110006'),('false',getdate(),'TirlokPuri',1,1,'110008'),('true',getdate(),'Ashok Nagar',1,2,'110007'),('false',getdate(),'Asohk vihar',1,3,'110009'),('true',getdate(),'Gandhi nagar',1,4,'110021'),('false',getdate(),'Tripura',1,5,'110011');

 */
