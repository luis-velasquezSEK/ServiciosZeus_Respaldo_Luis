﻿using Core.Entidades.Core;
using Core.Interfaces.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Core
{
    public interface IUnidadOrganizacionalCurricularRepository : IGenericRepository<UnidadOrganizacionCurricular>
    {
        Task<List<UnidadOrganizacionCurricular>> GetAllUOCActivasAsync();

        Task<List<UnidadOrganizacionCurricular>> GetByCoduocAsync(string coduoc);
    }
}
