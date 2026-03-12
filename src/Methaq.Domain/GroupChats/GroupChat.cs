using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.GroupChats;
using Methaq.Domain.Sections;

namespace Methaq.Domain.GroupChats;

public class GroupChat : BaseEntity
{
    public string Name { get; private set; } = null!;

    public Guid SectionId { get; private set; }
    public Section Section { get; private set; } = null!;

    private readonly List<ApplicationUser> _members = [];
    public IReadOnlyCollection<ApplicationUser> Members => _members.AsReadOnly();

    private readonly List<GroupMessage> _messages = [];
    public IReadOnlyCollection<GroupMessage> Messages => _messages.AsReadOnly();

    private readonly List<UserChatLastRead> _lastReads = [];
    public IReadOnlyCollection<UserChatLastRead> LastReads => _lastReads.AsReadOnly();

    protected GroupChat() { }

    private GroupChat(string name, Guid sectionId)
    {
        Name = name;
        SectionId = sectionId;
    }

    public static ErrorOr<GroupChat> Create(string name, Guid sectionId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GroupChatErrors.NameRequired;

        if (sectionId == Guid.Empty)
            return GroupChatErrors.SectionIdRequired;

        return new GroupChat(name, sectionId);
    }

    public ErrorOr<Success> AddMember(ApplicationUser user)
    {
        if (user == null)
            return GroupChatErrors.UserNull;

        if (_members.Any(m => m.Id == user.Id))
            return GroupChatErrors.UserAlreadyMember;

        _members.Add(user);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveMember(string userId)
    {
        var member = _members.FirstOrDefault(m => m.Id == userId);
        if (member == null)
            return GroupChatErrors.MemberNotFound;

        _members.Remove(member);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> SendMessage(GroupMessage message)
    {
        if (message == null)
            return GroupChatErrors.MessageNull;

        if (!_members.Any(m => m.Id == message.SenderId))
            return GroupChatErrors.SenderNotMember;

        _messages.Add(message);
        MarkAsUpdated();
        return Result.Success;
    }
}
