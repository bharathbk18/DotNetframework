using Assessmentapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessmentapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext moviedbcontext;
        public MoviesController(MoviesDbContext movieDbContext)
        {
            moviedbcontext = movieDbContext;
        }



        [HttpGet]
        public IEnumerable<Movies> GetMovies()
        {
            return moviedbcontext.Movie.ToList();
        }
        [HttpGet("GetMovieById")]
        public Movies GetMovieById(int Id)
        {
            return moviedbcontext.Movie.Find(Id);
        }

        [HttpPost("InsertMovie")]
        public IActionResult InsertMovie([FromBody] Movies Movie)
        {
            if (Movie.Id.ToString() != "")
            {

                moviedbcontext.Movie.Add(Movie);
                moviedbcontext.SaveChanges();
                return Ok("Movie details saved successfully");
            }
            else
                return BadRequest();
        }
        [HttpPut("UpdateMovie")]
        public IActionResult UpdateTutorial([FromBody] Movies Movie)
        {
            if (Movie.Id.ToString() != "")
            {
                //update tutorial set name=tutorial.name , desc=tutorial.desc , fees=tutorial.fees , publish=tutorial.publish where id=tutorial.id
                moviedbcontext.Entry(Movie).State = EntityState.Modified;
                moviedbcontext.SaveChanges();
                return Ok("Movies Detail Updated successfully");
            }
            else
                return BadRequest();
        }
        [HttpDelete("DeleteMovie")]
public IActionResult DeleteTutorial(int tutorialId)
    {
      
        var result = moviedbcontext.Movie.Find(tutorialId);
            moviedbcontext.Movie.Remove(result);
            moviedbcontext.SaveChanges();
        return Ok("Deleted Movies successfully");
    }


}
}
