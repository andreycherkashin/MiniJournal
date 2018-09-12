using System;
using System.Threading.Tasks;
using AutoMapper;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Реализует высокоуровненвый интерфейс для работы с пользователями.
    /// </summary>
    internal class UsersService : IUsersService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserFactory userFactory;
        private readonly IUserDomainService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/> class.
        /// </summary>
        /// <param name="userService"><see cref="IUserDomainService"/>IUserDomainService.</param>
        /// <param name="userFactory"><see cref="IUserFactory"/>IUserFactory.</param>
        /// <param name="mapper"><see cref="IMapper"/>Mapper.</param>
        /// <param name="unitOfWork"><see cref="IUnitOfWork"/>Unit of work.</param>
        public UsersService(
            IUserDomainService userService,
            IUserFactory userFactory,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.userService = userService;
            this.userFactory = userFactory;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение
        /// <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>.
        /// </exception>
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        public async Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User user = await this.userService.GetUserByNameAsync(request.UserName);

            return new GetUserByNameResponse(this.mapper.Map<Contracts.UsersApplicationService.Entities.User>(user));
        }

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User user = await this.userFactory.CreateAsync(request.UserName);

            await this.userService.CreateUserAsync(user);

            await this.unitOfWork.SaveChangesAsync();

            return new CreateNewUserResponse(true);
        }
    }
}
