using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Dominio.ModelViews;


public record AdministadorModelViews
{

    public int Id { get; set; }
   public string Email { get; set; } = default!;
    public string Perfil { get; set; } = default!;
}
    
