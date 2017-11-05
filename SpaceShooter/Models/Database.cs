using System;
using MySql.Data.MySqlClient;
using SpaceShooter;

namespace SpaceShooter.Models
{
  public class DB
  {
    public static MySqlConnection Connection
    {
      get
      {
        return new MySqlConnection(DBConfiguration.ConnectionString);
      }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection;
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM players;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
