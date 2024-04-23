using MemberClient.Models;

namespace MemberClient.APIServices
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetMembersAsync();
        Task<MemberViewModel> GetMemberAsync(int id);
        Task<MemberViewModel> InsertMemberAsync(MemberViewModel member);
        Task<MemberViewModel> UpdateMemberAsync(MemberViewModel member);
        Task<bool> DeleteMemberAsync(int memberId);
    }
}
