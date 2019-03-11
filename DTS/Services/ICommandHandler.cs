using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Services
{
    public interface ICommandHandlerAsync<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
