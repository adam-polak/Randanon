
namespace WebAPI.DataAccess.Lib;

public class SqlSB : ISqlStringBuilder
{
    public string DeleteString(string table, ISqlValue value)
    {
        string sqlStr = $"DELETE FROM {table} WHERE {value.GetSqlEqualsString()};";
        return sqlStr;
    }

    public string DeleteString(string table, List<ISqlValue> values)
    {
        string sqlStr = $"DELETE FROM {table} WHERE ";

        for(int i = 0; i < values.Count; i++)
        {
            ISqlValue value = values.ElementAt(i);

            sqlStr += value.GetSqlEqualsString();

            if(i == values.Count - 1) sqlStr += ";";
            else sqlStr += " AND ";
        }

        return sqlStr;
    }

    public string InsertString(string table, ISqlModel model)
    {
        string sqlStr = $"INSERT INTO {table} (";
        List<ISqlValue> values = model.GetSqlValues();

        for(int i = 0; i < values.Count; i++)
        {
            ISqlValue value = values.ElementAt(i);
            sqlStr += value.GetLabelString();
            if(i == values.Count - 1) sqlStr += ")";
            else sqlStr += ", ";
        }

        sqlStr += " VALUES (";

        for(int i = 0; i < values.Count; i++)
        {
            ISqlValue value = values.ElementAt(i);
            sqlStr += value.GetValueSqlString();

            if(i == values.Count - 1) sqlStr += ");";
            else sqlStr += ", ";
        }

        return sqlStr;
    }

    public string SelectString(string table, ISqlValue value)
    {
        string sqlStr = $"SELECT * FROM {table} WHERE {value.GetSqlEqualsString()};";
        return sqlStr;
    }

    public string SelectString(string table, List<ISqlValue> values)
    {
        string sqlStr = $"SELECT * FROM {table} WHERE ";

        for(int i = 0; i < values.Count; i++)
        {
            ISqlValue value = values.ElementAt(i);
            sqlStr += value.GetSqlEqualsString();

            if(i == values.Count - 1) sqlStr += ";";
            else sqlStr += " AND ";
        }

        return sqlStr;
    }

    public string SelectAllString(string table)
    {
        return $"SELECT * FROM {table};";
    }
}