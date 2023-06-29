using System;
using System.Collections.Generic;

namespace Core.Entidades.Public;

public partial class Perfil
{
    public int IdPerfil { get; set; }

    public string? NombrePerfil { get; set; }

    public string? DescripcionPerfil { get; set; }

    public bool? ActivoPerfil { get; set; }

    public virtual ICollection<Autorizacion> Autorizacions { get; } = new List<Autorizacion>();

    public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; } = new List<UsuarioPerfil>();
}
