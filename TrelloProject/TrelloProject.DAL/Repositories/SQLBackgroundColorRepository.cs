using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLBackgroundColorRepository : IBackgroundColorDTORepository
    {
        private readonly TrelloDbContext _trelloDbContext;
        private IMapper mapper;
        private MapperConfiguration config;

        public SQLBackgroundColorRepository(TrelloDbContext trelloDbContext)
        {
            _trelloDbContext = trelloDbContext;
        }

        private BackgroundColorDTO MapToBgColorDTO(BackgroundColor backgroundColor)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<BackgroundColor, BackgroundColorDTO>());
            mapper = config.CreateMapper();
            BackgroundColorDTO backgroundColorDTO = mapper.Map<BackgroundColor, BackgroundColorDTO>(backgroundColor);
            return backgroundColorDTO;
        }

        private BackgroundColor MapToBgColor(BackgroundColorDTO backgroundColorDTO)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<BackgroundColorDTO, BackgroundColor>());
            mapper = config.CreateMapper();
            BackgroundColor backgroundColor = mapper.Map<BackgroundColorDTO, BackgroundColor>(backgroundColorDTO);
            return backgroundColor;
        }

        public bool bgExists = false;
        public bool DoesBackgroundColorExist(int id)
        {
            BackgroundColor backgroundColor = _trelloDbContext.BackgroundColors.Find(id);
            if(backgroundColor == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                bgExists = true;
                return bgExists;
            }
        }

        public List<BackgroundColorDTO> GetAllBgColors()
        {
            IEnumerable<BackgroundColor> bgColors = _trelloDbContext.BackgroundColors.ToList();

            List<BackgroundColorDTO> bgColorsDTO = new List<BackgroundColorDTO>();
            foreach(var bgColor in bgColors)
            {
                BackgroundColorDTO bgColorDTO = MapToBgColorDTO(bgColor);
                bgColorsDTO.Add(bgColorDTO);
            }
            return bgColorsDTO;
        }
    }
}
