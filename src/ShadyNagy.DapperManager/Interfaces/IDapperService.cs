using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShadyNagy.DapperManager.Interfaces
{
  public interface IDapperService
  {
    Task<List<T>> GetFromAsync<T>(string name);
    Task<List<T>> GetColumnsFromAsync<T>(string name, List<string> columnsNames);
    List<T> GetFrom<T>(string name);
    List<T> GetColumnsFrom<T>(string name, List<string> columnsNames);
    Task<int> InsertAsync<T>(string tableFullName, object toInsert);
    Task<int> InsertSafeAsync<T>(string tableFullName, object toInsert, Dictionary<string, string> mapper);
    int Insert<T>(string tableFullName, object toInsert);
    int InsertSafe<T>(string tableFullName, object toInsert, Dictionary<string, string> mapper);
  }
}
