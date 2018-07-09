using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using World.Models;
using MySql.Data.MySqlClient;
using System;

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
    public ActionResult SearchCities(string citySearch, string comparison, string populationSearch, string order)
    {
      int popSearch = int.Parse(populationSearch);
      string commandText = "";
      if (!string.IsNullOrWhiteSpace(Request.Form["citySearch"]))
      {
      commandText = @"SELECT * FROM city WHERE Name = '" + citySearch + "';";
      }
      else
      {
        commandText = @"SELECT * FROM city WHERE population" + comparison + popSearch + " ORDER BY population " + order + ";";
      }
      return View("Index", City.MakeList(commandText));
    }
  }
}
