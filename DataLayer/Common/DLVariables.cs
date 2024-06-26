namespace DataLayer.Common
{
    public class DLVariables
    {
        public const string ConnectionString = "ConnectionString";
        public string QueryValidateMenu = "SELECT Count(*) FROM Menu WHERE FechaInicio = " 
            + DateTime.Now.Date.ToString("yyyy-MM-dd") + " AND FechaFin = " 
            + DateTime.Now.Date.ToString("yyyy-MM-dd");
    }
}
