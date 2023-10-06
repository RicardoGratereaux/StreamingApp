using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;

        public GenreService(ApplicationContext dbContext)
        {
            _genreRepository = new(dbContext);
        }

        public async Task Update(SaveGenreViewModel vm)
        {
            Genre genre = new();
            genre.Id = vm.Id;
            genre.Name = vm.Name;

            await _genreRepository.UpdateAsync(genre);
        }

        public async Task Add(SaveGenreViewModel vm)
        {
            Genre genre = new();
            genre.Name = vm.Name;

            await _genreRepository.AddAsync(genre);
        }

        public async Task Delete(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            await _genreRepository.DeleteAsync(genre);
        }

        public async Task<SaveGenreViewModel> GetByIdSaveViewModel(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            SaveGenreViewModel vm = new();
            vm.Id = genre.Id;
            vm.Name = genre.Name;

            return vm;
        }

        public async Task<List<GenreViewModel>> GetAllViewModel()
        {
            var genreList = await _genreRepository.GetAllAsync();

            return genreList.Select(genre => new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            }).ToList();
        }
    }
}
