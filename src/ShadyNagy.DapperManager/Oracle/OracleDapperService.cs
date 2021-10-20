﻿using Dapper;
using ShadyNagy.DapperManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShadyNagy.DapperManager.Oracle
{
    public class OracleDapperService: IDapperService
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ISyntexBuilder _oracleSyntexBuilder;

        public OracleDapperService(ISqlConnectionFactory sqlConnectionFactory, ISyntexBuilder oracleSyntexBuilder)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _oracleSyntexBuilder = oracleSyntexBuilder;
            _oracleSyntexBuilder.Reset();
        }

        public async Task<List<T>> GetFromAsync<T>(string name)
        {
            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                return (await connection.QueryAsync<T>(_oracleSyntexBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<T>> GetColumnsFromAsync<T>(string name, List<string> columnsNames)
        {
            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                var oracleSyntexBuilder = new OracleSyntexBuilder();

                return (await connection.QueryAsync<T>(oracleSyntexBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<T> GetFrom<T>(string name)
        {
            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                return connection.Query<T>(_oracleSyntexBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<T> GetColumnsFrom<T>(string name, List<string> columnsNames)
        {
            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                var oracleSyntexBuilder = new OracleSyntexBuilder();

                return connection.Query<T>(oracleSyntexBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
