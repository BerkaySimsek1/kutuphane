using KutuphaneTakip.Repositories.Interfaces;
using KutuphaneTakip.Models;
using KutuphaneTakip.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _unitOfWork.Members.GetAll();
        }

        public async Task<Member> GetMemberById(int id)
        {
            return await _unitOfWork.Members.GetById(id);
        }

        public async Task<Member> AddMember(Member member)
        {
            await _unitOfWork.Members.Add(member);
            await _unitOfWork.Complete();
            return member;
        }

        public async Task<bool> UpdateMember(int id, Member member)
        {
            var existingMember = await _unitOfWork.Members.GetById(id);
            if (existingMember == null)
            {
                return false;
            }

            existingMember.Name = member.Name;
            existingMember.Surname = member.Surname;
            existingMember.MembershipDate = member.MembershipDate;

            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteMember(int id)
        {
            var member = await _unitOfWork.Members.GetById(id);
            if (member == null)
            {
                return false;
            }

            await _unitOfWork.Members.Delete(member);
            await _unitOfWork.Complete();
            return true;
        }
    }
}