using System;
using AutoMapper;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Конфигурация AutoMapper.
    /// </summary>
    public class AutoMapperConfiguration
    {
        private readonly MapperConfiguration mapperConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConfiguration"/> class.
        /// </summary>
        public AutoMapperConfiguration()
        {
            this.mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Article, Contracts.Commands.ArticlesApplicationService.Entities.Article>();
                cfg.CreateMap<Comment, Contracts.Commands.ArticlesApplicationService.Entities.Comment>();
                cfg.CreateMap<User, Contracts.Commands.UsersApplicationService.Entities.User>();
            });

            this.mapperConfiguration.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Возвращает экземпляр AutoMapper.
        /// </summary>
        /// <returns>Экземпляр маппера.</returns>
        public IMapper GetMapper()
        {
            return this.mapperConfiguration.CreateMapper();
        }
    }
}
