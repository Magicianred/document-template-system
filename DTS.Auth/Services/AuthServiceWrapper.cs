using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using DTS.API.Services;
using Microsoft.IdentityModel.Tokens;

namespace DTS.Auth.Services
{
    public class AuthServiceWrapper : IAuthServiceWrapper
    {
        private IQueryHandlerAsync<LoginQuery, SecurityToken> _login;
        private ICommandHandlerAsync<SignInCommand> _signIn;
        private IRepositoryWrapper _repository;

        public IQueryHandlerAsync<LoginQuery, SecurityToken> Login
        {
            get
            {
                if (_login == null)
                {
                    _login = new LoginQueryHandler(_repository);
                }
                return _login;
            }
        }

        public ICommandHandlerAsync<SignInCommand> SignIn
        {
            get
            {
                if (_signIn == null)
                {
                    _signIn = new SignInCommandHandler(_repository);
                }
                return _signIn;
            }
        }

        public AuthServiceWrapper(IRepositoryWrapper repositoryWrapper)
        {
            this._repository = repositoryWrapper;
        }

    }
}
