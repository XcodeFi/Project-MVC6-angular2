using AutoMapper;
using Graduation.Entities;
using Graduation.Models;

namespace Graduation.Infrastructure.Mapping
{
    public class DomainToViewModelMappingProfile:Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<Card, CardViewModel>()
            //   //.ForMember(vm => vm.ImageUrl, map => map.MapFrom(p => p.ImageUrl))
            //   .ForMember(vm=>vm.Tag,map=>map.MapFrom(c=>c.TextSearch.Split(',')));
            Mapper.CreateMap<Card, CardViewModel>()
                .ForMember(vm => vm.Tag, map => map.MapFrom(c => c.TextSearch.Split(',')))
                .ForMember(vm=>vm.ImageUrl,map=>map.MapFrom(c=>"images/cms/news/"+c.ImageUrl));

            Mapper.CreateMap<Category, CateViewModel>()
                  .ForMember(vm => vm.ImageUrl, map => map.MapFrom(a => "images/cms/CROSSCARDS/" + a.ImageUrl))
                  ;
           
        }
    }
}
