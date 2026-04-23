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
