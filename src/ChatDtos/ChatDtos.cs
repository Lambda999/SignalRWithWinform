using System.Text.Json.Serialization;

namespace WinFormsSignalRDemo.ChatDtos;

public sealed class SendChatMessageInput
{
    public Guid? TenantId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = "";
    public string TenancyName { get; set; } = "";
    public string UserName { get; set; } = "";
    public Guid? ProfilePictureId { get; set; }
}

public sealed class SendGroupMessageInput
{
    public string GroupName { get; set; } = "";
    public string Message { get; set; } = "";
    public string SenderUserName { get; set; } = "";
    public Guid? SenderUserId { get; set; }
    public Guid? SenderTenantId { get; set; }
}

public sealed class BroadcastMessageInput
{
    public string Message { get; set; } = "";
    public string SenderUserName { get; set; } = "";
    public Guid? SenderUserId { get; set; }
    public Guid? SenderTenantId { get; set; }
}

public sealed class SendSystemMessageInput
{
    public Guid? TenantId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "info";
}

public sealed class GroupMessageDto
{
    public string GroupName { get; set; } = "";
    public string Message { get; set; } = "";
    public string SenderUserName { get; set; } = "";
    public Guid? SenderUserId { get; set; }
    public Guid? SenderTenantId { get; set; }
    public DateTime CreationTime { get; set; }
}

public sealed class BroadcastMessageDto
{
    public string Message { get; set; } = "";
    public string SenderUserName { get; set; } = "";
    public Guid? SenderUserId { get; set; }
    public Guid? SenderTenantId { get; set; }
    public DateTime CreationTime { get; set; }
}

public sealed class SystemMessageDto
{
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "info";
    public DateTime CreationTime { get; set; }
}

public sealed class LoginRequest
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
}

public sealed class LoginResult
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = "";

    [JsonPropertyName("encryptedAccessToken")]
    public string EncryptedAccessToken { get; set; } = "";

    [JsonPropertyName("expireInSeconds")]
    public int ExpireInSeconds { get; set; }

    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
}

public sealed class ApiResponse<T>
{
    [JsonPropertyName("result")]
    public T? Result { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("targetUrl")]
    public string? TargetUrl { get; set; }

    [JsonPropertyName("error")]
    public object? Error { get; set; }

    [JsonPropertyName("unAuthorizedRequest")]
    public bool UnAuthorizedRequest { get; set; }
}

public sealed class OnlineUserDto
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; } = "";
    public Guid? TenantId { get; set; }
    public string TenancyName { get; set; } = "";
    public string ConnectionId { get; set; } = "";
}
