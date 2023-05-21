using Utils.CrossCuttingConcerns.OS;
using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Utils.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly IDateTimeProvider _dateTimeProvider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeProvider dateTimeProvider) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IDisposable> BeginTransactionAsync(CancellationToken token = default)
        {
            _transaction = await Database.BeginTransactionAsync(token);
            return _transaction;
        }

        public async Task CommitTransactionAsync(CancellationToken token = default)
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync(token);
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>()
               .HasIndex(u => u.UserName)
               .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique(true);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                if (entry.Entity is TrackableEntity baseEntity)
                {
                    var now = _dateTimeProvider.OffsetNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            baseEntity.CreatedAt = now;
                            //Not Implemented
                            //baseEntity.CreatedBy = username;
                            break;

                        case EntityState.Modified:
                            baseEntity.UpdatedAt = now;
                            //Not Implemented
                            //baseEntity.UpdatedBy = username;
                            break;

                        case EntityState.Deleted:
                        case EntityState.Detached:
                        case EntityState.Unchanged:
                        default:
                            break;
                    }
                }
            }
        }
    }
}
