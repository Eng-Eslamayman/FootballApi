using AutoMapper;
using FootballApi.Core.DTOs;
using FootballApi.Core.Models;
using FootballApi.EF;
using FootballApi.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.EF.AutoMapperConfig
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        { 
            CreateMap<ClubDTO, Club>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.LeagueId, src => src.MapFrom(x => x.LeagueId))
                .ForMember(dist => dist.NumberOfTournaments, src => src.MapFrom(x => x.NumberOfTournaments))
                .ForMember(dist => dist.Players, src => src.Ignore())
                .ForMember(dist => dist.League, src => src.Ignore())
                .ReverseMap();

            CreateMap<League, LeagueDTO>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.NumberOfTeams, src => src.MapFrom(x => x.NumberOfTeams))
                .ForMember(dist => dist.FoundedYear, src => src.MapFrom(x => x.FoundedYear))
                .ForMember(dist => dist.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dist => dist.Country, src => src.MapFrom(x => x.Country))
                .ReverseMap();

            CreateMap<Player, PlayerDTO>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.ClubId, src => src.MapFrom(x => x.ClubId))
                .ForMember(dist => dist.Nationality, src => src.MapFrom(x => x.Nationality))
                .ForMember(dist => dist.Age, src => src.MapFrom(x => x.Age))
                .ReverseMap();

            CreateMap<ApplicationUser, RegisterDTO>()
               .ForMember(dist => dist.FirstName, src => src.MapFrom(x => x.FirstName))
               .ForMember(dist => dist.LastName, src => src.MapFrom(x => x.LastName))
               .ForMember(dist => dist.Email, src => src.MapFrom(x => x.Email))
               .ForMember(dist => dist.UserName, src => src.MapFrom(x => x.UserName))
               .ReverseMap();
        }

    }
}
