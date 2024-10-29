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

    private async Task<long> GetLargestPKey()
    {
        await _conneciton.OpenAsync();
        string query = $"SELECT * FROM {_table} WHERE {_pKeyName}=(SELECT MAX({_pKeyName}) FROM {_table});";
        ChatModel? m = ((List<ChatModel>)await _conneciton.QueryAsync<ChatModel>(query)).FirstOrDefault();
        long largest = m == null ? 0 : m.ChatNumber;
        await _conneciton.CloseAsync();
        return largest;
    }

    public async Task<int> GetChatCount()
    {
        await _conneciton.OpenAsync();
        List<int> response = (List<int>)await _conneciton.QueryAsync<int>($"SELECT COUNT(*) FROM {_table};");
        await _conneciton.CloseAsync();
        
        return response.FirstOrDefault();
    }

    public async Task<List<ChatModel>> GetChatsAbove(int x)
    {
        await _conneciton.OpenAsync();
        List<ChatModel> chats = (List<ChatModel>)await _conneciton.QueryAsync<ChatModel>($"SELECT * FROM {_table} WHERE ChatNumber > {x};");
        await _conneciton.CloseAsync();
        return chats;
    }

    public async Task<List<ChatModel>> GetAllChats()
    {
        List<ChatModel> chats = await _randConnection.SelectAllAsync<ChatModel>(_table);
        return chats;
    }

    public async void SendChatAsync(UserModel user, string message)
    {
        message = StringSqlValue.ValidateString(message);
        ChatModel m = new ChatModel() { UserID = user.ID, ChatNumber = (await GetLargestPKey()) + 1, Message = message };
        _randConnection.InsertAsync(_table, m);
    }

    public async Task<bool> DeleteChat(UserModel user, long chatNumber)
    {
        ChatModel m = new ChatModel() { UserID = user.ID, ChatNumber = chatNumber, Message = "" };
        List<ISqlValue> values = m.GetSqlValues()
                                    .Where(
                                        (x) => 
                                            x.GetValueSqlString().Equals($"{user.ID}") 
                                            || x.GetValueSqlString().Equals($"{chatNumber}")
                                        ).ToList();
        return await _randConnection.DeleteAsync<ChatModel>(_table, values);
    }
}