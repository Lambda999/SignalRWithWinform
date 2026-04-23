# .NET 8 WinForms SignalR Demo

这是一个可直接打开的 .NET 8 WinForms Demo，包含以下功能：

- 连接 SignalR Hub（HubUrl 自动由 `Api Base Url + signalr-chat` 生成）
- 独立“登录”分组：调用 `api/TokenAuth/Authenticate` 登录，自动回填 `EncryptedAccessToken`
- `Register`
- `GetOnlineUsers`
- 通过 API（Bearer `accessToken`）查询：
  - `/api/services/app/Chat/GetOnlineUsers`
  - `/api/services/app/Chat/GetMyOnlineFriends`
  - `/api/services/app/Chat/IsUserOnline`
- `SendMessage`
- `SendMessageToUser`
- 给“在线用户列表”中的多选用户批量发送消息
- 自动保存/恢复界面配置（`appsettings.json`）
- `JoinGroup`
- `LeaveGroup`
- `SendMessageToGroup`
- `BroadcastMessage`
- `SendSystemMessage`
- 接收事件：
  - `ReceiveMessage`
  - `ReceiveGroupMessage`
  - `ReceiveBroadcastMessage`
  - `ReceiveSystemMessage`

## 使用方法

1. 用 Visual Studio 2022 / Rider 打开 `WinFormsSignalRDemo.csproj`
2. 还原 NuGet 包
3. 修改界面中的：
   - `HubUrl`
   - `enc_auth_token`
   - `TenantId`
   - `Target UserId`
4. 运行后联调

## 注意

### Hub 地址
默认示例里填的是：

`http://dcloud-api.adtogroup.com:20980/signalr-chat`

你需要改成你服务端 `ChatHub` 的真实地址。

### 单聊接收事件名
当前客户端监听的是 `ReceiveMessage`。
如果你服务端 `_chatCommunicator` 实际推送的事件名不是这个，请同步改 `DCloudChatHubClient.cs`。

### 群聊 / 广播 / 系统消息
这三个 Demo 当前是“实时推送版”。
如果你服务端还没加对应 Hub 方法，需要把我之前给你的 `ChatHub` 方法补到服务端。
