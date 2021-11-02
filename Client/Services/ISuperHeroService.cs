using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHeroDB.Shared;

namespace SuperHeroDB.Client.Services
{
    public interface ISuperHeroService
    {
        Task<List<SuperHeroDB.Shared.SuperHero>> GetSuperHeroes();
        Task<SuperHeroDB.Shared.SuperHero> GetSuperHero(int id);
        Task<List<SuperHero>> CreateSuperHero(SuperHero hero);
        Task GetComics();
        List<Comic> Comics { get; set; }
        List<SuperHero> Heroes { get; set; }
        event Action OnChange;
        Task<List<SuperHero>> UpdateSuperHero(int id, SuperHero hero);
        Task<List<SuperHero>> DeleteSuperHero(int id);
    }
}