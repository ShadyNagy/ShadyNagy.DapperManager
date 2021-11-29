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
    Task<int> InsertAsync(string tableFullName, object toInsert);
    Task<int> InsertSafeAsync(string tableFullName, object toInsert, Dictionary<string, string> mapper);
    int Insert(string tableFullName, object toInsert);
    int InsertSafe(string tableFullName, object toInsert, Dictionary<string, string> mapper);
    Task<int> UpdateAsync(string tableFullName, object toUpdate);
    Task<int> UpdateSafeAsync(string tableFullName, object toUpdate, Dictionary<string, string> mapper);
    int Update(string tableFullName, object toUpdate);
    int UpdateSafe(string tableFullName, object toInsert, Dictionary<string, string> mapper);
  }
}
