using AutoMapper;
using MovieManager.Services.DomainModels;
using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Services.Mappings
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            // db model to view model
            CreateMap<DbMovie, MovieViewModel>()
            .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, source => source.MapFrom(src => src.Title))
            .ForMember(dest => dest.Director, source => source.MapFrom(src => src.Director))
            .ForMember(dest => dest.Image, source => source.MapFrom(src => src.Image))
            .ForMember(dest => dest.Year, source => source.MapFrom(src => src.Year))
            .ForMember(dest => dest.ActorList, source => source.MapFrom(src => SplitAndTrimByDelimiter(src.Actors)))
            .ForMember(dest => dest.Actors, source => source.MapFrom(src => src.Actors));

            //view model to db model
            CreateMap<MovieViewModel, DbMovie>()
            .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, source => source.MapFrom(src => src.Title))
            .ForMember(dest => dest.Director, source => source.MapFrom(src => src.Director))
            .ForMember(dest => dest.Image, source => source.MapFrom(src => src.Image))
            .ForMember(dest => dest.Year, source => source.MapFrom(src => src.Year))
            .ForMember(dest => dest.Actors, source => source.MapFrom(src => src.Actors));
        }

        private static List<string> SplitAndTrimByDelimiter(string inputString)
        {
            string delimiter = ",";
            return inputString.Split(delimiter, StringSplitOptions.None)
                .Select(actor => actor.Trim())
                .ToList();
        }
    }
}
