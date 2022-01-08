namespace TodoListApiSqlite.Repositories;

public abstract class AbstractRepository
{
    protected readonly IServiceScopeFactory serviceScopeFactory;

    public AbstractRepository(IServiceScopeFactory scopeFactory)
    {
        serviceScopeFactory = scopeFactory;
    }
}