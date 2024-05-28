using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> DeleteUser(User user);
        Task<Result<User>> Login(string username, string password);
        Task<Result> Registration(string username, string password);
        Task<Result> UpdateUser(User user);
    }
}
