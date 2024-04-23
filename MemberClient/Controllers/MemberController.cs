using MemberClient.APIServices;
using MemberClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemberClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _memberService.GetMembersAsync());
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(MemberViewModel member)
        {          
            if (ModelState.IsValid)
            {
                await _memberService.InsertMemberAsync(member);
                return RedirectToAction("Index");
            }
            return View();            
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
                return NotFound();

            var member = await _memberService.GetMemberAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MemberViewModel member)
        {
            if (ModelState.IsValid)
            {
                await _memberService.UpdateMemberAsync(member);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var member = await _memberService.GetMemberAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (ModelState.IsValid)
            {
                await _memberService.DeleteMemberAsync(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}