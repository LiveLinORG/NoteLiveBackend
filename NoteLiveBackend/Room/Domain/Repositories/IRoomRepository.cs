using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Model.Entities.Room>
{
   Task<IEnumerable<Model.Entities.Room>> FindByPdfNameAsync(string pdfName);
   new Task<Model.Entities.Room?> FindByIdAsync(Guid id);
   Task<Model.Entities.Room?> FindByIdWithChatAsync(Guid id);
   Task<Model.Entities.Room?> FindByIdWithChatAndPdfAsync(Guid id);
   Task<Model.Entities.Room?> FindByNameAsync(string roomName);
   Task LoadUsersAsync(Room.Domain.Model.Entities.Room room);

   Task<Model.Entities.Room> GetRoomWithUsersAsync(Guid roomId);
   Task SaveAsync();
    Task<(byte[]?, IReadOnlyList<Question?>)> FindPdfAndQuestionsAsync(Guid id);
    Task<IEnumerable<User>> GetUsersByRoomIdAsync(Guid roomId);

}