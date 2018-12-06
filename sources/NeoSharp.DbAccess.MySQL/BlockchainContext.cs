using Microsoft.EntityFrameworkCore;
using NeoSharp.Model;

namespace NeoSharp.DbAccess.MySQL
{
    public class BlockchainContext : DbContext
    {
        //public DbSet<Block> Blocks { get; set; }

        //public DbSet<BlockWitness> BlockWitnesses { get; set; }

        //public DbSet<Transaction> Transactions { get; set; }

        //public DbSet<TransactionWitness> TransactionWitnesses { get; set; }

        //public DbSet<InputCoinReference> InputCoinReferences { get; set; }

        //public DbSet<ClaimCoinReference> ClaimCoinReferences { get; set; }

        public DbSet<BlockRaw> BlocksRawData { get; set; }

        public BlockchainContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=NeoBlockchain;Uid=root;Pwd=0000");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlockRaw>(entity =>
            {
                entity.HasKey(x => x.Index);

                //entity
                //    .Property(x => x.Index)
                //    .HasDefaultValue(0);

                entity
                    .Property(x => x.RawData)
                    .HasColumnType("longtext");
            });

            //modelBuilder.Entity<BlockWitness>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //});

            //modelBuilder.Entity<Block>(entity =>
            //{
            //    entity.HasKey(x => x.Hash);
            //});

            //modelBuilder.Entity<BlockWitness>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x => x.Block)
            //        .WithOne(x => x.Script)
            //        .HasForeignKey<BlockWitness>(x => x.Hash);
            //});

            //modelBuilder.Entity<TransactionWitness>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x => x.Transaction)
            //        .WithMany(x => x.Scripts)
            //        .HasForeignKey(x => x.Hash);
            //});

            //modelBuilder.Entity<Transaction>(entity =>
            //{
            //    entity.HasKey(x => x.Hash);

            //    entity
            //        .HasOne(x => x.Block)
            //        .WithMany(x => x.Transactions)
            //        .HasForeignKey(x => x.BlockHash);
            //});

            //modelBuilder.Entity<Asset>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity.HasOne(x => x.Transaction)
            //        .WithOne(x => x.Asset)
            //        .HasForeignKey<Asset>(x => x.TransactionHash);
            //});

            //modelBuilder.Entity<Code>(entity =>
            //{
            //    entity.HasKey(x => x.Hash);

            //    entity
            //        .HasOne(x => x.Contract)
            //        .WithOne(x => x.Code)
            //        .HasForeignKey<Code>(x => x.Hash);
            //});

            //modelBuilder.Entity<Contract>(entity =>
            //{
            //    entity.HasKey(x => x.Hash);

            //    entity
            //        .HasOne(x => x.Transaction)
            //        .WithOne(x => x.Contract)
            //        .HasForeignKey<Contract>(x => x.Hash);
            //});

            //modelBuilder.Entity<TransactionOutput>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne<Contract>();

            //    entity
            //        .HasOne<Transaction>()
            //        .WithMany(x => x.Outputs)
            //        .HasForeignKey(x => x.TransactionHash);
            //});

            //modelBuilder.Entity<TransactionAttribute>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x => x.Transaction)
            //        .WithMany(x => x.Attributes)
            //        .HasForeignKey(x => x.TransactionHash);
            //});

            //modelBuilder.Entity<InputCoinReference>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x => x.Transaction)
            //        .WithMany(x => x.Inputs)
            //        .HasForeignKey(x => x.TransactionHash);
            //});

            //modelBuilder.Entity<ClaimCoinReference>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x => x.Transaction)
            //        .WithMany(x => x.Claims)
            //        .HasForeignKey(x => x.TransactionHash);
            //});
        }
    }
}
