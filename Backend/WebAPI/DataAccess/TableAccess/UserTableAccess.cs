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

    public UserModel CreateUser()
    {
        long id = GetRandomLong();
        List<UserModel> users = _connection.SelectAll<UserModel>(_table);

        while(users.Where(x => x.ID == id).Count() != 0)
        {
            id = GetRandomLong();
        }

        UserModel user = new UserModel() { ID = id, UserKey = GetRandomLong() };

        _connection.Insert(_table, user);

        return user;
    }

    public bool ValidUser(UserModel user)
    {
        return _connection.Contains<UserModel>(_table, user.GetSqlValues());
    }
}