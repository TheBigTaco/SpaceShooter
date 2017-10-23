using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rename.Models
{
  public class Class
  {
    public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM _database;";
        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
