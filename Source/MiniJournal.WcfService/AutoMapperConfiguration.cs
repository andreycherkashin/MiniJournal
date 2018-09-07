using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Infotecs.MiniJournal.WcfService
{
    public class AutoMapperConfiguration
    {
        private readonly MapperConfiguration mapperConfiguration;

        public AutoMapperConfiguration()
        {
            this.mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Articles.Article, DataTransferObjects.Article>();
                cfg.CreateMap<Domain.Comments.Comment, DataTransferObjects.Comment>();
                cfg.CreateMap<Domain.Users.User, DataTransferObjects.User>();
            });
            
            this.mapperConfiguration.AssertConfigurationIsValid();
        }

        public IMapper GetMapper()
        {
            return this.mapperConfiguration.CreateMapper();
        }
    }
}