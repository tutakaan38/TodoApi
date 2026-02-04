using Business.Dtos;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterDto userDto);
        Task<User> Login(string username, string password);
    }
}
