using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperHeroDB.Shared;

namespace SuperHeroDB.Server.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
         static List<Comic> comics = new()
        {
            new() {Name = "Marvel",Id=1},
            new() {Name = "DC",Id = 2}
        };
         static List<SuperHero> heroes = new()
        {
            new ()
            {
                FirstName = "Peter",
                LastName = "Parker",
                Id = 1,
                HeroName = "SpiderMan",
                Comic = new ()
                {
                    Name = "Marvel",
                    Id = 1
                }
            },
            new ()
            {
                FirstName = "Bruce",
                LastName = "Wayne",
                Id = 2,
                HeroName = "Batman",
                Comic = new ()
                {
                    Name = "DC",
                    Id=2
                }
            }
        };
        [HttpGet]
        public async Task<IActionResult> GetSuperHeroes()
        {
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuperHero(int id)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);
            if (hero == null)
                return NotFound("SuperHero not found .Too Bad 😂😂");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperHero(SuperHero hero)
        {
            if (heroes.Count == 0)
                hero.Id = 1;
            hero.Id = heroes.Max(h => h.Id + 1);
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpGet("comics")]
        public async Task<IActionResult> GetComics()
        {
            return Ok(comics);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSuperHero(int id, SuperHero hero)
        {
            var dbhero = heroes.FirstOrDefault(h => h.Id == id);
            if (dbhero == null)
                return NotFound("Super Hero wasn't found Too Bad . :(");
            heroes[heroes.IndexOf(dbhero)] = hero;
            return Ok(heroes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var dbhero = heroes.FirstOrDefault(h => h.Id == id);
            if (dbhero == null)
                return NotFound("Super Hero wasn't found Too Bad . :(");
            heroes.Remove(dbhero);
            return Ok(heroes);
        }
    }
}