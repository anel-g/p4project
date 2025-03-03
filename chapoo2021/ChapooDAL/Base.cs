﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ChapooDAL
{
    public class Base
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public Base()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ChapooDatabase"].ConnectionString);
            adapter = new SqlDataAdapter();
        }
        protected SqlConnection OpenConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        private void CloseConnection()
        {
            conn.Close();
        }
        /* For Insert/Update/Delete Queries with transaction */
        protected void ExecuteEditTranQuery(String query, SqlParameter[] sqlParameters, SqlTransaction sqlTransaction)
        {
            SqlCommand command = new SqlCommand(query, conn, sqlTransaction);
            try
            {
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Print.ErrorLog(e);
                throw;
            }
        }
        /* For Insert/Update/Delete Queries */
        protected void ExecuteEditQuery(String query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /* For Select Queries */
        protected DataTable ExecuteSelectQuery(String query, params SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            DataTable dataTable;
            DataSet dataSet = new DataSet();

            try
            {
                command.Connection = OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException e)
            {
                // Print.ErrorLog(e);
                return null;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }
    }
}