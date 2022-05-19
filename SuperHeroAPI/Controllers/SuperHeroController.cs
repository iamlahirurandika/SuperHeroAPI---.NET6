using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id = 1,
                Name = "Captain America",
                FirstName = "Steve",
                LastName =  "Rogers",
                Place = "New York",
            },
            new SuperHero
            {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName =  "Stark",
                Place = "Long Island",
            }
        };

        private readonly DataContext _context;

        //constructor 
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //return Ok(heroes);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found"); 
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            //adding a hero to db
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //Change a hero 
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            /*
            var hero = heroes.Find(h => h.Id == request.Id);
            if (hero == null)
                return BadRequest("Hero not found");
            */

            //Update to the db 
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero Not Found!");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

           // heroes.Add(hero);
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //Delete 
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {

            //var hero = heroes.Find(h => h.Id == id);
            //delete from db 
            var dbHero = await _context.SuperHeroes.FindAsync(id); 
            if (dbHero == null)
                return BadRequest("Hero not found");

            //heroes.Remove(hero);
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(dbHero);
        } 

    }
        
       
}
