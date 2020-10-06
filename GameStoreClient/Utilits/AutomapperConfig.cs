using AutoMapper;
using GameStoreClient.Models;
using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreClient.Utilits
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig() {
            CreateMap<Game, GameViewModel>()
                .ForMember(x=>x.Developer,opt=>opt.MapFrom(x=>x.Developer.Name))
                .ForMember(x=>x.Genre,opt=>opt.MapFrom(x=>x.Genre.Name));

            CreateMap<GameViewModel, Game>()
                .ForMember(x=>x.Developer,opt=>opt.MapFrom(x=>new Developer { Name=x.Developer}))
                .ForMember(x=>x.Genre,opt=>opt.MapFrom(x=>new Genre { Name=x.Genre}));
            CreateMap<Genre, GenreViewModel>();
            CreateMap<GenreViewModel, Genre>();
            CreateMap<Developer, DeveloperViewModel>();
            CreateMap<DeveloperViewModel, Developer>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<AddressViewModel, Address>();

        }
    }
}