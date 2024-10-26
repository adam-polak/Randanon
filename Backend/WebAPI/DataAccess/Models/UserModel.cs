using WebAPI.DataAccess.Lib;

namespace WebAPI.DataAccess.Models;

public class UserModel : ISqlModel
{
    public long ID { get; set; }
    public long Key { get; set; }

    public List<ISqlValue> GetSqlValues()
    {
        throw new NotImplementedException();
    }
}