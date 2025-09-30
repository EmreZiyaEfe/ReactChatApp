using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Business.Abstract
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<Message> CreateMessageAsync(Message message);
        Task<IEnumerable<MessageDto>> GetMessagesWithUserAsync(int userId);

    }
}
