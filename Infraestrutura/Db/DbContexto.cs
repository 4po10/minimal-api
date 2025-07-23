using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{
    private readonly IConfiguration _configuacaoAppSettings;
    public DbContexto(IConfiguration configuracaoAppSettings)

    {
        _configuacaoAppSettings = configuracaoAppSettings;
    }
    public DbSet<Administrador> Administradores { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _configuacaoAppSettings.GetConnectionString("mysql")?.ToString();
            if (!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseMySql(
                    stringConexao,
                    ServerVersion.AutoDetect(stringConexao)
                );
            }
        }



    }


}