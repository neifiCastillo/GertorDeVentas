﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SisVenttas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Datos
{
    public class FCliente
    {
        public static DataSet GetAll()
        {
           SqlParameter[] dbParams = new SqlParameter[]
              {
                  
              };
          return FDBHelper.ExecuteDataSet("usp_Data_FCliente_GetAll", dbParams);

        }
        public static int Insertar(Cliente cliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
               {
                    FDBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, cliente.Nombre),
                    FDBHelper.MakeParam("@Apellido", SqlDbType.VarChar, 0, cliente.Apellido),
                    FDBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, cliente.Direccion),
                    FDBHelper.MakeParam("@Dni", SqlDbType.Int, 0, cliente.Dni),
                    FDBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 0, cliente.Telefono),

               };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCliente_Insertar", dbParams));

        }
        public static int Actualizar(Cliente cliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
               {
                    FDBHelper.MakeParam("@Id", SqlDbType.Int, 0, cliente.Id),
                    FDBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, cliente.Nombre),
                    FDBHelper.MakeParam("@Apellido", SqlDbType.VarChar, 0, cliente.Apellido),
                    FDBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, cliente.Direccion),
                    FDBHelper.MakeParam("@Dni", SqlDbType.Int, 0, cliente.Dni),
                    FDBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 0, cliente.Telefono),

               };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCliente_Actualizar", dbParams));

        }

        public static int Eliminar (Cliente cliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
               {
                    FDBHelper.MakeParam("@Id", SqlDbType.Int, 0, cliente.Id),
                   
               };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCliente_Eliminar", dbParams));

        }
    }
}
