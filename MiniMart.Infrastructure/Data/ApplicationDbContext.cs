using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore.Design;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                .UseSqlServer("server=118.69.61.226;database=Intern_Microservice;user id=sa;password=vStation123;");

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
