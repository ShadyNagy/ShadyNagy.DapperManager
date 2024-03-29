﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShadyNagy.DapperManager.Interfaces
{
  public interface IDapperService
  {
    Task<List<T>> GetFromAsync<T>(string name);
    Task<List<T>> GetColumnsFromAsync<T>(string name, List<string> columnsNames);
    List<T> GetFrom<T>(string name);
    List<T> GetByIdsFrom<T>(string name, Dictionary<string, object> fields);
    List<T> GetByIdsFromSafe<T>(string name, Dictionary<string, string> fields, object idsObject);
    Task<List<T>> GetByFilterFromSafeAsync<T>(string name, object databaseFields, bool isRemoveNull);
    Task<List<T>> GetByIdsFromAsync<T>(string name, Dictionary<string, object> fields);
    Task<List<T>> GetByIdsFromSafeAsync<T>(string name, Dictionary<string, string> fields, object idsObject);
    List<T> GetColumnsFrom<T>(string name, List<string> columnsNames);
    Task<int> InsertAsync(string tableFullName, object toInsert);
    Task<int> InsertSafeAsync(string tableFullName, object toInsert, Dictionary<string, string> mapper);
    int Insert(string tableFullName, object toInsert);
    int InsertSafe(string tableFullName, object toInsert, Dictionary<string, string> mapper);
    Task<int> UpdateAsync(string tableFullName, Dictionary<string, object> keys, object toUpdate);
    Task<int> UpdateSafeAsync(string tableFullName, object toUpdate, Dictionary<string, string> keys,
      Dictionary<string, string> mapper);
    int Update(string tableFullName, Dictionary<string, object> keys, object toUpdate);
    int UpdateSafe(string tableFullName, object toInsert, Dictionary<string, string> keys,
      Dictionary<string, string> mapper);
  }
}
