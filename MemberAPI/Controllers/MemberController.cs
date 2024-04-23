using MemberAPI.Models;
using MemberAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class MemberController(IMemberService memberService) : ControllerBase
    {
        private readonly IMemberService _memberService = memberService;

        [HttpPost]
        public async Task<object> Post([FromBody] Member member)
        {
            try
            {
                 return await _memberService.InsertMember(member);               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The add cannot be executed");
            }  
        }

        [HttpPut]
        public async Task<object> Put([FromBody] Member member)
        {
            try
            {
                return await _memberService.UpdateMember(member);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The update cannot be executed");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {         
            try
            {
                return await _memberService.DeleteMember(id);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The delete cannot be executed");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var member = await _memberService.GetMemberById(id);   
                return Ok(member);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The get member by id cannot be executed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Member> members = await _memberService.GetMembers();
                return Ok(members);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The get members cannot be executed");
            }
        }
    }
}
