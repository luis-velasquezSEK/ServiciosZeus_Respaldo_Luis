using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Core
{
    public class ComponentesPlanificacionDto
    {
        public int ID_NIVEL_ESTUDIO { get; set; }
        public int    ID_MALLA { get; set; }
        public int   HORAS_SEMESTRALES_MATERIA { get; set; }
        public float   CREDITOS_MATERIA { get; set; }
        public int ID_PLANIFICACION { get; set; }
        public int ID_PERIODO { get;set; }
        public string CODIGO_PERIODO { get; set; }
        public int ID_MATERIA { get; set; }
        public string CODIGO_MATERIA { get; set; }
        public string NOMBRE_MATERIA { get; set; }
        public string PARALELO { get; set; }
        public int CUPO { get; set; }
        public string DNI_PROFESORC { get; set; }
        public string NOMBRES_EMP { get; set; }
        public string APELLIDO_EMP { get; set; }
        public int ID_MODALIDAD_PLANIFICACION { get; set; }
        public string NOMBRE_MODALIDAD_PE { get; set; }
        public string NOMBRE_MODALIDADP { get; set; }
        public string CODIGO_ESPACIOS_FISICOS { get; set; }
        public int ID_PLAN_ESTUDIO { get; set; }
        public string CODIGO_PLAN_ESTUDIO_MALLA { get; set; }
    }
}
