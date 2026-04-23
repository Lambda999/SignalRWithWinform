using WinFormsSignalRDemo.ChatDtos;

namespace WinFormsSignalRDemo;

public sealed class MainForm : Form
{
    private readonly DCloudChatHubClient _chatClient = new();

    private TextBox txtHubUrl = null!;
    private TextBox txtEncToken = null!;
    private TextBox txtTenantId = null!;
    private TextBox txtUserId = null!;
    private TextBox txtUserName = null!;
    private TextBox txtTenancyName = null!;
    private TextBox txtGroupName = null!;
    private TextBox txtSystemTitle = null!;
    private TextBox txtMessage = null!;
    private TextBox txtLog = null!;

    private Button btnConnect = null!;
    private Button btnDisconnect = null!;
    private Button btnRegister = null!;
    private Button btnSendMessage = null!;
    private Button btnSendUser = null!;
    private Button btnJoinGroup = null!;
    private Button btnLeaveGroup = null!;
    private Button btnSendGroup = null!;
    private Button btnBroadcast = null!;
    private Button btnSendSystem = null!;

    public MainForm()
    {
        InitializeComponent();

        _chatClient.OnLog += AppendLog;
        _chatClient.OnSingleMessageRaw += msg => AppendLog("[单聊事件] " + msg);
        _chatClient.OnGroupMessage += dto => AppendLog($"[群消息][{dto.GroupName}] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnBroadcastMessage += dto => AppendLog($"[广播事件] {dto.SenderUserName}: {dto.Message}");
        _chatClient.OnSystemMessage += dto => AppendLog($"[系统消息事件][{dto.Level}] {dto.Title}: {dto.Message}");
    }

    private void InitializeComponent()
    {
        Text = ".NET 8 WinForms SignalR Demo";
        StartPosition = FormStartPosition.CenterScreen;
        Width = 1300;
        Height = 900;

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(10)
        };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(root);

        var top = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 4,
            AutoSize = true
        };
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        root.Controls.Add(top, 0, 0);

        top.Controls.Add(BuildConnectionGroup(), 0, 0);
        top.Controls.Add(BuildSingleGroup(), 1, 0);
        top.Controls.Add(BuildGroupGroup(), 2, 0);
        top.Controls.Add(BuildBroadcastSystemGroup(), 3, 0);

        txtLog = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            ReadOnly = true,
            Font = new Font("Consolas", 10F),
            WordWrap = false
        };
        root.Controls.Add(txtLog, 0, 1);
    }

    private GroupBox BuildConnectionGroup()
    {
        var box = new GroupBox { Text = "连接", Dock = DockStyle.Fill, Padding = new Padding(10), AutoSize = true };
        var panel = NewFieldsPanel();

        txtHubUrl = AddTextRow(panel, "HubUrl", "http://dcloud-api.adtogroup.com:20980/signalr-chat");
        txtEncToken = AddTextRow(panel, "enc_auth_token", "");
        txtTenantId = AddTextRow(panel, "TenantId", "");
        txtUserId = AddTextRow(panel, "Target UserId", "");
        txtUserName = AddTextRow(panel, "Sender UserName", "demo-user");
        txtTenancyName = AddTextRow(panel, "Sender TenancyName", "Default");

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnConnect = NewButton("连接", btnConnect_Click);
        btnDisconnect = NewButton("断开", btnDisconnect_Click);
        btnRegister = NewButton("Register", btnRegister_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnConnect, btnDisconnect, btnRegister });
        panel.Controls.Add(buttonPanel);

        box.Controls.Add(panel);
        return box;
    }

    private GroupBox BuildSingleGroup()
    {
        var box = new GroupBox { Text = "单聊", Dock = DockStyle.Fill, Padding = new Padding(10), AutoSize = true };
        var panel = NewFieldsPanel();

        txtMessage = AddMultiRow(panel, "Message", "你好，这是一条测试消息。", 120);

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnSendMessage = NewButton("SendMessage", btnSendMessage_Click);
        btnSendUser = NewButton("SendMessageToUser", btnSendUser_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnSendMessage, btnSendUser });
        panel.Controls.Add(buttonPanel);

        box.Controls.Add(panel);
        return box;
    }

    private GroupBox BuildGroupGroup()
    {
        var box = new GroupBox { Text = "群组", Dock = DockStyle.Fill, Padding = new Padding(10), AutoSize = true };
        var panel = NewFieldsPanel();

        txtGroupName = AddTextRow(panel, "GroupName", "room-1");

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnJoinGroup = NewButton("JoinGroup", btnJoinGroup_Click);
        btnLeaveGroup = NewButton("LeaveGroup", btnLeaveGroup_Click);
        btnSendGroup = NewButton("SendMessageToGroup", btnSendGroup_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnJoinGroup, btnLeaveGroup, btnSendGroup });
        panel.Controls.Add(buttonPanel);

        box.Controls.Add(panel);
        return box;
    }

    private GroupBox BuildBroadcastSystemGroup()
    {
        var box = new GroupBox { Text = "广播 / 系统消息", Dock = DockStyle.Fill, Padding = new Padding(10), AutoSize = true };
        var panel = NewFieldsPanel();

        txtSystemTitle = AddTextRow(panel, "System Title", "系统通知");

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnBroadcast = NewButton("BroadcastMessage", btnBroadcast_Click);
        btnSendSystem = NewButton("SendSystemMessage", btnSendSystem_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnBroadcast, btnSendSystem });
        panel.Controls.Add(buttonPanel);

        box.Controls.Add(panel);
        return box;
    }

    private static FlowLayoutPanel NewFieldsPanel()
    {
        return new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };
    }

    private static Button NewButton(string text, EventHandler handler)
    {
        var btn = new Button { Text = text, AutoSize = true, Margin = new Padding(3, 3, 10, 3) };
        btn.Click += handler;
        return btn;
    }

    private static TextBox AddTextRow(Control parent, string labelText, string defaultValue)
    {
        var wrapper = new Panel { Width = 280, Height = 52 };
        var label = new Label { Text = labelText, Left = 0, Top = 0, Width = 260 };
        var box = new TextBox { Left = 0, Top = 20, Width = 260, Text = defaultValue };
        wrapper.Controls.Add(label);
        wrapper.Controls.Add(box);
        parent.Controls.Add(wrapper);
        return box;
    }

    private static TextBox AddMultiRow(Control parent, string labelText, string defaultValue, int height)
    {
        var wrapper = new Panel { Width = 280, Height = height + 25 };
        var label = new Label { Text = labelText, Left = 0, Top = 0, Width = 260 };
        var box = new TextBox
        {
            Left = 0,
            Top = 20,
            Width = 260,
            Height = height,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Text = defaultValue
        };
        wrapper.Controls.Add(label);
        wrapper.Controls.Add(box);
        parent.Controls.Add(wrapper);
        return box;
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
}
