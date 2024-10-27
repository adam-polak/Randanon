using WebAPI.DataAccess.Lib;
using WebAPI.DataAccess.Models;
using WebAPI.Controllers.Models;
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

    private async Task<long> GetLargestPKey()
    {
        await _conneciton.OpenAsync();
        string query = $"SELECT * FROM {_table} WHERE {_pKeyName}=(SELECT MAX({_pKeyName}) FROM {_table});";
        ChatModel? m = ((List<ChatModel>)await _conneciton.QueryAsync<ChatModel>(query)).FirstOrDefault();
        long largest = m == null ? 0 : m.ChatNumber;
        await _conneciton.CloseAsync();
        return largest;
    }

    public async void SendChatAsync(UserModel user, string message)
    {
        message = StringSqlValue.ValidateString(message);
        ChatModel m = new ChatModel() { UserID = user.ID, ChatNumber = (await GetLargestPKey()) + 1, Message = message };
        _randConnection.InsertAsync(_table, m);
    }
}