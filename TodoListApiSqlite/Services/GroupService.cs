using TodoListApiSqlite.Models;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;

namespace TodoListApiSqlite.Services;

public class GroupService
{
    private readonly GroupRepository _groupRepository;
    
    public GroupService(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Group Create(GroupModel model)
    {
        Group group = new Group();
        SetCommonFields(model, group);
        _groupRepository.CreateGroup(group);

        return group;
    }

    private Group SetCommonFields(GroupModel model, Group group)
    {
        group.Name = model.Name;

        return group;
    }
    
}