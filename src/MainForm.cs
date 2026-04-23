using System.Net.Http.Json;
using System.Text.Json;
using WinFormsSignalRDemo.ChatDtos;

namespace WinFormsSignalRDemo;

public partial class MainForm : Form
{
    private readonly DCloudChatHubClient _chatClient = new();
    private readonly string _settingsFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public MainForm()
    {
        InitializeComponent();

        _chatClient.OnLog += AppendLog;
        _chatClient.OnSingleMessageRaw += msg => AppendLog("[单聊事件] " + msg);
        _chatClient.OnGroupMessage += dto => AppendLog($"[群消息][{dto.GroupName}] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnBroadcastMessage += dto => AppendLog($"[广播事件] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnSystemMessage += dto => AppendLog($"[系统消息事件][{dto.Level}] {dto.Title}: {dto.Message}");

        LoadSettings();
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
                    EncAuthToken = txtEncToken.Text.Trim(),
                    TenantId = txtTenantId.Text.Trim(),
                    TargetUserId = txtUserId.Text.Trim(),
                    SenderUserName = txtUserName.Text.Trim(),
                    SenderTenancyName = txtTenancyName.Text.Trim(),
                    LoginUserName = txtLoginUserName.Text.Trim(),
                    LoginPassword = txtLoginPassword.Text,
                    GroupName = txtGroupName.Text.Trim(),
                    SystemTitle = txtSystemTitle.Text.Trim(),
                    Message = txtMessage.Text
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

    private async void btnConnect_Click(object? sender, EventArgs e)
    {
        try
        {
            btnConnect.Enabled = false;
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
            var users = await _chatClient.GetOnlineUsersAsync();
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

    private static async Task<LoginResult> AuthenticateAsync(string hubUrl, string username, string password)
    {
        if (string.IsNullOrWhiteSpace(hubUrl))
        {
            throw new InvalidOperationException("HubUrl 不能为空");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException("Login UserName 不能为空");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException("Login Password 不能为空");
        }

        var hubUri = new Uri(hubUrl);
        var apiBase = $"{hubUri.Scheme}://{hubUri.Authority}";
        var authUrl = $"{apiBase}/api/TokenAuth/Authenticate";

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

        public OnlineUserListItem(OnlineUserDto user)
        {
            UserId = user.UserId ?? Guid.Empty;
            UserName = user.UserName;
            TenancyName = user.TenancyName;
        }

        public override string ToString()
        {
            return $"{UserName} ({UserId}) [{TenancyName}]";
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
}

internal static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web);
    public static readonly JsonSerializerOptions Indented = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };
}
