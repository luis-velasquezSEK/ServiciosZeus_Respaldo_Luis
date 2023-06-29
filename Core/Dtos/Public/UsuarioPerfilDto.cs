using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servicios_Zeus.Dtos
{
    public partial class UsuarioPerfilDto
    {

        //public string NombreUsuario { get; set; }
        public int IdPerfil { get; set; }

        public bool? ActivoPerfilUsuario { get; set; }

        public string? NombrePerfil { get; set; }

        public string? DescripcionPerfil { get; set; }

    }
}
