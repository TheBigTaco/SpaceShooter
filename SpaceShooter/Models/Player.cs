using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SpaceShooter.Models
{
  public class Player
  {
    public int Id {get; private set;}

    public Player(int id = 0)
    {
      Id = id;
    }
  }
}
