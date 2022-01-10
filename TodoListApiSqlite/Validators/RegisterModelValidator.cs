using TodoListApiSqlite.RequestModel;

namespace TodoListApiSqlite.Validators;

public class RegisterModelValidator
{

    public static bool Validate(RegisterModel model)
    {
        if (model.Password != model.PasswordCheck)
        {
            return false;
        }

        return true;
    }
    
}