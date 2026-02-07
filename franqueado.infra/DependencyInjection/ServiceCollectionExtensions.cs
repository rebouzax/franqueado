using Franqueado.Application.Abstractions;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Domain.Repositories;
using Franqueado.Infra.Persistence;
using Franqueado.Infra.Persistence.Context;
using Franqueado.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Franqueado.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FranqueadoDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoReadRepository, ProdutoReadRepository>();
        services.AddScoped<IEstoqueReadRepository, EstoqueReadRepository>();
        services.AddScoped<IEstoqueRepository, EstoqueRepository>();
        services.AddScoped<IMovimentacaoEstoqueRepository, MovimentacaoEstoqueRepository>();
        services.AddScoped<IMovimentacaoEstoqueReadRepository, MovimentacaoEstoqueReadRepository>();
        services.AddScoped<IConcurrencyService, ConcurrencyService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
