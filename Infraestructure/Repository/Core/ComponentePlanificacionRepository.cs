﻿using Core.Dtos.Core;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Infraestructure.Configuration.Conexion.LoginDB;
using Infraestructure.Configuration.Zeus.Core;
using Infraestructure.Repository.Generico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Core
{
    public class ComponentePlanificacionRepository : GenericCoreRepository<ComponentesPlanificacionDto>, IComponentePlanificacionRepository
    {
        public ComponentePlanificacionRepository(ZeusCoreContext context) : base(context)
        {
        }

        public List<ComponentesPlanificacionDto> getPlanificacion(int idperiodo, int idplanestudio, int idmodalidadplanificacio)
        {
            ComponentesPlanificacionDto planificacion = new ComponentesPlanificacionDto();
            List<ComponentesPlanificacionDto> listaPlanificacion = new List<ComponentesPlanificacionDto>();
            DataSet ds_planificacion = Conexion.BuscarZEUS_ds("PLANIFICACION pln\r\ninner join MATERIA mat on pln.ID_MATERIA=mat.ID_MATERIA\r\ninner join PLAN_ESTUDIO ple on pln.ID_PLAN_ESTUDIO=ple.ID_PLAN_ESTUDIO\r\ninner join PERIODO per on per.ID_PERIODO=pln.ID_PERIODO\r\ninner join MODALIDAD_PE mo on mo.ID_MODALIDAD_PE=pln.ID_MODALIDAD_PLANIFICACION\r\ninner join EMPLEADO pro on pro.DNI_EMP=pln.DNI_PROFESORC\r\ninner join MODALIDAD_PERIODO mope on per.ID_MODALIDAD=mope.ID_MODALIDAD\r\ninner join ESPACIOS_FISICOS ef on ef.ID_ESPACIOS_FISICOS=pln.ID_ESPACIOS_FISICOS", "pln.ID_NIVEL_ESTUDIO,pln.ID_MALLA,mat.HORAS_SEMESTRALES_MATERIA,mat.CREDITOS_MATERIA,per.ID_PERIODO,per.CODIGO_PERIODO,mat.ID_MATERIA,mat.CODIGO_MATERIA,mat.NOMBRE_MATERIA,PARALELO,CUPO,pln.DNI_PROFESORC,pro.NOMBRES_EMP,pro.APELLIDO_EMP,pln.ID_MODALIDAD_PLANIFICACION,mo.NOMBRE_MODALIDAD_PE,mope.NOMBRE_MODALIDADP,ef.CODIGO_ESPACIOS_FISICOS, \r\nple.ID_PLAN_ESTUDIO,ple.CODIGO_PLAN_ESTUDIO_MALLA,ID_PLANIFICACION", "where per.ID_PERIODO=" + idperiodo + " and ple.ID_PLAN_ESTUDIO=" + idplanestudio + " and pln.ID_MODALIDAD_PLANIFICACION=" + idmodalidadplanificacio);
            if (ds_planificacion.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds_planificacion.Tables[0].Rows)
                {
                    planificacion.ID_NIVEL_ESTUDIO = Convert.ToInt32(row["ID_NIVEL_ESTUDIO"].ToString());
                    planificacion.ID_MALLA = Convert.ToInt32(row["ID_MALLA"].ToString());
                    planificacion.HORAS_SEMESTRALES_MATERIA = Convert.ToInt32(row["HORAS_SEMESTRALES_MATERIA"].ToString());
                    planificacion.CREDITOS_MATERIA = float.Parse(row["CREDITOS_MATERIA"].ToString());


                    planificacion.ID_PLANIFICACION = Convert.ToInt32(row["ID_PLANIFICACION"].ToString());
                    planificacion.ID_PERIODO = Convert.ToInt32(row["ID_PERIODO"].ToString());
                    planificacion.CODIGO_PERIODO = row["CODIGO_PERIODO"].ToString();
                    planificacion.ID_MATERIA = Convert.ToInt32(row["ID_MATERIA"].ToString());
                    planificacion.CODIGO_MATERIA = row["CODIGO_MATERIA"].ToString();
                    planificacion.NOMBRE_MATERIA = row["NOMBRE_MATERIA"].ToString();
                    planificacion.PARALELO = row["PARALELO"].ToString();
                    planificacion.CUPO = Convert.ToInt32(row["CUPO"].ToString());
                    planificacion.DNI_PROFESORC = row["DNI_PROFESORC"].ToString();
                    planificacion.NOMBRES_EMP = row["NOMBRES_EMP"].ToString();
                    planificacion.ID_MODALIDAD_PLANIFICACION = Convert.ToInt32(row["ID_MODALIDAD_PLANIFICACION"].ToString());
                    planificacion.NOMBRE_MODALIDAD_PE = row["NOMBRE_MODALIDAD_PE"].ToString();
                    planificacion.NOMBRE_MODALIDADP = row["NOMBRE_MODALIDADP"].ToString();
                    planificacion.CODIGO_ESPACIOS_FISICOS = row["CODIGO_ESPACIOS_FISICOS"].ToString();
                    planificacion.ID_PLAN_ESTUDIO = Convert.ToInt32(row["ID_PLAN_ESTUDIO"].ToString());
                    listaPlanificacion.Add(planificacion);
                    planificacion = new ComponentesPlanificacionDto();
                }
            }
            return listaPlanificacion;
        }

        public bool updatehorasPlanificacion(PlanificacionMallaDto planificacionDto, int idplanificacion)
        {
            bool result = Conexion.ActualizarZeus("PLANIFICACION",
                "ID_MALLA=" + planificacionDto.ID_MALLA + "," +
                "ID_MATERIA=" + planificacionDto.ID_MATERIA + "," +
                "ID_NIVEL_ESTUDIO=" + planificacionDto.ID_NIVEL_ESTUDIO + "," +
                "ID_PLAN_ESTUDIO=" + planificacionDto.ID_PLAN_ESTUDIO + "," +
                "ID_PERIODO=" + planificacionDto.ID_PERIODO + "," +
                "ID_PERIODICIDAD=" + planificacionDto.ID_PERIODICIDAD + "," +
                "ID_TIPO_PERIODO=" + planificacionDto.ID_TIPO_PERIODO + "," +
                "ID_MODALIDAD=" + planificacionDto.ID_MODALIDAD + "," +
                "ID_ESTADO_PERIODO=" + planificacionDto.ID_ESTADO_PERIODO + "," +
                "ID_MODALIDAD_PLANIFICACION=" + planificacionDto.ID_MODALIDAD_PLANIFICACION + "," +
                "ID_PERIODICIDAD_PLANIFICACION=" + planificacionDto.ID_PERIODICIDAD_PLANIFICACION + "," +
                "DNI_PROFESORC='" + planificacionDto.DNI_PROFESORC + "'," +
                "ID_TIPO_COMPONENTE=" + planificacionDto.ID_TIPO_COMPONENTE + "," +
                "PARALELO='" + planificacionDto.PARALELO + "'," +
                "ID_ESPACIOS_FISICOS=" + planificacionDto.ID_ESPACIOS_FISICOS + "," +
                "CUPO=" + planificacionDto.CUPO
                , " WHERE ID_PLANIFICACION=" + idplanificacion);
            return result;
        }
    }
}
