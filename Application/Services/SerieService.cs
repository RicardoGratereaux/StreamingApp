using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SerieService
    {
        private readonly SerieRepository _serieRepository;

        public SerieService(ApplicationContext dbContext)
        {
            _serieRepository = new(dbContext);
        }

        public async Task Update(SaveSerieViewModel vm)
        {
            Serie serie = new();
            serie.Id = vm.Id;
            serie.Name = vm.Name;
            serie.ImageUrl = vm.ImageUrl;
            serie.VideoUrl = vm.VideoUrl;
            serie.ProducerId = vm.ProducerId;
            serie.GenreId = vm.GenreId;

            await _serieRepository.UpdateAsync(serie);
        }

        public async Task<SaveSerieViewModel> Add(SaveSerieViewModel vm)
        {
            Serie serie = new();
            serie.Name = vm.Name;
            serie.ImageUrl = vm.ImageUrl;
            serie.VideoUrl = vm.VideoUrl;
            serie.GenreId = vm.GenreId;
            serie.ProducerId = vm.ProducerId;

            await _serieRepository.AddAsync(serie);

            SaveSerieViewModel serieVm = new();

            serieVm.Id = vm.Id;
            serieVm.Name = vm.Name;
            serieVm.ImageUrl = vm.ImageUrl;
            serieVm.VideoUrl = vm.VideoUrl;
            serieVm.ProducerId = vm.ProducerId;
            serieVm.GenreId = vm.GenreId;

            return serieVm;
        }

        public async Task Delete(int id)
        {
            var serie = await _serieRepository.GetByIdAsync(id);
            await _serieRepository.DeleteAsync(serie);
        }

        public async Task<SaveSerieViewModel> GetByIdSaveViewModel(int id)
        {
            var serie = await _serieRepository.GetByIdAsync(id);

            SaveSerieViewModel vm = new();
            vm.Id = serie.Id;
            vm.Name = serie.Name;
            vm.ImageUrl = serie.ImageUrl;
            vm.VideoUrl = serie.VideoUrl;
            vm.GenreId = serie.GenreId;
            vm.ProducerId = serie.ProducerId;

            return vm;
        }

        public async Task<List<SerieViewModel>> GetAllViewModel()
        {
            var serieList = await _serieRepository.GetAllWithIncludeAsync(new List<string> { "Genre", "Producer" });
            return serieList.Select(serie => new SerieViewModel
            {
                Id = serie.Id,
                Name = serie.Name,
                ImageUrl = serie.ImageUrl,
                VideoUrl = serie.VideoUrl,
                GenreName = serie.Genre.Name,
                ProducerName = serie.Producer.Name
            }).ToList();
        }

        public async Task<List<SerieViewModel>> GetAllViewModelWithFilters(FilterSerieViewModel filters)
        {
            var serieList = await _serieRepository.GetAllWithIncludeAsync(new List<string> { "Genre", "Producer" });

            var listViewModels = serieList.Select(serie => new SerieViewModel
            {
                Id = serie.Id,
                Name = serie.Name,
                ImageUrl = serie.ImageUrl,
                VideoUrl = serie.VideoUrl,
                GenreName = serie.Genre.Name,
                ProducerName = serie.Producer.Name,
                GenreId = serie.Genre.Id,
                ProducerId = serie.Producer.Id,
                SerieName = serie.Name
            }).ToList();

            if(filters.ProducerId != null)
            {
                listViewModels = listViewModels.Where(serie => serie.ProducerId == filters.ProducerId.Value).ToList();
            }
            if (filters.SerieName != null)
            {
                listViewModels = listViewModels.Where(serie => serie.SerieName == filters.SerieName).ToList();
            }

            return listViewModels;
        }
    }
}
