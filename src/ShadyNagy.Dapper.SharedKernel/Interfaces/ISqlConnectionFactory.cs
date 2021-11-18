using System.Data;

namespace ShadyNagy.Dapper.SharedKernel.Interfaces
{
  public interface ISqlConnectionFactory
  {
    IDbConnection GetOpenConnection();
  }
}
