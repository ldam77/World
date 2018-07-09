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
      string commandText = @"SELECT * FROM country WHERE name='" + countrySearch + "';";
      return View("Index", Country.MakeList(commandText));
    }
  }
}
