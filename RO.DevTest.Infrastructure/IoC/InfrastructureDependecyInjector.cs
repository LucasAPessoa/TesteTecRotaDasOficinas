﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RO.DevTest.Application.Common.Interfaces;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Common;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Infrastructure.Abstractions;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.Repositories;

namespace RO.DevTest.Infrastructure.IoC;

public static class InfrastructureDependecyInjector {
    /// <summary>
    /// Inject the dependencies of the Infrastructure layer into an
    /// <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to inject the dependencies into
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with dependencies injected
    /// </returns>
    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services) {
        services.AddDefaultIdentity<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DefaultContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IIdentityAbstractor, IdentityAbstractor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<SaleItemService>();

        return services;
    }
}
