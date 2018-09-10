using System;
using AutoMapper;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entites;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Application
{
    public class AutoMapperConfiguration
    {
        private readonly MapperConfiguration mapperConfiguration;

        public AutoMapperConfiguration()
        {
            this.mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Articles.Article, Article>();
                cfg.CreateMap<Domain.Comments.Comment, Comment>();
                cfg.CreateMap<Domain.Users.User, User>();
            });
            
            this.mapperConfiguration.AssertConfigurationIsValid();
        }

        public IMapper GetMapper()
        {
            return this.mapperConfiguration.CreateMapper();
        }
    }
}