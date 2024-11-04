using WebAPI.DataAccess.Lib;
using WebAPI.DataAccess.Models;
using System.Data.Common;
using Dapper;

namespace WebAPI.DataAccess.TableAccess;

public class ChatTableAccess
{
    private RandanonConnection _randConnection;
    private DbConnection _conneciton;
    private string _table;
    private string _pKeyName;

    public ChatTableAccess()
    {
        _conneciton = DbConnectionFactory.CreateDbConnection();
        _randConnection = new RandanonConnection();
        _table = "randanon_chat";
        _pKeyName = "ChatNumber";
    }

    private long GetLargestPKey()
    {
        _conneciton.Open();
        string query = $"SELECT * FROM {_table} WHERE {_pKeyName}=(SELECT MAX({_pKeyName}) FROM {_table});";
        ChatModel? m = ((List<ChatModel>)_conneciton.Query<ChatModel>(query)).FirstOrDefault();
        long largest = m == null ? 0 : m.ChatNumber;
        _conneciton.Close();
        return largest;
    }

    public int GetChatCount()
    {
        _conneciton.Open();
        List<int> response = (List<int>)_conneciton.Query<int>($"SELECT COUNT(*) FROM {_table};");
        _conneciton.Close();
        
        return response.FirstOrDefault();
    }

    public List<ChatModel> GetChatsAbove(int x)
    {
        _conneciton.Open();
        List<ChatModel> chats = (List<ChatModel>)_conneciton.Query<ChatModel>($"SELECT * FROM {_table} WHERE ChatNumber > {x};");
        _conneciton.Close();
        return chats;
    }

    public List<ChatModel> GetAllChats()
    {
        List<ChatModel> chats = _randConnection.SelectAll<ChatModel>(_table);
        return chats;
    }

    public  void SendChat(UserModel user, string message)
    {
        message = StringSqlValue.ValidateString(message);
        ChatModel m = new ChatModel() { UserID = user.ID, ChatNumber = (GetLargestPKey()) + 1, Message = message };
        _randConnection.Insert(_table, m);
    }

    public bool DeleteChat(UserModel user, long chatNumber)
    {
        ChatModel m = new ChatModel() { UserID = user.ID, ChatNumber = chatNumber, Message = "" };
        List<ISqlValue> values = m.GetSqlValues()
                                    .Where(
                                        (x) => 
                                            x.GetValueSqlString().Equals($"{user.ID}") 
                                            || x.GetValueSqlString().Equals($"{chatNumber}")
                                        ).ToList();
        return _randConnection.Delete<ChatModel>(_table, values);
    }
}