using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Domain.Services;

public class ChatService
{
    public void SendMessage(Chat chat, ChatMessage message)
    {
        chat.Messages.Add(message);
        // Lógica adicional para manejar el mensaje
    }
}
