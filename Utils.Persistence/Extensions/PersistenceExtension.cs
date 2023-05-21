﻿using Utils.Domain.Repositories;
using Utils.Persistence.Contexts;
using Utils.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Persistence.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString, string migrationAssembly = "")
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, sql =>
            {
                if (!string.IsNullOrEmpty(migrationAssembly))
                {
                    sql.MigrationsAssembly(migrationAssembly);
                }
            }))
            .AddDbContextFactory<ApplicationDbContext>((Action<DbContextOptionsBuilder>)null, ServiceLifetime.Scoped)
            .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IUnitOfWork), services =>
            {
                return services.GetRequiredService<ApplicationDbContext>();
            });

            return services;
        }
    }
}
