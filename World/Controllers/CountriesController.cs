using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using World.Models;
using MySql.Data.MySqlClient;

namespace World.Controllers
{
  public class CountriesController : Controller
  {

    [HttpGet("/countries")]
    public ActionResult Index()
    {
        List<Country> allCountries = Country.GetAll();
        return View(allCountries);
    }
    [HttpGet("/countries/search")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/countries")]
    public ActionResult SearchCountries(string countrySearch)
    {
      List<Country> allCountries = new List<Country> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM country WHERE name='" + countrySearch + "';";
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
      if (conn != null)
      {
        conn.Dispose();
      }
      return View("Index", allCountries);
    }
  }
}
