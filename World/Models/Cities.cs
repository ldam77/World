using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using World;

namespace World.Models
{
  public class City
  {
    private int id;
    private string name;
    private string code;
    private string district;
    private int population;

    public City(int cityId, string cityName, string countryCode, string cityDistrict, int cityPopulation)
    {
      id = cityId;
      name = cityName;
      code = countryCode;
      district = cityDistrict;
      population = cityPopulation;
    }
    public int GetCityId()
    {
      return id;
    }
    public string GetCityName()
    {
      return name;
    }
    public string GetCountryCode()
    {
      return code;
    }
    public string GetCityDistrict()
    {
      return district;
    }
    public string GetCityPopulation()
    {
      return population;
    }
    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string code = rdr.GetString(2);
        string district = rdr.GetString(3);
        int population = rdr.GetInt32(4);
        City newCity = new City(id, name, code, district, population);
        allCities.Add(newCity);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }
  }
}
