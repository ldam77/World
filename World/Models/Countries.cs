using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using World;

namespace World.Models
{
  public class Country
  {
    private string name;
    private string code;
    private string continent;
    private float area;
    private int population;
    private float gnp;

    public Country(string countryName, string countryCode, string countryContinent, float countryArea, int countryPopulation, float countryGnp)
    {
      name = countryName;
      code = countryCode;
      continent = countryContinent;
      area = countryArea;
      population = countryPopulation;
      gnp = countryGnp;
    }
    public string GetCountryName()
    {
      return name;
    }
    public string GetCountryCode()
    {
      return code;
    }
    public string GetCountryContinent()
    {
      return continent;
    }
    public float GetCountryArea()
    {
      return area;
    }
    public int GetCountryPopulation()
    {
      return population;
    }
    public float GetCountryGnp()
    {
      return gnp;
    }
    public static List<Country> GetAll()
    {
      List<Country> allCountries = new List<Country> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM country;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(1);
        string code = rdr.GetString(0);
        string continent = rdr.GetString(2);
        float area = rdr.GetFloat(4);
        int population = rdr.GetInt32(6);
        float gnp = rdr.GetFloat(8);
        Country newCountry = new Country(name, code, continent, area, population, gnp);
        allCountries.Add(newCountry);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCountries;
    }
  }
}
