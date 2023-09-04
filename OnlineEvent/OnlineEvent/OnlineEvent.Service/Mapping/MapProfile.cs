using AutoMapper;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model.AppUserEventModels;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.CategoryModels;
using OnlineEvent.Model.CityModels;
using OnlineEvent.Model.EventModels;

namespace OnlineEvent.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<City, CityModel>().ReverseMap();
            CreateMap<City, CityWithEventModel>();

            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, CategoryWithEventModel>();
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<UpdateCategoryModel, Category>();

            CreateMap<Event, EventModel>().ReverseMap();
            CreateMap<UpdateEventModel, Event>();


            CreateMap<AppUser, AppUserModel>().ReverseMap();

            CreateMap<AppUserEventModel, AppUserEvent>();

        }
    }
}
