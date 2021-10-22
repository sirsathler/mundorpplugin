using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Rocket.Core.Logging;

namespace MundoRP
{
	public class DataConnector
	{
		private string host = "127.0.0.1";
		private string port = "3306";
		private string user = "root";
		private string password = "mundorpadmin1";

		public bool ExecuteSQLQuery(string query)
		{
			try
			{
				MySqlConnection sqlConn = new MySqlConnection("server=" + host + ";port="+ port + ";User Id=" + user + ";password=" + password);
				sqlConn.Open();
				MySqlCommand sqlCmd = new MySqlCommand(query, sqlConn);
				sqlCmd.CommandType = System.Data.CommandType.Text;
				sqlCmd.ExecuteNonQuery();
				sqlConn.Close();
				return true;
			}
			catch(Exception ex)
			{
				Logger.Log(ex.ToString());
				return false;
			} 
		}
	}
}
