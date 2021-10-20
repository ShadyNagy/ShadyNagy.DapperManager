using System.Data;

namespace ShadyNagy.DapperManager.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
