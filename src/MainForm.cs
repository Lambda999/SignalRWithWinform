using System.Net.Http.Json;
using System.Text.Json;
using WinFormsSignalRDemo.ChatDtos;
using System.Net.Http.Json;
using System.Text.Json;

namespace WinFormsSignalRDemo;

public partial class MainForm : Form
{
    private readonly DCloudChatHubClient _chatClient = new();
    private readonly string _settingsFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    private string _accessToken = "";

    public MainForm()
    {
        InitializeComponent();

        _chatClient.OnLog += AppendLog;
        _chatClient.OnSingleMessageRaw += msg => AppendLog("[单聊事件] " + msg);
        _chatClient.OnGroupMessage += dto => AppendLog($"[群消息][{dto.GroupName}] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnBroadcastMessage += dto => AppendLog($"[广播事件] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnSystemMessage += dto => AppendLog($"[系统消息事件][{dto.Level}] {dto.Title}: {dto.Message}");

        LoadSettings();
        SyncHubUrlFromApiBase();
    }

    private void LoadSettings()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(_settingsFilePath);
            var cfg = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions.Default);
            if (cfg?.Client is null)
            {
                return;
            }

            txtHubUrl.Text = cfg.Client.HubUrl;
            txtApiBaseUrl.Text = cfg.Client.ApiBaseUrl;
            txtEncToken.Text = cfg.Client.EncAuthToken;
            txtTenantId.Text = cfg.Client.TenantId;
            txtUserId.Text = cfg.Client.TargetUserId;
            txtUserName.Text = cfg.Client.SenderUserName;
            txtTenancyName.Text = cfg.Client.SenderTenancyName;
            txtLoginUserName.Text = cfg.Client.LoginUserName;
            txtLoginPassword.Text = cfg.Client.LoginPassword;
            txtGroupName.Text = cfg.Client.GroupName;
            txtSystemTitle.Text = cfg.Client.SystemTitle;
            txtMessage.Text = cfg.Client.Message;
            _accessToken = cfg.Client.AccessToken;
            SyncHubUrlFromApiBase();
        }
        catch (Exception ex)
        {
            AppendLog("读取 appsettings.json 失败: " + ex.Message);
        }
    }

    private void SaveSettings()
    {
        try
        {
            var cfg = new AppSettings
            {
                Client = new ClientSettings
                {
                    HubUrl = txtHubUrl.Text.Trim(),
                    ApiBaseUrl = txtApiBaseUrl.Text.Trim(),
                    EncAuthToken = txtEncToken.Text.Trim(),
                    TenantId = txtTenantId.Text.Trim(),
                    TargetUserId = txtUserId.Text.Trim(),
                    SenderUserName = txtUserName.Text.Trim(),
                    SenderTenancyName = txtTenancyName.Text.Trim(),
                    LoginUserName = txtLoginUserName.Text.Trim(),
                    LoginPassword = txtLoginPassword.Text,
                    GroupName = txtGroupName.Text.Trim(),
                    SystemTitle = txtSystemTitle.Text.Trim(),
                    Message = txtMessage.Text,
                    AccessToken = _accessToken
                }
            };

            var json = JsonSerializer.Serialize(cfg, JsonOptions.Indented);
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception ex)
        {
            AppendLog("保存 appsettings.json 失败: " + ex.Message);
        }
    }

    private async void btnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            btnLogin.Enabled = false;
            var loginResult = await AuthenticateAsync(
                txtApiBaseUrl.Text.Trim(),
                txtLoginUserName.Text.Trim(),
                txtLoginPassword.Text);

            txtEncToken.Text = loginResult.EncryptedAccessToken;
            txtUserId.Text = loginResult.UserId.ToString();
            _accessToken = loginResult.AccessToken;
            AppendLog($"登录成功，Token 有效秒数: {loginResult.ExpireInSeconds}");
        }
        catch (Exception ex)
        {
            AppendLog("登录失败: " + ex);
            MessageBox.Show(this, ex.Message, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private async void btnConnect_Click(object? sender, EventArgs e)
    {
        try
        {
            btnConnect.Enabled = false;
            SyncHubUrlFromApiBase();
            await _chatClient.ConnectAsync(txtHubUrl.Text.Trim(), txtEncToken.Text.Trim());
            AppendLog("连接成功");
        }
        catch (Exception ex)
        {
            AppendLog("连接失败: " + ex);
            MessageBox.Show(this, ex.Message, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnConnect.Enabled = true;
        }
    }

    private async void btnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            btnLogin.Enabled = false;
            var loginResult = await AuthenticateAsync(
                txtHubUrl.Text.Trim(),
                txtLoginUserName.Text.Trim(),
                txtLoginPassword.Text);

            txtEncToken.Text = loginResult.EncryptedAccessToken;
            txtUserId.Text = loginResult.UserId.ToString();
            AppendLog($"登录成功，Token 有效秒数: {loginResult.ExpireInSeconds}");
        }
        catch (Exception ex)
        {
            AppendLog("登录失败: " + ex);
            MessageBox.Show(this, ex.Message, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private async void btnDisconnect_Click(object? sender, EventArgs e)
    {
        try
        {
            await _chatClient.DisconnectAsync();
        }
        catch (Exception ex)
        {
            AppendLog("断开失败: " + ex);
        }
    }

    private async void btnRegister_Click(object? sender, EventArgs e)
    {
        await InvokeWithResult(async () => await _chatClient.RegisterAsync(), "Register");
    }

    private async void btnSendMessage_Click(object? sender, EventArgs e)
    {
        await InvokeWithResult(async () => await _chatClient.SendMessageAsync(BuildSendChatMessageInput()), "SendMessage");
    }

    private async void btnSendUser_Click(object? sender, EventArgs e)
    {
        await InvokeWithResult(async () => await _chatClient.SendMessageToUserAsync(BuildSendChatMessageInput()), "SendMessageToUser");
    }

    private async void btnGetOnlineUsers_Click(object? sender, EventArgs e)
    {
        try
        {
            var users = await GetOnlineUsersByApiAsync();
            lstOnlineUsers.Items.Clear();
            foreach (var user in users.Where(u => u.UserId.HasValue))
            {
                lstOnlineUsers.Items.Add(new OnlineUserListItem(user));
            }

            AppendLog($"GetOnlineUsers 成功，在线用户数: {users.Count}");
        }
        catch (Exception ex)
        {
            AppendLog("GetOnlineUsers 异常: " + ex);
            MessageBox.Show(this, ex.Message, "GetOnlineUsers", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnGetMyOnlineFriends_Click(object? sender, EventArgs e)
    {
        try
        {
            var friends = await GetMyOnlineFriendsByApiAsync();
            AppendLog($"GetMyOnlineFriends 成功，在线好友数: {friends.Count}");
            foreach (var friend in friends.Take(20))
            {
                AppendLog($"  - {friend.UserName} ({friend.UserId}) | {friend.ConnectionId}");
            }
        }
        catch (Exception ex)
        {
            AppendLog("GetMyOnlineFriends 异常: " + ex);
            MessageBox.Show(this, ex.Message, "GetMyOnlineFriends", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnIsUserOnline_Click(object? sender, EventArgs e)
    {
        try
        {
            var userId = ParseRequiredGuid(txtUserId.Text, "Target UserId");
            var online = await IsUserOnlineByApiAsync(userId);
            AppendLog($"IsUserOnline 成功: {userId} => {(online ? "在线" : "离线")}");
        }
        catch (Exception ex)
        {
            AppendLog("IsUserOnline 异常: " + ex);
            MessageBox.Show(this, ex.Message, "IsUserOnline", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSendUsers_Click(object? sender, EventArgs e)
    {
        try
        {
            var selected = lstOnlineUsers.SelectedItems.Cast<OnlineUserListItem>().ToList();
            if (selected.Count == 0)
            {
                throw new InvalidOperationException("请先在在线用户列表里选择至少 1 个用户");
            }

            foreach (var user in selected)
            {
                var input = BuildSendChatMessageInput();
                input.UserId = user.UserId;
                await _chatClient.SendMessageToUserAsync(input);
            }

            AppendLog($"已向 {selected.Count} 个在线用户发送消息");
        }
        catch (Exception ex)
        {
            AppendLog("发送给勾选用户 异常: " + ex);
            MessageBox.Show(this, ex.Message, "发送给勾选用户", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnJoinGroup_Click(object? sender, EventArgs e)
    {
        await InvokeWithResult(async () => await _chatClient.JoinGroupAsync(txtGroupName.Text.Trim()), "JoinGroup");
    }

    private async void btnLeaveGroup_Click(object? sender, EventArgs e)
    {
        await InvokeWithResult(async () => await _chatClient.LeaveGroupAsync(txtGroupName.Text.Trim()), "LeaveGroup");
    }

    private async void btnSendGroup_Click(object? sender, EventArgs e)
    {
        var input = new SendGroupMessageInput
        {
            GroupName = txtGroupName.Text.Trim(),
            Message = txtMessage.Text,
            SenderUserName = txtUserName.Text.Trim(),
            SenderUserId = Guid.TryParse(txtUserId.Text.Trim(), out var uid) ? uid : null,
            SenderTenantId = ParseNullableGuid(txtTenantId.Text)
        };

        await InvokeWithResult(async () => await _chatClient.SendMessageToGroupAsync(input), "SendMessageToGroup");
    }

    private async void btnBroadcast_Click(object? sender, EventArgs e)
    {
        var input = new BroadcastMessageInput
        {
            Message = txtMessage.Text,
            SenderUserName = txtUserName.Text.Trim(),
            SenderUserId = Guid.TryParse(txtUserId.Text.Trim(), out var uid) ? uid : null,
            SenderTenantId = ParseNullableGuid(txtTenantId.Text)
        };

        await InvokeWithResult(async () => await _chatClient.BroadcastMessageAsync(input), "BroadcastMessage");
    }

    private async void btnSendSystem_Click(object? sender, EventArgs e)
    {
        var input = new SendSystemMessageInput
        {
            TenantId = ParseNullableGuid(txtTenantId.Text),
            UserId = ParseRequiredGuid(txtUserId.Text, "Target UserId"),
            Title = txtSystemTitle.Text.Trim(),
            Message = txtMessage.Text,
            Level = "info"
        };

        await InvokeWithResult(async () => await _chatClient.SendSystemMessageAsync(input), "SendSystemMessage");
    }

    private SendChatMessageInput BuildSendChatMessageInput()
    {
        return new SendChatMessageInput
        {
            TenantId = ParseNullableGuid(txtTenantId.Text),
            UserId = ParseRequiredGuid(txtUserId.Text, "Target UserId"),
            Message = txtMessage.Text,
            TenancyName = txtTenancyName.Text.Trim(),
            UserName = txtUserName.Text.Trim(),
            ProfilePictureId = null
        };
    }

    private async Task InvokeWithResult(Func<Task<string>> action, string actionName)
    {
        try
        {
            var result = await action();
            if (string.IsNullOrWhiteSpace(result))
            {
                AppendLog($"{actionName} 成功");
            }
            else
            {
                AppendLog($"{actionName} 返回: {result}");
            }
        }
        catch (Exception ex)
        {
            AppendLog($"{actionName} 异常: {ex}");
            MessageBox.Show(this, ex.Message, actionName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private Guid? ParseNullableGuid(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        return Guid.Parse(text.Trim());
    }

    private static Guid ParseRequiredGuid(string? text, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException(fieldName + " 不能为空");
        }

        return Guid.Parse(text.Trim());
    }

    private void AppendLog(string message)
    {
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(new Action(() => AppendLog(message)));
            return;
        }

        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
    }

    private static async Task<LoginResult> AuthenticateAsync(string apiBaseUrl, string username, string password)
    {
        if (string.IsNullOrWhiteSpace(apiBaseUrl))
        {
            throw new InvalidOperationException("Api Base Url 不能为空");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException("Login UserName 不能为空");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException("Login Password 不能为空");
        }

        var normalizedBaseUrl = NormalizeApiBaseUrl(apiBaseUrl);
        var authUrl = $"{normalizedBaseUrl}api/TokenAuth/Authenticate";

        using var http = new HttpClient();
        var response = await http.PostAsJsonAsync(authUrl, new LoginRequest
        {
            UserName = username,
            Password = password
        });
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var apiResult = JsonSerializer.Deserialize<ApiResponse<LoginResult>>(json, JsonOptions.Default)
                        ?? throw new InvalidOperationException("登录接口返回为空");

        if (!apiResult.Success || apiResult.Result is null)
        {
            throw new InvalidOperationException("登录接口返回失败: " + json);
        }

        return apiResult.Result;
    }

    private async Task<List<OnlineUserDto>> GetOnlineUsersByApiAsync()
    {
        var result = await PostApiAsync<JsonElement>("/api/services/app/Chat/GetOnlineUsers", null);
        return ParseOnlineUsers(result);
    }

    private async Task<List<OnlineUserDto>> GetMyOnlineFriendsByApiAsync()
    {
        var result = await PostApiAsync<JsonElement>("/api/services/app/Chat/GetMyOnlineFriends", null);
        return ParseOnlineUsers(result);
    }

    private async Task<bool> IsUserOnlineByApiAsync(Guid userId)
    {
        var result = await PostApiAsync<JsonElement>("/api/services/app/Chat/IsUserOnline", new { userId });
        return result.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Object when result.TryGetProperty("isOnline", out var isOnline) => isOnline.GetBoolean(),
            _ => bool.TryParse(result.ToString(), out var online) && online
        };
    }

    private async Task<T> PostApiAsync<T>(string path, object? payload)
    {
        if (string.IsNullOrWhiteSpace(_accessToken))
        {
            throw new InvalidOperationException("请先登录，获取 accessToken 后再调用 API");
        }

        var normalizedBaseUrl = NormalizeApiBaseUrl(txtApiBaseUrl.Text.Trim());
        var apiUrl = $"{normalizedBaseUrl}{path.TrimStart('/')}";

        using var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        request.Content = JsonContent.Create(payload ?? new { });

        using var http = new HttpClient();
        using var response = await http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var apiResult = JsonSerializer.Deserialize<ApiResponse<T>>(json, JsonOptions.Default)
                        ?? throw new InvalidOperationException("API 返回为空: " + path);
        if (!apiResult.Success || apiResult.Result is null)
        {
            throw new InvalidOperationException("API 返回失败: " + json);
        }

        return apiResult.Result;
    }

    private void txtApiBaseUrl_TextChanged(object? sender, EventArgs e)
    {
        SyncHubUrlFromApiBase();
    }

    private void SyncHubUrlFromApiBase()
    {
        txtHubUrl.Text = BuildHubUrlFromApiBase(txtApiBaseUrl.Text);
    }

    private static string BuildHubUrlFromApiBase(string apiBaseUrl)
    {
        return NormalizeApiBaseUrl(apiBaseUrl) + "signalr-chat";
    }

    private static string NormalizeApiBaseUrl(string apiBaseUrl)
    {
        var trimmed = (apiBaseUrl ?? "").Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            throw new InvalidOperationException("Api Base Url 不能为空");
        }

        if (!trimmed.EndsWith('/'))
        {
            trimmed += "/";
        }

        return trimmed;
    }

    private static List<OnlineUserDto> ParseOnlineUsers(JsonElement result)
    {
        var users = new List<OnlineUserDto>();
        JsonElement sourceArray;
        if (result.ValueKind == JsonValueKind.Array)
        {
            sourceArray = result;
        }
        else if (result.ValueKind == JsonValueKind.Object &&
                 (result.TryGetProperty("items", out var items) || result.TryGetProperty("Items", out items)) &&
                 items.ValueKind == JsonValueKind.Array)
        {
            sourceArray = items;
        }
        else
        {
            return users;
        }

        foreach (var item in sourceArray.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object)
            {
                continue;
            }

            var dto = new OnlineUserDto
            {
                UserId = TryGetGuid(item, "userId"),
                UserName = TryGetString(item, "userName", "friendUserName", "name"),
                TenantId = TryGetGuid(item, "tenantId"),
                TenancyName = TryGetString(item, "tenancyName", "tenantName"),
                ConnectionId = TryGetString(item, "connectionId")
            };
            users.Add(dto);
        }

        return users;
    }

    private static Guid? TryGetGuid(JsonElement element, params string[] names)
    {
        foreach (var name in names)
        {
            if (!element.TryGetProperty(name, out var prop))
            {
                continue;
            }

            if (prop.ValueKind == JsonValueKind.String && Guid.TryParse(prop.GetString(), out var g))
            {
                return g;
            }
        }

        return null;
    }

    private static string TryGetString(JsonElement element, params string[] names)
    {
        foreach (var name in names)
        {
            if (element.TryGetProperty(name, out var prop) && prop.ValueKind == JsonValueKind.String)
            {
                return prop.GetString() ?? "";
            }
        }

        return "";
    }

    protected override async void OnFormClosing(FormClosingEventArgs e)
    {
        SaveSettings();

        if (_chatClient.Connection is not null)
        {
            try
            {
                await _chatClient.DisconnectAsync();
            }
            catch
            {
                // ignore
            }
        }

        base.OnFormClosing(e);
    }

    private sealed class OnlineUserListItem
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string TenancyName { get; }
        public string ConnectionId { get; }

        public OnlineUserListItem(OnlineUserDto user)
        {
            UserId = user.UserId ?? Guid.Empty;
            UserName = user.UserName;
            TenancyName = user.TenancyName;
            ConnectionId = user.ConnectionId;
        }

        public override string ToString()
        {
            return $"{UserName} ({UserId}) [{TenancyName}] | {ConnectionId}";
        }
    }
}

public sealed class AppSettings
{
    public ClientSettings Client { get; set; } = new();
}

public sealed class ClientSettings
{
    public string HubUrl { get; set; } = "";
    public string ApiBaseUrl { get; set; } = "http://localhost:44380/";
    public string EncAuthToken { get; set; } = "";
    public string TenantId { get; set; } = "";
    public string TargetUserId { get; set; } = "";
    public string SenderUserName { get; set; } = "demo-user";
    public string SenderTenancyName { get; set; } = "Default";
    public string LoginUserName { get; set; } = "";
    public string LoginPassword { get; set; } = "";
    public string GroupName { get; set; } = "room-1";
    public string SystemTitle { get; set; } = "系统通知";
    public string Message { get; set; } = "你好，这是一条测试消息。";
    public string AccessToken { get; set; } = "";
}

internal static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web);
    public static readonly JsonSerializerOptions Indented = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };
}
