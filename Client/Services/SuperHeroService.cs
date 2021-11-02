using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using SuperHeroDB.Shared;

namespace SuperHeroDB.Client.Services
{
    public class SuperHeroService:ISuperHeroService
    {
        private readonly HttpClient _httpClient;
        public List<Comic> Comics { get; set; } = new List<Comic>();
        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public event Action OnChange;
        public async Task<List<SuperHero>> UpdateSuperHero(int id, SuperHero hero)
        {
            
            var result = await _httpClient.PutAsJsonAsync<SuperHero>($"api/superhero/{id}",hero);
             Heroes= await result.Content.ReadFromJsonAsync<List<SuperHero>>();
             OnChange.Invoke();
            return Heroes;
        }

        public async  Task<List<SuperHero>> DeleteSuperHero(int id)
        {
            var response =await _httpClient.DeleteAsync($"api/superhero/{id}");
             Heroes=await  response.Content.ReadFromJsonAsync<List<SuperHero>>();
            OnChange.Invoke();
            return Heroes;
        }

        public SuperHeroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SuperHero>> GetSuperHeroes()
        {
            Heroes= await _httpClient.GetFromJsonAsync<List<SuperHero>>("api/superhero");
            return Heroes;
        }

        public async Task<SuperHero> GetSuperHero(int id)
        {
            return await _httpClient.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
        }

        public async Task<List<SuperHero>> CreateSuperHero(SuperHero hero)
        {
            var result = await _httpClient.PostAsJsonAsync<SuperHero>("api/superhero",hero);
             Heroes= await result.Content.ReadFromJsonAsync<List<SuperHero>>();
             OnChange.Invoke();
            return Heroes;
        }

        public async Task GetComics()
        {
            Comics= await _httpClient.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
        }
    }
}