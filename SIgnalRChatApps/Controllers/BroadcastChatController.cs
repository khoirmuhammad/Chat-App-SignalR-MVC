using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SIgnalRChatApps.Data;
using SIgnalRChatApps.Hubs;
using SIgnalRChatApps.Models;
using System;

namespace SIgnalRChatApps.Controllers
{
    public class BroadcastChatController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ChatHub _chatHub;
        public BroadcastChatController(ApplicationContext context, ChatHub chatHub)
        {
            _context = context;
            _chatHub = chatHub;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string userTarget, CancellationToken cancellationToken)
        {
            var currentUser = HttpContext.User.Identity?.Name;

            List<User> otherUsers = await _context.Users.Where(w => w.Username != currentUser).ToListAsync(cancellationToken);
            ViewBag.OtherUsers = otherUsers;

            if (!string.IsNullOrEmpty(userTarget))
            {  
                var chats = await _context.Chats.Select(s => new
                {
                    Id = s.Id,
                    Sender =  _context.Users.First(f => f.Id == s.Sender).Username,
                    Receiver = _context.Users.First(f => f.Id == s.Receiver).Username,
                    Message = s.Message,
                    SentDate = s.SentDate,
                    IsDeleted = s.IsDeleted,
                    IsMessageSender = s.IsMessageSender,
                    Position = _context.Users.First(f => f.Id == s.Sender).Username == currentUser ? "right" : "left",})
                    .Where(w => (w.Sender == currentUser && w.Receiver == userTarget) || 
                              (w.Sender == userTarget && w.Receiver == currentUser))
                    .ToListAsync(cancellationToken);


                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userTarget, cancellationToken);

                ViewBag.FullnameTarget = user?.Fullname ?? string.Empty;
                ViewBag.PhotoTarget = user?.Photo ?? string.Empty;
                ViewBag.Chat = chats;
                
            }
            else
            {
                ViewBag.FullnameTarget = string.Empty;
                ViewBag.PhotoTarget = string.Empty;
                ViewBag.Chat = null;
            }

            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] MessageModel messageModel)
        {
            string sender = HttpContext.User.Identity?.Name ?? string.Empty;

            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;

            string sentDate = now.Day == today.Day ? $"Today, {DateTime.Now.ToString("h:mm tt")}" : DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt");

            

            await _chatHub.SendMessageToGroup(sender, messageModel.Receiver, messageModel.Message, sentDate);

            return StatusCode(201);
        }
    }
}
