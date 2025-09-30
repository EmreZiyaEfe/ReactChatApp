using Backend.Business.Abstract;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly AppDbContext _appDbContext;

        public MessageManager(IRepository<Message> messageRepository, AppDbContext appDbContext)
        {
            _messageRepository = messageRepository;
            _appDbContext = appDbContext;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            return await _messageRepository.AddAsync(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            return messages;
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesWithUserAsync(int userId)
        {
            var messages = await _appDbContext.Messages
        .Where(m => m.UserId == userId)
        .Select(m => new MessageDto
        {
            Id = m.Id,
            Text = m.Text,
            UserId = m.UserId,
            NickName = m.User.NickName,
            SentimentLabel = m.SentimentLabel
        })
        .OrderBy(m => m.Id) // veya CreatedAt varsa ona göre
        .ToListAsync();

            return messages;
        }
    }
}
