using MemberAPI.Models;

namespace MemberAPI.Service
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMemberById(int id);
        Task<Member> InsertMember(Member member);
        Task<Member> UpdateMember(Member member);
        Task<bool> DeleteMember(int memberId);
    }
}
