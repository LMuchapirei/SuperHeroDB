using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperHeroDB.Server.Data;
using SuperHeroDB.Shared;

namespace SuperHeroDB.Server.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;

        // Inject DbContext into SuperHeroController
        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        static List<Comic> comics = new()
        {
            new() {Name = "Marvel",Id=1},
            new() {Name = "DC",Id = 2}
        };
         static List<SuperHero> heroes = new()
        {
            new ()
            {FirstName = "Peter",LastName = "Parker",Id = 1,HeroName = "SpiderMan",Comic = new (){Name = "Marvel",Id = 1}
            },
            new ()
            {FirstName = "Bruce",LastName = "Wayne",Id = 2,HeroName = "Batman",Comic = new (){Name = "DC", Id=2}
            }
        };

         private Task<List<SuperHero>> GetDbHeroes()
         {
             return _dataContext.SuperHeroes.Include(sh => sh.Comic).ToListAsync();
         }
         // Get all SuperHeroes
         [HttpGet]
         public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
         {
             return base.Ok(await GetDbHeroes());
         }
        // public async Task<IActionResult> GetSuperHeroes()
        // {
        //     return Ok(heroes);
        // }
        
        // Get Superhero by id
        [HttpGet("{id}", Name = "GetSuperHero")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            var SuperHero = await _dataContext.SuperHeroes.Include(sh=>sh.Comic).FirstOrDefaultAsync(x => x.Id == id);
            if (SuperHero == null)
            {
                return NotFound();
            }
            return Ok(SuperHero);
        }
        
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetSuperHero(int id)
        // {
        //     var hero = heroes.FirstOrDefault(h => h.Id == id);
        //     if (hero == null)
        //         return NotFound("SuperHero not found .Too Bad 😂😂");
        //     return Ok(hero);
        // }
          
       // Create SuperHero
       [HttpPost]
        public async Task<IActionResult> CreateSuperHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            // return CreatedAtRoute("GetSuperHero", new { id = hero.Id }, hero);
            return Ok(await GetDbHeroes());
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateSuperHero(SuperHero hero)
        // {
        //     if (heroes.Count == 0)
        //         hero.Id = 1;
        //     Random rand=new();
        //     var randomId=rand.Next(heroes.Count+1,1000);
        //     hero.Id =  randomId;
        //     heroes.Add(hero);
        //     return Ok(heroes);
        // }

        // Get Comics
        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _dataContext.Comics.ToListAsync();
            return comics;
        }
        // [HttpGet("comics")]
        // public async Task<IActionResult> GetComics()
        // {
        //     return Ok(comics);
        // }

       // Update SuperHero
       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSuperHero(int id, SuperHero hero)
        {
            var superHero = await _dataContext.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }
            superHero.FirstName = hero.FirstName;
            superHero.LastName = hero.LastName;
            superHero.HeroName = hero.HeroName;
            superHero.Comic = hero.Comic;
            await _dataContext.SaveChangesAsync();
            return Ok(await GetDbHeroes());
        }
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateSuperHero(int id, SuperHero hero)
        // {
        //     var dbhero = heroes.FirstOrDefault(h => h.Id == id);
        //     if (dbhero == null)
        //         return NotFound("Super Hero wasn't found Too Bad . :(");
        //     heroes[heroes.IndexOf(dbhero)] = hero;
        //     return Ok(heroes);
        // }

        // Delete SuperHero 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await GetDbHeroes());
        }
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteSuperHero(int id)
        // {
        //     var dbhero = heroes.FirstOrDefault(h => h.Id == id);
        //     if (dbhero == null)
        //         return NotFound("Super Hero wasn't found Too Bad . :(");
        //     heroes.Remove(dbhero);
        //     return Ok(heroes);
        // }
    }
}