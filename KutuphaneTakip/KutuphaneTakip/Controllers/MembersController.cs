using Microsoft.AspNetCore.Mvc;
using KutuphaneTakip.Services.Interfaces;
using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member member)
        {
            var newMember = await _memberService.AddMember(member);
            return CreatedAtAction(nameof(GetMember), new { id = newMember.Id }, newMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            var result = await _memberService.UpdateMember(id, member);
            if (!result)
            {
                return NotFound("Member not found.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMember(id);
            if (!result)
            {
                return NotFound("Member not found.");
            }
            return NoContent();
        }
    }
}