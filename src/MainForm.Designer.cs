namespace WinFormsSignalRDemo;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private GroupBox grpLogin = null!;
    private GroupBox grpConnection = null!;
    private GroupBox grpSingle = null!;
    private GroupBox grpGroup = null!;
    private GroupBox grpBroadcast = null!;

    private Label lblLoginUserName = null!;
    private Label lblLoginPassword = null!;
    private Label lblHubUrl = null!;
    private Label lblEncToken = null!;
    private Label lblTenantId = null!;
    private Label lblUserId = null!;
    private Label lblUserName = null!;
    private Label lblTenancyName = null!;
    private Label lblMessage = null!;
    private Label lblGroupName = null!;
    private Label lblSystemTitle = null!;

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

        grpLogin = new GroupBox();
        grpConnection = new GroupBox();
        grpSingle = new GroupBox();
        grpGroup = new GroupBox();
        grpBroadcast = new GroupBox();
        txtLog = new TextBox();

        lblLoginUserName = new Label();
        lblLoginPassword = new Label();
        lblHubUrl = new Label();
        lblEncToken = new Label();
        lblTenantId = new Label();
        lblUserId = new Label();
        lblUserName = new Label();
        lblTenancyName = new Label();
        lblMessage = new Label();
        lblGroupName = new Label();
        lblSystemTitle = new Label();

        txtLoginUserName = new TextBox();
        txtLoginPassword = new TextBox();
        txtHubUrl = new TextBox();
        txtEncToken = new TextBox();
        txtTenantId = new TextBox();
        txtUserId = new TextBox();
        txtUserName = new TextBox();
        txtTenancyName = new TextBox();
        txtMessage = new TextBox();
        txtGroupName = new TextBox();
        txtSystemTitle = new TextBox();

        btnLogin = new Button();
        btnConnect = new Button();
        btnDisconnect = new Button();
        btnRegister = new Button();
        btnSendMessage = new Button();
        btnSendUser = new Button();
        btnGetOnlineUsers = new Button();
        btnSendUsers = new Button();
        btnJoinGroup = new Button();
        btnLeaveGroup = new Button();
        btnSendGroup = new Button();
        btnBroadcast = new Button();
        btnSendSystem = new Button();
        lstOnlineUsers = new ListBox();

        SuspendLayout();

        ConfigureLoginGroup();
        ConfigureConnectionGroup();
        ConfigureSingleGroup();
        ConfigureGroupGroup();
        ConfigureBroadcastGroup();

        txtLog.Location = new Point(10, 460);
        txtLog.Size = new Size(1660, 430);
        txtLog.Multiline = true;
        txtLog.ScrollBars = ScrollBars.Both;
        txtLog.ReadOnly = true;
        txtLog.Font = new Font("Consolas", 10F);
        txtLog.WordWrap = false;

        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1680, 900);
        Controls.Add(grpLogin);
        Controls.Add(grpConnection);
        Controls.Add(grpSingle);
        Controls.Add(grpGroup);
        Controls.Add(grpBroadcast);
        Controls.Add(txtLog);
        StartPosition = FormStartPosition.CenterScreen;
        Text = ".NET 8 WinForms SignalR Demo";

        ResumeLayout(false);
    }

    private void ConfigureLoginGroup()
    {
        grpLogin.Text = "登录";
        grpLogin.Location = new Point(10, 10);
        grpLogin.Size = new Size(320, 440);

        lblLoginUserName.Text = "UserName";
        lblLoginUserName.Location = new Point(12, 30);
        lblLoginUserName.AutoSize = true;

        txtLoginUserName.Location = new Point(12, 50);
        txtLoginUserName.Size = new Size(300, 23);

        lblLoginPassword.Text = "Password";
        lblLoginPassword.Location = new Point(12, 88);
        lblLoginPassword.AutoSize = true;

        txtLoginPassword.Location = new Point(12, 108);
        txtLoginPassword.Size = new Size(300, 23);
        txtLoginPassword.UseSystemPasswordChar = true;

        btnLogin.Text = "登录";
        btnLogin.Location = new Point(12, 150);
        btnLogin.Size = new Size(120, 30);
        btnLogin.Click += btnLogin_Click;

        grpLogin.Controls.AddRange(new Control[]
        {
            lblLoginUserName, txtLoginUserName,
            lblLoginPassword, txtLoginPassword,
            btnLogin
        });
    }

    private void ConfigureConnectionGroup()
    {
        grpConnection.Text = "连接";
        grpConnection.Location = new Point(340, 10);
        grpConnection.Size = new Size(320, 440);

        lblHubUrl.Text = "HubUrl";
        lblHubUrl.Location = new Point(12, 30);
        lblHubUrl.AutoSize = true;
        txtHubUrl.Location = new Point(12, 50);
        txtHubUrl.Size = new Size(300, 23);
        txtHubUrl.Text = "http://dcloud-api.adtogroup.com:20980/signalr-chat";

        lblEncToken.Text = "enc_auth_token";
        lblEncToken.Location = new Point(12, 88);
        lblEncToken.AutoSize = true;
        txtEncToken.Location = new Point(12, 108);
        txtEncToken.Size = new Size(300, 23);

        lblTenantId.Text = "TenantId";
        lblTenantId.Location = new Point(12, 146);
        lblTenantId.AutoSize = true;
        txtTenantId.Location = new Point(12, 166);
        txtTenantId.Size = new Size(300, 23);

        lblUserId.Text = "Target UserId";
        lblUserId.Location = new Point(12, 204);
        lblUserId.AutoSize = true;
        txtUserId.Location = new Point(12, 224);
        txtUserId.Size = new Size(300, 23);

        lblUserName.Text = "Sender UserName";
        lblUserName.Location = new Point(12, 262);
        lblUserName.AutoSize = true;
        txtUserName.Location = new Point(12, 282);
        txtUserName.Size = new Size(300, 23);
        txtUserName.Text = "demo-user";

        lblTenancyName.Text = "Sender TenancyName";
        lblTenancyName.Location = new Point(12, 320);
        lblTenancyName.AutoSize = true;
        txtTenancyName.Location = new Point(12, 340);
        txtTenancyName.Size = new Size(300, 23);
        txtTenancyName.Text = "Default";

        btnConnect.Text = "连接";
        btnConnect.Location = new Point(12, 380);
        btnConnect.Size = new Size(80, 30);
        btnConnect.Click += btnConnect_Click;

        btnDisconnect.Text = "断开";
        btnDisconnect.Location = new Point(100, 380);
        btnDisconnect.Size = new Size(80, 30);
        btnDisconnect.Click += btnDisconnect_Click;

        btnRegister.Text = "Register";
        btnRegister.Location = new Point(188, 380);
        btnRegister.Size = new Size(90, 30);
        btnRegister.Click += btnRegister_Click;

        grpConnection.Controls.AddRange(new Control[]
        {
            lblHubUrl, txtHubUrl,
            lblEncToken, txtEncToken,
            lblTenantId, txtTenantId,
            lblUserId, txtUserId,
            lblUserName, txtUserName,
            lblTenancyName, txtTenancyName,
            btnConnect, btnDisconnect, btnRegister
        });
    }

    private void ConfigureSingleGroup()
    {
        grpSingle.Text = "单聊";
        grpSingle.Location = new Point(670, 10);
        grpSingle.Size = new Size(320, 440);

        lblMessage.Text = "Message";
        lblMessage.Location = new Point(12, 30);
        lblMessage.AutoSize = true;

        txtMessage.Location = new Point(12, 50);
        txtMessage.Size = new Size(300, 120);
        txtMessage.Multiline = true;
        txtMessage.ScrollBars = ScrollBars.Vertical;
        txtMessage.Text = "你好，这是一条测试消息。";

        btnSendMessage.Text = "SendMessage";
        btnSendMessage.Location = new Point(12, 190);
        btnSendMessage.Size = new Size(130, 30);
        btnSendMessage.Click += btnSendMessage_Click;

        btnSendUser.Text = "SendMessageToUser";
        btnSendUser.Location = new Point(150, 190);
        btnSendUser.Size = new Size(160, 30);
        btnSendUser.Click += btnSendUser_Click;

        btnGetOnlineUsers.Text = "GetOnlineUsers";
        btnGetOnlineUsers.Location = new Point(12, 230);
        btnGetOnlineUsers.Size = new Size(130, 30);
        btnGetOnlineUsers.Click += btnGetOnlineUsers_Click;

        btnSendUsers.Text = "发送给勾选用户";
        btnSendUsers.Location = new Point(150, 230);
        btnSendUsers.Size = new Size(160, 30);
        btnSendUsers.Click += btnSendUsers_Click;

        lstOnlineUsers.Location = new Point(12, 270);
        lstOnlineUsers.Size = new Size(300, 140);
        lstOnlineUsers.SelectionMode = SelectionMode.MultiExtended;

        grpSingle.Controls.AddRange(new Control[]
        {
            lblMessage, txtMessage,
            btnSendMessage, btnSendUser,
            btnGetOnlineUsers, btnSendUsers,
            lstOnlineUsers
        });
    }

    private void ConfigureGroupGroup()
    {
        grpGroup.Text = "群组";
        grpGroup.Location = new Point(1000, 10);
        grpGroup.Size = new Size(320, 440);

        lblGroupName.Text = "GroupName";
        lblGroupName.Location = new Point(12, 30);
        lblGroupName.AutoSize = true;

        txtGroupName.Location = new Point(12, 50);
        txtGroupName.Size = new Size(300, 23);
        txtGroupName.Text = "room-1";

        btnJoinGroup.Text = "JoinGroup";
        btnJoinGroup.Location = new Point(12, 90);
        btnJoinGroup.Size = new Size(90, 30);
        btnJoinGroup.Click += btnJoinGroup_Click;

        btnLeaveGroup.Text = "LeaveGroup";
        btnLeaveGroup.Location = new Point(110, 90);
        btnLeaveGroup.Size = new Size(100, 30);
        btnLeaveGroup.Click += btnLeaveGroup_Click;

        btnSendGroup.Text = "SendMessageToGroup";
        btnSendGroup.Location = new Point(12, 130);
        btnSendGroup.Size = new Size(198, 30);
        btnSendGroup.Click += btnSendGroup_Click;

        grpGroup.Controls.AddRange(new Control[]
        {
            lblGroupName, txtGroupName,
            btnJoinGroup, btnLeaveGroup, btnSendGroup
        });
    }

    private void ConfigureBroadcastGroup()
    {
        grpBroadcast.Text = "广播 / 系统消息";
        grpBroadcast.Location = new Point(1330, 10);
        grpBroadcast.Size = new Size(340, 440);

        lblSystemTitle.Text = "System Title";
        lblSystemTitle.Location = new Point(12, 30);
        lblSystemTitle.AutoSize = true;

        txtSystemTitle.Location = new Point(12, 50);
        txtSystemTitle.Size = new Size(300, 23);
        txtSystemTitle.Text = "系统通知";

        btnBroadcast.Text = "BroadcastMessage";
        btnBroadcast.Location = new Point(12, 90);
        btnBroadcast.Size = new Size(140, 30);
        btnBroadcast.Click += btnBroadcast_Click;

        btnSendSystem.Text = "SendSystemMessage";
        btnSendSystem.Location = new Point(160, 90);
        btnSendSystem.Size = new Size(152, 30);
        btnSendSystem.Click += btnSendSystem_Click;

        grpBroadcast.Controls.AddRange(new Control[]
        {
            lblSystemTitle, txtSystemTitle,
            btnBroadcast, btnSendSystem
        });
    }
}
