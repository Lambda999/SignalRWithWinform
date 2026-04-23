namespace WinFormsSignalRDemo;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private TableLayoutPanel rootLayout = null!;
    private TableLayoutPanel topLayout = null!;
    private GroupBox grpLogin = null!;
    private GroupBox grpConnection = null!;
    private GroupBox grpSingle = null!;
    private GroupBox grpGroup = null!;
    private GroupBox grpBroadcast = null!;

    private TextBox txtHubUrl = null!;
    private TextBox txtEncToken = null!;
    private TextBox txtTenantId = null!;
    private TextBox txtUserId = null!;
    private TextBox txtUserName = null!;
    private TextBox txtTenancyName = null!;
    private TextBox txtLoginUserName = null!;
    private TextBox txtLoginPassword = null!;
    private TextBox txtGroupName = null!;
    private TextBox txtSystemTitle = null!;
    private TextBox txtMessage = null!;
    private TextBox txtLog = null!;

    private Button btnLogin = null!;
    private Button btnConnect = null!;
    private Button btnDisconnect = null!;
    private Button btnRegister = null!;
    private Button btnSendMessage = null!;
    private Button btnSendUser = null!;
    private Button btnGetOnlineUsers = null!;
    private Button btnSendUsers = null!;
    private Button btnJoinGroup = null!;
    private Button btnLeaveGroup = null!;
    private Button btnSendGroup = null!;
    private Button btnBroadcast = null!;
    private Button btnSendSystem = null!;
    private ListBox lstOnlineUsers = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        rootLayout = new TableLayoutPanel();
        topLayout = new TableLayoutPanel();
        grpLogin = new GroupBox();
        grpConnection = new GroupBox();
        grpSingle = new GroupBox();
        grpGroup = new GroupBox();
        grpBroadcast = new GroupBox();
        txtLog = new TextBox();

        SuspendLayout();

        rootLayout.Dock = DockStyle.Fill;
        rootLayout.ColumnCount = 1;
        rootLayout.RowCount = 2;
        rootLayout.Padding = new Padding(10);
        rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        topLayout.Dock = DockStyle.Top;
        topLayout.AutoSize = true;
        topLayout.ColumnCount = 5;
        for (var i = 0; i < 5; i++)
        {
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        }

        grpLogin.Text = "登录";
        grpLogin.Dock = DockStyle.Fill;
        grpLogin.Padding = new Padding(10);
        grpLogin.Controls.Add(BuildLoginPanel());

        grpConnection.Text = "连接";
        grpConnection.Dock = DockStyle.Fill;
        grpConnection.Padding = new Padding(10);
        grpConnection.Controls.Add(BuildConnectionPanel());

        grpSingle.Text = "单聊";
        grpSingle.Dock = DockStyle.Fill;
        grpSingle.Padding = new Padding(10);
        grpSingle.Controls.Add(BuildSinglePanel());

        grpGroup.Text = "群组";
        grpGroup.Dock = DockStyle.Fill;
        grpGroup.Padding = new Padding(10);
        grpGroup.Controls.Add(BuildGroupPanel());

        grpBroadcast.Text = "广播 / 系统消息";
        grpBroadcast.Dock = DockStyle.Fill;
        grpBroadcast.Padding = new Padding(10);
        grpBroadcast.Controls.Add(BuildBroadcastPanel());

        topLayout.Controls.Add(grpLogin, 0, 0);
        topLayout.Controls.Add(grpConnection, 1, 0);
        topLayout.Controls.Add(grpSingle, 2, 0);
        topLayout.Controls.Add(grpGroup, 3, 0);
        topLayout.Controls.Add(grpBroadcast, 4, 0);

        txtLog.Dock = DockStyle.Fill;
        txtLog.Multiline = true;
        txtLog.ScrollBars = ScrollBars.Both;
        txtLog.ReadOnly = true;
        txtLog.Font = new Font("Consolas", 10F);
        txtLog.WordWrap = false;

        rootLayout.Controls.Add(topLayout, 0, 0);
        rootLayout.Controls.Add(txtLog, 0, 1);

        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1550, 900);
        Controls.Add(rootLayout);
        StartPosition = FormStartPosition.CenterScreen;
        Text = ".NET 8 WinForms SignalR Demo";

        ResumeLayout(false);
    }

    private Control BuildLoginPanel()
    {
        var panel = NewFieldsPanel();
        txtLoginUserName = AddTextRow(panel, "UserName", "");
        txtLoginPassword = AddTextRow(panel, "Password", "");
        txtLoginPassword.UseSystemPasswordChar = true;

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnLogin = NewButton("登录", btnLogin_Click);
        buttonPanel.Controls.Add(btnLogin);
        panel.Controls.Add(buttonPanel);
        return panel;
    }

    private Control BuildConnectionPanel()
    {
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
        return panel;
    }

    private Control BuildSinglePanel()
    {
        var panel = NewFieldsPanel();
        txtMessage = AddMultiRow(panel, "Message", "你好，这是一条测试消息。", 120);

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnSendMessage = NewButton("SendMessage", btnSendMessage_Click);
        btnSendUser = NewButton("SendMessageToUser", btnSendUser_Click);
        btnGetOnlineUsers = NewButton("GetOnlineUsers", btnGetOnlineUsers_Click);
        btnSendUsers = NewButton("发送给勾选用户", btnSendUsers_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnSendMessage, btnSendUser, btnGetOnlineUsers, btnSendUsers });
        panel.Controls.Add(buttonPanel);

        lstOnlineUsers = new ListBox
        {
            Width = 260,
            Height = 160,
            SelectionMode = SelectionMode.MultiExtended
        };
        panel.Controls.Add(lstOnlineUsers);

        return panel;
    }

    private Control BuildGroupPanel()
    {
        var panel = NewFieldsPanel();
        txtGroupName = AddTextRow(panel, "GroupName", "room-1");

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnJoinGroup = NewButton("JoinGroup", btnJoinGroup_Click);
        btnLeaveGroup = NewButton("LeaveGroup", btnLeaveGroup_Click);
        btnSendGroup = NewButton("SendMessageToGroup", btnSendGroup_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnJoinGroup, btnLeaveGroup, btnSendGroup });
        panel.Controls.Add(buttonPanel);
        return panel;
    }

    private Control BuildBroadcastPanel()
    {
        var panel = NewFieldsPanel();
        txtSystemTitle = AddTextRow(panel, "System Title", "系统通知");

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        btnBroadcast = NewButton("BroadcastMessage", btnBroadcast_Click);
        btnSendSystem = NewButton("SendSystemMessage", btnSendSystem_Click);
        buttonPanel.Controls.AddRange(new Control[] { btnBroadcast, btnSendSystem });
        panel.Controls.Add(buttonPanel);
        return panel;
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
}
