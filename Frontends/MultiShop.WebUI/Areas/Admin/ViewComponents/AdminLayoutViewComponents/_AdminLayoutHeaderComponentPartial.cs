using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentService commentService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentService = commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();

            ViewBag.messageCount =
                await _messageService.GetTotalMessageCountByReceiverId(user.Id);

            ViewBag.lastMessages =
                await _messageService.GetLastInboxMessages(user.Id, 3);

            ViewBag.totalCommentCount =
                await _commentService.GetTotalCommentCount();

            ViewBag.lastComments =
                await _commentService.GetLastComments(3);

            return View();
        }
    }
}
