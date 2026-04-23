using Microsoft.AspNetCore.SignalR.Client;
using WinFormsSignalRDemo.ChatDtos;

namespace WinFormsSignalRDemo;

public sealed class DCloudChatHubClient
{
    private HubConnection? _connection;

    public HubConnection? Connection => _connection;

    public event Action<string>? OnLog;
    public event Action<string>? OnSingleMessageRaw;
    public event Action<GroupMessageDto>? OnGroupMessage;
    public event Action<BroadcastMessageDto>? OnBroadcastMessage;
    public event Action<SystemMessageDto>? OnSystemMessage;

    public async Task ConnectAsync(string hubUrl, string encryptedAccessToken)
    {
        var finalUrl = hubUrl;
        if (!string.IsNullOrWhiteSpace(encryptedAccessToken))
        {
            finalUrl += (hubUrl.Contains('?') ? "&" : "?") +
                        "enc_auth_token=" + Uri.EscapeDataString(encryptedAccessToken);
        }

        _connection = new HubConnectionBuilder()
            .WithUrl(finalUrl)
            .WithAutomaticReconnect()
            .Build();

        _connection.Closed += async ex =>
        {
            OnLog?.Invoke("连接关闭: " + ex?.Message);
            await Task.CompletedTask;
        };

        _connection.Reconnecting += async ex =>
        {
            OnLog?.Invoke("重连中: " + ex?.Message);
            await Task.CompletedTask;
        };

        _connection.Reconnected += async id =>
        {
            OnLog?.Invoke("已重连: " + id);
            await Task.CompletedTask;
        };

        _connection.On<string>("ReceiveMessage", msg =>
        {
            OnSingleMessageRaw?.Invoke(msg);
            OnLog?.Invoke("收到单聊消息: " + msg);
        });

        _connection.On<GroupMessageDto>("ReceiveGroupMessage", dto =>
        {
            OnGroupMessage?.Invoke(dto);
            OnLog?.Invoke($"收到群消息[{dto.GroupName}] {dto.SenderUserName}: {dto.Message}");
        });

        _connection.On<BroadcastMessageDto>("ReceiveBroadcastMessage", dto =>
        {
            OnBroadcastMessage?.Invoke(dto);
            OnLog?.Invoke($"收到广播消息: {dto.SenderUserName}: {dto.Message}");
        });

        _connection.On<SystemMessageDto>("ReceiveSystemMessage", dto =>
        {
            OnSystemMessage?.Invoke(dto);
            OnLog?.Invoke($"收到系统消息[{dto.Level}] {dto.Title}: {dto.Message}");
        });

        await _connection.StartAsync();
        OnLog?.Invoke("SignalR 已连接");
    }

    public async Task DisconnectAsync()
    {
        if (_connection is null)
        {
            return;
        }

        await _connection.StopAsync();
        await _connection.DisposeAsync();
        _connection = null;
        OnLog?.Invoke("SignalR 已断开");
    }

    public async Task<string> RegisterAsync()
    {
        EnsureConnected();
        await _connection!.InvokeAsync("Register");
        return string.Empty;
    }

    public async Task<string> SendMessageAsync(SendChatMessageInput input)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("SendMessage", input);
    }

    public async Task<string> SendMessageToUserAsync(SendChatMessageInput input)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("SendMessageToUser", input);
    }

    public async Task<string> JoinGroupAsync(string groupName)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("JoinGroup", groupName);
    }

    public async Task<string> LeaveGroupAsync(string groupName)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("LeaveGroup", groupName);
    }

    public async Task<string> SendMessageToGroupAsync(SendGroupMessageInput input)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("SendMessageToGroup", input);
    }

    public async Task<string> BroadcastMessageAsync(BroadcastMessageInput input)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("BroadcastMessage", input);
    }

    public async Task<string> SendSystemMessageAsync(SendSystemMessageInput input)
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<string>("SendSystemMessage", input);
    }

    public async Task<List<OnlineUserDto>> GetOnlineUsersAsync()
    {
        EnsureConnected();
        return await _connection!.InvokeAsync<List<OnlineUserDto>>("GetOnlineUsers");
    }

    private void EnsureConnected()
    {
        if (_connection is null || _connection.State != HubConnectionState.Connected)
        {
            throw new InvalidOperationException("SignalR 尚未连接");
        }
    }
}
