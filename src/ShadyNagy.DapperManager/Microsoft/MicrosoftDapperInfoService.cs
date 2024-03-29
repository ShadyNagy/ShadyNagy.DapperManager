﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Microsoft
{
  public class MicrosoftDapperInfoService: IDapperInfoService
  {
    private static string TABLE_TO_GET_TABLES = "INFORMATION_SCHEMA.TABLES";
    private static string TABLE_TO_GET_VIEWS = "INFORMATION_SCHEMA.VIEWS";
    private static string TABLE_TO_GET_COLUMNS = "INFORMATION_SCHEMA.COLUMNS";

    private readonly IDapperService _microsoftDapperService;
    private readonly ILogger<MicrosoftDapperInfoService> _logger;

    public MicrosoftDapperInfoService(IDapperService microsoftDapperService, ILogger<MicrosoftDapperInfoService> logger)
    {
      _microsoftDapperService = microsoftDapperService;
      _logger = logger;
    }

    public async Task<List<DatabaseTable>> GetAllTablesNamesAsync()
    {
      return await _microsoftDapperService.GetFromAsync<DatabaseTable>(TABLE_TO_GET_TABLES);
    }

    public async Task<List<DatabaseTable>> GetAllTablesAsync()
    {
      var tables = await _microsoftDapperService.GetFromAsync<DatabaseTable>(TABLE_TO_GET_TABLES);
      for (var i = 0; i < tables.Count; i++)
      {
        var columns = await _microsoftDapperService.GetFromAsync<DatabaseColumn>(TABLE_TO_GET_COLUMNS);
        tables[i].Columns.AddRange(columns);  
      }

      return tables;
    }

    public async Task<List<DatabaseTable>> GetAllViewsNamesAsync()
    {
      return await _microsoftDapperService.GetFromAsync<DatabaseTable>(TABLE_TO_GET_TABLES);
    }

    public async Task<List<DatabaseTable>> GetAllViewsAsync()
    {
      var views = await _microsoftDapperService.GetFromAsync<DatabaseTable>(TABLE_TO_GET_VIEWS);
      for (var i = 0; i < views.Count; i++)
      {
        var columns = await _microsoftDapperService.GetFromAsync<DatabaseColumn>(TABLE_TO_GET_COLUMNS);
        views[i].Columns.AddRange(columns);
      }

      return views;
    }

    public async Task<List<DatabaseColumn>> GetAllColumnsAsync(string tableName)
    {
      return (await _microsoftDapperService.GetFromAsync<DatabaseColumn>(TABLE_TO_GET_COLUMNS)).Where(x => 
        string.Equals(x.TableName, tableName, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }
  }
}
