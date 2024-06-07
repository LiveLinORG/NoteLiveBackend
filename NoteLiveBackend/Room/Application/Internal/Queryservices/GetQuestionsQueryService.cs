using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;
public class GetQuestionsQueryService
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionsQueryService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public IEnumerable<Question> Handle(GetQuestionsQuery query)
    {
        // Implementa la lógica para obtener las preguntas según el roomId
        // Por ejemplo:
        return _questionRepository.GetQuestionsByRoomId(query.RoomId);
        // Esto es solo un ejemplo, debes adaptarlo a tu lógica y estructura de datos
        throw new NotImplementedException();
    }
}

public interface IQuestionRepository
{

    Task<IEnumerable<Question>> GetByRoomId(Guid roomId);

    IEnumerable<Question> GetQuestionsByRoomId(Guid queryRoomId);
}