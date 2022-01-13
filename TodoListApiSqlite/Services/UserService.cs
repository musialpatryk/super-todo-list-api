using System.Security.Cryptography;
using NuGet.Packaging;
using TodoListApiSqlite.Exceptions;
using TodoListApiSqlite.Extensions;
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
        SetPassword(model.Password, user);
        _userRepository.CreateUser(user);
        return user;
    }

    public User Edit(IUserModel model, User user)
    {
        user = SetCommonFields(model, user);
        return user;
    }

    public User SetCommonFields(IUserModel model, User user)
    {
        if (model.Email != null)
        {
            user.Email = model.Email;
        }
        if (model.Name != null)
        {
            user.Name = model.Name;
        }
        user.InvitationLink = RandomStringGenerator.Generate(30);
        return user;
    }

    public User ChangePassword(PasswordChangeModel model, User user)
    {
        if (BCryptNet.HashPassword(model.OldPassword) != user.Password)
        {
            throw new PasswordNotCorrectException("Password is not correct");
        }
        SetPassword(model.OldPassword, user);
        return user;
    }

    public void SetPassword(string password, User user)
    {
        user.Password = BCryptNet.HashPassword(password);
    }
}