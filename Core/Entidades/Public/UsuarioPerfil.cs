using System;
using System.Collections.Generic;

namespace Core.Entidades.Public;

public partial class UsuarioPerfil
{
    public string NombreUsuario { get; set; } = null!;

    public int IdPerfil { get; set; }

    public bool? ActivoPerfilUsuario { get; set; }

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual Usuario NombreUsuarioNavigation { get; set; } = null!;
}
