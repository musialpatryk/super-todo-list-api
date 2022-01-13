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

    public Group Edit(GroupModel model, Group group)
    {
        SetCommonFields(model, group);
        _groupRepository.UpdateGroup(group);

        return group;
    }

    private Group SetCommonFields(GroupModel model, Group group)
    {
        group.Name = model.Name;
        group.AdministratorId = model.AdministratorId;
        return group;
    }
    
}