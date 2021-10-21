using System;
using System.Data;
using LivrariaComLog.Infra.Settings;
using MySql.Data.MySqlClient;

namespace LivrariaComLog.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public MySqlConnection MySqlConnection { get; set; }

        public DataContext(AppSettings appSettings)
        {
            MySqlConnection = new MySqlConnection(appSettings.ConnectionString);
            MySqlConnection.Open();
        }


        public void Dispose()
        {
            if (MySqlConnection.State != ConnectionState.Closed)
                MySqlConnection.Close();
        }
    }
}