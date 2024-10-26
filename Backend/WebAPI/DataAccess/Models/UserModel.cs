using WebAPI.DataAccess.Lib;

namespace WebAPI.DataAccess.Models;

public class UserModel : ISqlModel
{
    public long ID { get; set; }
    public long Key { get; set; }

    public List<ISqlValue> GetSqlValues()
    {
        List<ISqlValue> values = [
            SqlValueFactory.CreateSqlValue(nameof(ID), ID),
            SqlValueFactory.CreateSqlValue(nameof(Key), Key)
        ];
        
        return values;
    }
}