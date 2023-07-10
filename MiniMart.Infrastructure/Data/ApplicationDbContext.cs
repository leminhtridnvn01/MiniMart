using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore.Design;
using MiniMart.Domain.Entities;
using System.Diagnostics.Metrics;

namespace MiniMart.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Staff> Staffs  { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Store> Stores { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderParrent> OrderParrents { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }

        public DbSet<Ward> Wards { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Citys { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<StrategyDetail> StrategiesDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Client)
                        .WithOne(x => x.User)
                        .HasForeignKey<Client>(x => x.UserId);
            //modelBuilder.Entity<Client>()
            //            .HasOne(x => x.User)
            //            .WithOne(x => x.Client)
            //            .HasForeignKey<User>(x => x.ClientId);
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Staff)
                        .WithOne(x => x.User)
                        .HasForeignKey<Staff>(x => x.UserId);
            //modelBuilder.Entity<Staff>()
            //            .HasOne(x => x.User)
            //            .WithOne(x => x.Staff)
            //            .HasForeignKey<User>(x => x.StaffId);
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Manager)
                        .WithOne(x => x.User)
                        .HasForeignKey<Manager>(x => x.UserId);
            //modelBuilder.Entity<Manager>()
            //            .HasOne(x => x.User)
            //            .WithOne(x => x.Manager)
            //            .HasForeignKey<User>(x => x.ManagerId);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(this);
            return true;
        }

        public DbConnection GetConnection()
        {
            DbConnection _connection = Database.GetDbConnection();
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }
    }

    public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=tcp:trile-sql-us.database.windows.net,1433;Initial Catalog=MiniMart;Persist Security Info=False;User ID=trile;Password=Pa$$word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new ApplicationDbContext(optionsBuilder.Options, new NoMediator());
        }

        private class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<TResponse>) ?? throw new NotImplementedException();
            }

            public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<object?>) ?? throw new NotImplementedException();
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(TResponse) ?? throw new ArgumentNullException());
            }

            public Task<object?> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
