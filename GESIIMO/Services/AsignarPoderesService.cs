using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class AsignarPoderesService : IAsignarPoderesService
    {
        private readonly SqlConfiguration configuration;

        public AsignarPoderesService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<AsignarPoderes>> GetApartamentoRepresentado(int idTorre, int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asignar_Poderes";

            var parametros = new
            {
                Nombre = "ApartamentoRepresentado",
                Id1 = idTorre,
                Id2 = 0,
                idAsamblea = idAsamblea
            };

            return await db.QueryAsync<AsignarPoderes>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<AsignarPoderes>> GetApartamentoRepresentante(int idPropietario, int idTorre, int idAsamblea )
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asignar_Poderes";

            var parametros = new
            {
                Nombre = "ApartamentoRepresentante",
                Id1 = idPropietario,
                Id2 = idTorre,
                idAsamblea = idAsamblea
            };

            return await db.QueryAsync<AsignarPoderes>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<AsignarPoderes> GetRepresentante(int idApartamento)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asignar_Poderes";

            var parametros = new
            {
                Nombre = "Representado",
                Id1 = idApartamento,
                Id2 = 0,
                idAsamblea = 0
            };

            return await db.QuerySingleAsync<AsignarPoderes>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}
