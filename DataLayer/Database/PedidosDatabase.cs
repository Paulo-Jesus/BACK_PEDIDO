using DataLayer.Common;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;

namespace DataLayer.Database
{
    public class PedidosDatabase
    {
        private readonly string ConnectionString = Environment.GetEnvironmentVariable(DLVariables.ConnectionString)!;
        private readonly SqlConnection Connection = new();
        private readonly Response Response = new();

        public Response DatabaseConnection
        {
            get
            {
                try
                {
                    Connection.ConnectionString = ConnectionString;
                    Connection.Open();

                    Response.Code = ResponseType.Success;
                    Response.Message = DLMessages.ConexionExitosa;
                    Response.Data = Connection;
                } 
                catch (Exception ex)
                {
                    Response.Code = ResponseType.Error;
                    Response.Message = ex.Message;
                    Response.Data = ex.Data;

                    return Response;
                }
                return Response;
            }
        }
    }
}
