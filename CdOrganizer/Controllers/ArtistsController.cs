using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using CdOrganizer.Models;

namespace CdOrganizer.Controllers
{
  public class ArtistsController : Controller
  {

    [HttpGet("/artists")]
    public ActionResult Index()
    {
      List<Artist> allArtists = Artist.GetAll();
      return View(allArtists);
    }

    [HttpGet("/artists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/artists")]
    public ActionResult Create(string artistName)
    {
      Artist newArtist = new Artist(artistName);
      List<Artist> allArtists = Artist.GetAll();
      return View("Index", allArtists);
    }

    [HttpGet("/artists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Artist selectedArtist = Artist.Find(id);
      List<Album> artistAlbums= selectedArtist.GetAlbums();
      model.Add("artist", selectedArtist);
      model.Add("albums", artistAlbums);
      return View(model);
    }

    [HttpGet("/artists/search")]
    public ActionResult Search()
    {
      return View();
    }

    [HttpPost("/artists/search")]
    public ActionResult Search(string searchName)
    {
     Dictionary<string, object> model = new Dictionary<string, object>();
      Artist foundArtist = Artist.Search(searchName);
      List<Album> foundAlbums = foundArtist.GetAlbums();
      model.Add("artist", foundArtist);
      model.Add("albums", foundAlbums);
      return View("Show", model);
    }

    

    // This one creates new albums for a given artist, not new artists:
    [HttpPost("/artists/{artistId}/albums")]
    public ActionResult Create(int artistId, string albumDescription)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Artist foundArtist = Artist.Find(artistId);
      Album newAlbum = new Album(albumDescription);
      foundArtist.AddAlbum(newAlbum);
      List<Album> artistAlbums = foundArtist.GetAlbums();
      model.Add("albums", artistAlbums);
      model.Add("artist", foundArtist);
      return View("Show", model);
    }

  }
}