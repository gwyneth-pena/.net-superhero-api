using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context) {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null) return BadRequest("Hero not found.");
            return Ok(hero);
        }


        [HttpPost()]
        public async Task<ActionResult<SuperHero>> AddSuperHero(SuperHero superhero)
        {
            _context.Add <SuperHero>(superhero);
            await _context.SaveChangesAsync();

            return Ok(superhero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHeroById(int id, SuperHero superhero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero is null) return BadRequest("Hero not found.");
            dbHero.Name = superhero.Name;
            dbHero.LastName = superhero.LastName;
            dbHero.FirstName = superhero.FirstName;
            await _context.SaveChangesAsync();

            return Ok(dbHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSuperHeroById(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero is null) return BadRequest("Hero not found.");
            _context.Remove<SuperHero>(dbHero);
            await _context.SaveChangesAsync();

            return Ok("Hero removed!");
        }




    }
}
