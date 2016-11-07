using System;
using System.Threading.Tasks;

namespace TodoApplication.Services
{
    public interface INotifierService
    {
        Task SendMessage(string @from, string to, string subject, string message, DateTime dateTime);
    }
}