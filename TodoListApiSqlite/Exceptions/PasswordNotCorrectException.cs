using System;

namespace TodoListApiSqlite.Exceptions;

public class PasswordNotCorrectException : Exception
{
    public PasswordNotCorrectException()
    {
    }

    public PasswordNotCorrectException(string message)
        : base(message)
    {
    }

    public PasswordNotCorrectException(string message, Exception inner)
        : base(message, inner)
    {
    }
}