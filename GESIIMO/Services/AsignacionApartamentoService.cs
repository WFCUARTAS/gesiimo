using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class AsignacionApartamentoService : IAsignacionApartamentoService
    {
        private readonly SqlConfiguration configuration;

        public AsignacionApartamentoService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<AsignacionApartamento>> GetAsignacionApartamentos(int? idPropietario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_ApartamentoPropietario_GET_MS";

            var parametros = new
            {
                IdPropietario = idPropietario
            };

            return await db.QueryAsync<AsignacionApartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<AsignacionApartamento> GetAsignacionApartamento(int idApartamentoPropietario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_ApartamentoPropietario_GET_DT";

            var parametros = new
            {
                IdApartamentoPropietario = idApartamentoPropietario
            };

            return await db.QuerySingleAsync<AsignacionApartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetAsignacionApartamentos(AsignacionApartamento apartamentopropietario)
        {

            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_ApartamentoPropietario_SET";

            var parametros = new
            {
                IdApartamentoPropietario = apartamentopropietario.IdApartamentoPropietario,
                IdApartamento = apartamentopropietario.IdApartamento,
                IdPropietario = apartamentopropietario.IdPropietario,
                Action = apartamentopropietario.IdApartamentoPropietario == 0 ? "POST" : "PUT"
            };

            try
            {
                return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (System.Exception e)
            {
                throw;
            }
        }

        public async Task<int> DelAsignacionApartamentos(int idApartamentoPropietario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_ApartamentoPropietario_SET";

            var parametros = new
            {
                IdApartamentoPropietario = idApartamentoPropietario,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetCambioEstado(AsignacionApartamento apartamentopropietario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_ApartamentoPropietario_SET";

            var parametros = new
            {
                IdApartamentoPropietario = apartamentopropietario.IdApartamentoPropietario,
                IdApartamento = apartamentopropietario.IdApartamento,
                IdPropietario = apartamentopropietario.IdPropietario,
                Action = "DEL"
            };

            try
            {
                return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
    }
}