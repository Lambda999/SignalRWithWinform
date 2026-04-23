namespace WinFormsSignalRDemo;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private GroupBox grpLogin = null!;
    private GroupBox grpConnection = null!;
    private GroupBox grpSingle = null!;
    private GroupBox grpGroup = null!;
    private GroupBox grpBroadcast = null!;
    private GroupBox grpFriendship = null!;

    private Label lblLoginUserName = null!;
    private Label lblLoginPassword = null!;
    private Label lblApiBaseUrl = null!;
    private Label lblHubUrl = null!;
    private Label lblEncToken = null!;
    private Label lblTenantId = null!;
    private Label lblUserId = null!;
    private Label lblUserName = null!;
    private Label lblTenancyName = null!;
    private Label lblMessage = null!;
    private Label lblGroupName = null!;
    private Label lblSystemTitle = null!;
    private Label lblFriendTenancyName = null!;
    private Label lblFriendUserName = null!;

    private TextBox txtHubUrl = null!;
    private TextBox txtEncToken = null!;
    private TextBox txtTenantId = null!;
    private TextBox txtUserId = null!;
    private TextBox txtUserName = null!;
    private TextBox txtTenancyName = null!;
    private TextBox txtLoginUserName = null!;
    private TextBox txtLoginPassword = null!;
    private TextBox txtApiBaseUrl = null!;
    private TextBox txtGroupName = null!;
    private TextBox txtSystemTitle = null!;
    private TextBox txtFriendTenancyName = null!;
    private TextBox txtFriendUserName = null!;
    private TextBox txtMessage = null!;
    private TextBox txtLog = null!;

    private Button btnLogin = null!;
    private Button btnConnect = null!;
    private Button btnDisconnect = null!;
    private Button btnRegister = null!;
    private Button btnSendMessage = null!;
    private Button btnSendUser = null!;
    private Button btnGetOnlineUsers = null!;
    private Button btnGetMyOnlineFriends = null!;
    private Button btnIsUserOnline = null!;
    private Button btnSendUsers = null!;
    private Button btnJoinGroup = null!;
    private Button btnLeaveGroup = null!;
    private Button btnSendGroup = null!;
    private Button btnBroadcast = null!;
    private Button btnSendSystem = null!;
    private Button btnCreateFriendshipRequest = null!;
    private Button btnCreateFriendshipWithDifferentTenant = null!;
    private Button btnCreateFriendshipForCurrentTenant = null!;
    private Button btnBlockUser = null!;
    private Button btnUnblockUser = null!;
    private Button btnAcceptFriendshipRequest = null!;
    private Button btnRemoveFriend = null!;
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
        grpFriendship = new GroupBox();

        lblLoginUserName = new Label();
        lblLoginPassword = new Label();
        lblApiBaseUrl = new Label();
        lblHubUrl = new Label();
        lblEncToken = new Label();
        lblTenantId = new Label();
        lblUserId = new Label();
        lblUserName = new Label();
        lblTenancyName = new Label();
        lblMessage = new Label();
        lblGroupName = new Label();
        lblSystemTitle = new Label();
        lblFriendTenancyName = new Label();
        lblFriendUserName = new Label();

        txtLoginUserName = new TextBox();
        txtLoginPassword = new TextBox();
        txtApiBaseUrl = new TextBox();
        txtHubUrl = new TextBox();
        txtEncToken = new TextBox();
        txtTenantId = new TextBox();
        txtUserId = new TextBox();
        txtUserName = new TextBox();
        txtTenancyName = new TextBox();
        txtMessage = new TextBox();
        txtGroupName = new TextBox();
        txtSystemTitle = new TextBox();
        txtFriendTenancyName = new TextBox();
        txtFriendUserName = new TextBox();
        txtLog = new TextBox();

        btnLogin = new Button();
        btnConnect = new Button();
        btnDisconnect = new Button();
        btnRegister = new Button();
        btnSendMessage = new Button();
        btnSendUser = new Button();
        btnGetOnlineUsers = new Button();
        btnGetMyOnlineFriends = new Button();
        btnIsUserOnline = new Button();
        btnSendUsers = new Button();
        btnJoinGroup = new Button();
        btnLeaveGroup = new Button();
        btnSendGroup = new Button();
        btnBroadcast = new Button();
        btnSendSystem = new Button();
        btnCreateFriendshipRequest = new Button();
        btnCreateFriendshipWithDifferentTenant = new Button();
        btnCreateFriendshipForCurrentTenant = new Button();
        btnBlockUser = new Button();
        btnUnblockUser = new Button();
        btnAcceptFriendshipRequest = new Button();
        btnRemoveFriend = new Button();
        lstOnlineUsers = new ListBox();

        SuspendLayout();
        grpLogin.SuspendLayout();
        grpConnection.SuspendLayout();
        grpSingle.SuspendLayout();
        grpGroup.SuspendLayout();
        grpBroadcast.SuspendLayout();
        grpFriendship.SuspendLayout();

        // grpLogin
        grpLogin.Text = "登录";
        grpLogin.Location = new Point(10, 10);
        grpLogin.Size = new Size(320, 440);

        lblLoginUserName.AutoSize = true;
        lblLoginUserName.Location = new Point(12, 86);
        lblLoginUserName.Text = "UserName";

        txtLoginUserName.Location = new Point(12, 106);
        txtLoginUserName.Size = new Size(300, 23);

        lblApiBaseUrl.AutoSize = true;
        lblApiBaseUrl.Location = new Point(12, 30);
        lblApiBaseUrl.Text = "Api Base Url";

        txtApiBaseUrl.Location = new Point(12, 50);
        txtApiBaseUrl.Size = new Size(300, 23);
        txtApiBaseUrl.Text = "http://localhost:44380/";
        txtApiBaseUrl.TextChanged += txtApiBaseUrl_TextChanged;

        lblLoginPassword.AutoSize = true;
        lblLoginPassword.Location = new Point(12, 144);
        lblLoginPassword.Text = "Password";

        txtLoginPassword.Location = new Point(12, 164);
        txtLoginPassword.Size = new Size(300, 23);
        txtLoginPassword.UseSystemPasswordChar = true;

        btnLogin.Location = new Point(12, 206);
        btnLogin.Size = new Size(120, 30);
        btnLogin.Text = "登录";
        btnLogin.Click += btnLogin_ClickAsync;

        grpLogin.Controls.Add(lblApiBaseUrl);
        grpLogin.Controls.Add(txtApiBaseUrl);
        grpLogin.Controls.Add(lblLoginUserName);
        grpLogin.Controls.Add(txtLoginUserName);
        grpLogin.Controls.Add(lblLoginPassword);
        grpLogin.Controls.Add(txtLoginPassword);
        grpLogin.Controls.Add(btnLogin);

        // grpConnection
        grpConnection.Text = "连接";
        grpConnection.Location = new Point(340, 10);
        grpConnection.Size = new Size(320, 440);

        lblHubUrl.AutoSize = true;
        lblHubUrl.Location = new Point(12, 30);
        lblHubUrl.Text = "HubUrl";
        txtHubUrl.Location = new Point(12, 50);
        txtHubUrl.Size = new Size(300, 23);
        txtHubUrl.ReadOnly = true;

        lblEncToken.AutoSize = true;
        lblEncToken.Location = new Point(12, 88);
        lblEncToken.Text = "enc_auth_token";
        txtEncToken.Location = new Point(12, 108);
        txtEncToken.Size = new Size(300, 23);

        lblTenantId.AutoSize = true;
        lblTenantId.Location = new Point(12, 146);
        lblTenantId.Text = "TenantId";
        txtTenantId.Location = new Point(12, 166);
        txtTenantId.Size = new Size(300, 23);

        lblUserId.AutoSize = true;
        lblUserId.Location = new Point(12, 204);
        lblUserId.Text = "Target UserId";
        txtUserId.Location = new Point(12, 224);
        txtUserId.Size = new Size(300, 23);

        lblUserName.AutoSize = true;
        lblUserName.Location = new Point(12, 262);
        lblUserName.Text = "Sender UserName";
        txtUserName.Location = new Point(12, 282);
        txtUserName.Size = new Size(300, 23);
        txtUserName.Text = "demo-user";

        lblTenancyName.AutoSize = true;
        lblTenancyName.Location = new Point(12, 320);
        lblTenancyName.Text = "Sender TenancyName";
        txtTenancyName.Location = new Point(12, 340);
        txtTenancyName.Size = new Size(300, 23);
        txtTenancyName.Text = "Default";

        btnConnect.Location = new Point(12, 380);
        btnConnect.Size = new Size(80, 30);
        btnConnect.Text = "连接";
        btnConnect.Click += btnConnect_Click;

        btnDisconnect.Location = new Point(100, 380);
        btnDisconnect.Size = new Size(80, 30);
        btnDisconnect.Text = "断开";
        btnDisconnect.Click += btnDisconnect_Click;

        btnRegister.Location = new Point(188, 380);
        btnRegister.Size = new Size(90, 30);
        btnRegister.Text = "Register";
        btnRegister.Click += btnRegister_Click;

        grpConnection.Controls.Add(lblHubUrl);
        grpConnection.Controls.Add(txtHubUrl);
        grpConnection.Controls.Add(lblEncToken);
        grpConnection.Controls.Add(txtEncToken);
        grpConnection.Controls.Add(lblTenantId);
        grpConnection.Controls.Add(txtTenantId);
        grpConnection.Controls.Add(lblUserId);
        grpConnection.Controls.Add(txtUserId);
        grpConnection.Controls.Add(lblUserName);
        grpConnection.Controls.Add(txtUserName);
        grpConnection.Controls.Add(lblTenancyName);
        grpConnection.Controls.Add(txtTenancyName);
        grpConnection.Controls.Add(btnConnect);
        grpConnection.Controls.Add(btnDisconnect);
        grpConnection.Controls.Add(btnRegister);

        // grpSingle
        grpSingle.Text = "单聊";
        grpSingle.Location = new Point(670, 10);
        grpSingle.Size = new Size(320, 440);

        lblMessage.AutoSize = true;
        lblMessage.Location = new Point(12, 30);
        lblMessage.Text = "Message";

        txtMessage.Location = new Point(12, 50);
        txtMessage.Size = new Size(300, 120);
        txtMessage.Multiline = true;
        txtMessage.ScrollBars = ScrollBars.Vertical;
        txtMessage.Text = "你好，这是一条测试消息。";

        btnSendMessage.Location = new Point(12, 190);
        btnSendMessage.Size = new Size(130, 30);
        btnSendMessage.Text = "SendMessage";
        btnSendMessage.Click += btnSendMessage_Click;

        btnSendUser.Location = new Point(150, 190);
        btnSendUser.Size = new Size(160, 30);
        btnSendUser.Text = "SendMessageToUser";
        btnSendUser.Click += btnSendUser_Click;

        btnGetOnlineUsers.Location = new Point(12, 230);
        btnGetOnlineUsers.Size = new Size(130, 30);
        btnGetOnlineUsers.Text = "GetOnlineUsers";
        btnGetOnlineUsers.Click += btnGetOnlineUsers_Click;

        btnSendUsers.Location = new Point(150, 230);
        btnSendUsers.Size = new Size(160, 30);
        btnSendUsers.Text = "发送给勾选用户";
        btnSendUsers.Click += btnSendUsers_Click;

        btnGetMyOnlineFriends.Location = new Point(12, 270);
        btnGetMyOnlineFriends.Size = new Size(145, 30);
        btnGetMyOnlineFriends.Text = "GetMyOnlineFriends";
        btnGetMyOnlineFriends.Click += btnGetMyOnlineFriends_Click;

        btnIsUserOnline.Location = new Point(165, 270);
        btnIsUserOnline.Size = new Size(145, 30);
        btnIsUserOnline.Text = "IsUserOnline";
        btnIsUserOnline.Click += btnIsUserOnline_Click;

        lstOnlineUsers.Location = new Point(12, 310);
        lstOnlineUsers.Size = new Size(300, 115);
        lstOnlineUsers.SelectionMode = SelectionMode.MultiExtended;

        grpSingle.Controls.Add(lblMessage);
        grpSingle.Controls.Add(txtMessage);
        grpSingle.Controls.Add(btnSendMessage);
        grpSingle.Controls.Add(btnSendUser);
        grpSingle.Controls.Add(btnGetOnlineUsers);
        grpSingle.Controls.Add(btnSendUsers);
        grpSingle.Controls.Add(btnGetMyOnlineFriends);
        grpSingle.Controls.Add(btnIsUserOnline);
        grpSingle.Controls.Add(lstOnlineUsers);

        // grpGroup
        grpGroup.Text = "群组";
        grpGroup.Location = new Point(1000, 10);
        grpGroup.Size = new Size(320, 440);

        lblGroupName.AutoSize = true;
        lblGroupName.Location = new Point(12, 30);
        lblGroupName.Text = "GroupName";

        txtGroupName.Location = new Point(12, 50);
        txtGroupName.Size = new Size(300, 23);
        txtGroupName.Text = "room-1";

        btnJoinGroup.Location = new Point(12, 90);
        btnJoinGroup.Size = new Size(90, 30);
        btnJoinGroup.Text = "JoinGroup";
        btnJoinGroup.Click += btnJoinGroup_Click;

        btnLeaveGroup.Location = new Point(110, 90);
        btnLeaveGroup.Size = new Size(100, 30);
        btnLeaveGroup.Text = "LeaveGroup";
        btnLeaveGroup.Click += btnLeaveGroup_Click;

        btnSendGroup.Location = new Point(12, 130);
        btnSendGroup.Size = new Size(198, 30);
        btnSendGroup.Text = "SendMessageToGroup";
        btnSendGroup.Click += btnSendGroup_Click;

        grpGroup.Controls.Add(lblGroupName);
        grpGroup.Controls.Add(txtGroupName);
        grpGroup.Controls.Add(btnJoinGroup);
        grpGroup.Controls.Add(btnLeaveGroup);
        grpGroup.Controls.Add(btnSendGroup);

        // grpBroadcast
        grpBroadcast.Text = "广播 / 系统消息";
        grpBroadcast.Location = new Point(1330, 10);
        grpBroadcast.Size = new Size(340, 440);

        lblSystemTitle.AutoSize = true;
        lblSystemTitle.Location = new Point(12, 30);
        lblSystemTitle.Text = "System Title";

        txtSystemTitle.Location = new Point(12, 50);
        txtSystemTitle.Size = new Size(300, 23);
        txtSystemTitle.Text = "系统通知";

        btnBroadcast.Location = new Point(12, 90);
        btnBroadcast.Size = new Size(140, 30);
        btnBroadcast.Text = "BroadcastMessage";
        btnBroadcast.Click += btnBroadcast_Click;

        btnSendSystem.Location = new Point(160, 90);
        btnSendSystem.Size = new Size(152, 30);
        btnSendSystem.Text = "SendSystemMessage";
        btnSendSystem.Click += btnSendSystem_Click;

        grpBroadcast.Controls.Add(lblSystemTitle);
        grpBroadcast.Controls.Add(txtSystemTitle);
        grpBroadcast.Controls.Add(btnBroadcast);
        grpBroadcast.Controls.Add(btnSendSystem);

        // grpFriendship
        grpFriendship.Text = "好友管理服务 (Friendship)";
        grpFriendship.Location = new Point(1330, 460);
        grpFriendship.Size = new Size(340, 440);

        lblFriendTenancyName.AutoSize = true;
        lblFriendTenancyName.Location = new Point(12, 30);
        lblFriendTenancyName.Text = "Friend TenancyName";

        txtFriendTenancyName.Location = new Point(12, 50);
        txtFriendTenancyName.Size = new Size(300, 23);
        txtFriendTenancyName.Text = "Default";

        lblFriendUserName.AutoSize = true;
        lblFriendUserName.Location = new Point(12, 80);
        lblFriendUserName.Text = "Friend UserName";

        txtFriendUserName.Location = new Point(12, 100);
        txtFriendUserName.Size = new Size(300, 23);

        btnCreateFriendshipRequest.Location = new Point(12, 135);
        btnCreateFriendshipRequest.Size = new Size(300, 30);
        btnCreateFriendshipRequest.Text = "CreateFriendshipRequest";
        btnCreateFriendshipRequest.Click += btnCreateFriendshipRequest_Click;

        btnCreateFriendshipWithDifferentTenant.Location = new Point(12, 171);
        btnCreateFriendshipWithDifferentTenant.Size = new Size(300, 30);
        btnCreateFriendshipWithDifferentTenant.Text = "CreateFriendshipWithDifferentTenant";
        btnCreateFriendshipWithDifferentTenant.Click += btnCreateFriendshipWithDifferentTenant_Click;

        btnCreateFriendshipForCurrentTenant.Location = new Point(12, 207);
        btnCreateFriendshipForCurrentTenant.Size = new Size(300, 30);
        btnCreateFriendshipForCurrentTenant.Text = "CreateFriendshipForCurrentTenant";
        btnCreateFriendshipForCurrentTenant.Click += btnCreateFriendshipForCurrentTenant_Click;

        btnBlockUser.Location = new Point(12, 243);
        btnBlockUser.Size = new Size(145, 30);
        btnBlockUser.Text = "BlockUser";
        btnBlockUser.Click += btnBlockUser_Click;

        btnUnblockUser.Location = new Point(167, 243);
        btnUnblockUser.Size = new Size(145, 30);
        btnUnblockUser.Text = "UnblockUser";
        btnUnblockUser.Click += btnUnblockUser_Click;

        btnAcceptFriendshipRequest.Location = new Point(12, 279);
        btnAcceptFriendshipRequest.Size = new Size(300, 30);
        btnAcceptFriendshipRequest.Text = "AcceptFriendshipRequest";
        btnAcceptFriendshipRequest.Click += btnAcceptFriendshipRequest_Click;

        btnRemoveFriend.Location = new Point(12, 315);
        btnRemoveFriend.Size = new Size(300, 30);
        btnRemoveFriend.Text = "RemoveFriend";
        btnRemoveFriend.Click += btnRemoveFriend_Click;

        grpFriendship.Controls.Add(lblFriendTenancyName);
        grpFriendship.Controls.Add(txtFriendTenancyName);
        grpFriendship.Controls.Add(lblFriendUserName);
        grpFriendship.Controls.Add(txtFriendUserName);
        grpFriendship.Controls.Add(btnCreateFriendshipRequest);
        grpFriendship.Controls.Add(btnCreateFriendshipWithDifferentTenant);
        grpFriendship.Controls.Add(btnCreateFriendshipForCurrentTenant);
        grpFriendship.Controls.Add(btnBlockUser);
        grpFriendship.Controls.Add(btnUnblockUser);
        grpFriendship.Controls.Add(btnAcceptFriendshipRequest);
        grpFriendship.Controls.Add(btnRemoveFriend);

        // log
        txtLog.Location = new Point(10, 910);
        txtLog.Size = new Size(1660, 75);
        txtLog.Multiline = true;
        txtLog.ScrollBars = ScrollBars.Both;
        txtLog.ReadOnly = true;
        txtLog.Font = new Font("Consolas", 10F);
        txtLog.WordWrap = false;

        // Form
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1680, 995);
        Controls.Add(grpLogin);
        Controls.Add(grpConnection);
        Controls.Add(grpSingle);
        Controls.Add(grpGroup);
        Controls.Add(grpBroadcast);
        Controls.Add(grpFriendship);
        Controls.Add(txtLog);
        StartPosition = FormStartPosition.CenterScreen;
        Text = ".NET 8 WinForms SignalR Demo";

        grpLogin.ResumeLayout(false);
        grpLogin.PerformLayout();
        grpConnection.ResumeLayout(false);
        grpConnection.PerformLayout();
        grpSingle.ResumeLayout(false);
        grpSingle.PerformLayout();
        grpGroup.ResumeLayout(false);
        grpGroup.PerformLayout();
        grpBroadcast.ResumeLayout(false);
        grpBroadcast.PerformLayout();
        grpFriendship.ResumeLayout(false);
        grpFriendship.PerformLayout();
        ResumeLayout(false);
    }
}
