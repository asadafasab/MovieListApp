using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieListApp.Data;
using MovieListApp.Models;

namespace MovieListApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly DataContext _context;

    public MovieController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> Get()
    {
        return Ok(await _context.Movies.ToListAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return BadRequest("Movie not found");
        }
        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Data not valid");
        }
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return Ok(await _context.Movies.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Movie>>> UpdateMovie(Movie movie)
    {
        var movieDB = await _context.Movies.FindAsync(movie.Id);
        if (!ModelState.IsValid)
        //TODO ValidateAntiForgeryToken
        {
            return BadRequest("Data not valid");
        }
        if (movieDB == null)
        {
            return BadRequest("Not found");
        }
        movieDB.Name = movie.Name;
        movieDB.ProductionYear = movie.ProductionYear;

        await _context.SaveChangesAsync();
        return Ok(await _context.Movies.ToListAsync());
    }

    [HttpDelete]
    public async Task<ActionResult<List<Movie>>> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return BadRequest("Not found");
        }
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return Ok(await _context.Movies.ToListAsync());
    }
}
