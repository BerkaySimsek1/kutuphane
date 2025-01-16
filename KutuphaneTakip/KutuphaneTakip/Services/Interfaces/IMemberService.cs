using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member> GetMemberById(int id);
        Task<Member> AddMember(Member member);
        Task<bool> UpdateMember(int id, Member member);
        Task<bool> DeleteMember(int id);
    }
}