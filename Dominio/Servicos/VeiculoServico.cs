using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _conexto;
    public VeiculoServico(DbContexto contexto)
    {
        _conexto = contexto;
    }

    public void Atualizar(Veiculo veiculo)
    {
        _conexto.Veiculos.Update(veiculo);
        _conexto.SaveChanges();
        // Lógica de atualização do veículo
    }

    public void Apagar(Veiculo veiculo)
    {
        _conexto.Veiculos.Remove(veiculo);
        _conexto.SaveChanges();
        // Lógica de exclusão do veículo
    }

    public Veiculo? BuscarPorId(int id)
    {
        return _conexto.Veiculos
            .Where(v => v.Id == id)
            .FirstOrDefault();
    }

    public void Incluir(Veiculo veiculo)
    {
        _conexto.Veiculos.Add(veiculo);
        _conexto.SaveChanges();
    }


    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
    {
        var query = _conexto.Veiculos.AsQueryable();
        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
        }

        int itensPorPagina = 10;

        if (pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * itensPorPagina)
                     .Take(itensPorPagina);
        }


        return query.ToList();
    }

}