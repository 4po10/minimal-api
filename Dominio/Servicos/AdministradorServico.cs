using System.Data.Common;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _conexto;
    public AdministradorServico(DbContexto contexto)
    {
        _conexto = contexto;
    }
    public Administrador? BuscarPorId(int id)
    {
        return _conexto.Administradores
            .Where(v => v.Id == id)
            .FirstOrDefault();
    }
    public Administrador Incluir(Administrador administrador)
    {
        _conexto.Administradores.Add(administrador);
        _conexto.SaveChanges();
        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _conexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        return adm;

    }

    public List<Administrador> Todos(int? pagina)
    {
        var query = _conexto.Administradores.AsQueryable();
        int itensPorPagina = 10;

        if (pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * itensPorPagina)
                     .Take(itensPorPagina);
        }

        return query.ToList();
    }
}