using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using World.Models;
using MySql.Data.MySqlClient;

namespace World.Controllers
{
  public class CitiesController : Controller
  {

    [HttpGet("/cities")]
    public ActionResult Index()
    {
        List<City> allCities = City.GetAll();
        return View(allCities);
    }
    [HttpGet("/cities/search")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/cities")]
    public ActionResult SearchCities(string citySearch)
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city WHERE name='" + citySearch + "';";
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
      return View("Index", allCities);
    }
  }
}
