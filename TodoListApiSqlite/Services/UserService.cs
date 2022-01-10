using System.Security.Cryptography;
using TodoListApiSqlite.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;

namespace TodoListApiSqlite.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Create(RegisterModel model)
    {
        var user = new User();
        user = SetCommonFields(model, user);
        _userRepository.CreateUser(user);
        return user;
    }

    public User SetCommonFields(RegisterModel model, User user)
    {
        user.Email = model.Email;
        user.Name = model.Name;
        user.Password = BCryptNet.HashPassword(model.Password);

        return user;
    }
}