namespace DapperMvcDemo.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(String spName, P parameters,
        string connectionId = "conn");

        Task SaveData<T>(string spName, T parameters, string
        connectionId = "conn");
    }
}