using WebAPI.DataAccess.Lib;
using WebAPI.DataAccess.Models;

namespace WebAPI.DataAccess.TableAccess;

public class UserTableAccess
{

    private RandanonConnection _connection;
    private string _table;
    private Random _rnd;

    public UserTableAccess()
    {
        _connection = new RandanonConnection();
        _table = "randanon_user";
        _rnd = new Random();
    }

    private long GetRandomLong()
    {
        return _rnd.NextInt64(1928, 9999999);
    }

    public async Task<UserModel> CreateUser()
    {
        long id = GetRandomLong();
        List<UserModel> users = await _connection.SelectAllAsync<UserModel>(_table);

        while(users.Where(x => x.ID == id).Count() != 0)
        {
            id = GetRandomLong();
        }

        UserModel user = new UserModel() { ID = id, UserKey = GetRandomLong() };

        _connection.InsertAsync(_table, user);

        return user;
    }

    public async Task<bool> ValidUser(UserModel user)
    {
        return await _connection.ContainsAsync<UserModel>(_table, user);
    }
}