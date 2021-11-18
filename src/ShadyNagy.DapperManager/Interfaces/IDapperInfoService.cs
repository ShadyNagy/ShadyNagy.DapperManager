using System.Collections.Generic;
using System.Threading.Tasks;
using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Interfaces
{
  public interface IDapperInfoService
  {
    Task<List<DatabaseTable>> GetAllTablesNamesAsync();
    Task<List<DatabaseTable>> GetAllTablesAsync();
    Task<List<DatabaseTable>> GetAllViewsNamesAsync();
    Task<List<DatabaseTable>> GetAllViewsAsync();
    Task<List<DatabaseColumn>> GetAllColumnsAsync(string tableName);
  }
}
