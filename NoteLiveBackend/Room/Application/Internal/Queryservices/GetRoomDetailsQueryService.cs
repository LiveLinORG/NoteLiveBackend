using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;

public class GetRoomDetailsQueryService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IPDFRepository _pdfRepository;

    public GetRoomDetailsQueryService(IRoomRepository roomRepository, IQuestionRepository questionRepository, IChatRepository chatRepository, IPDFRepository pdfRepository)
    {
        _roomRepository = roomRepository;
        _questionRepository = questionRepository;
        _chatRepository = chatRepository;
        _pdfRepository = pdfRepository;
    }

    public RoomDetailsDto Handle(GetRoomDetailsQuery query)
    {
        var room = _roomRepository.GetById(query.RoomId);
        var questions = _questionRepository.GetByRoomId(query.RoomId)
            .Select(q => new QuestionDto(q.Id, q.Text, q.Likes, q.UserName)).ToList();
        var chat = _chatRepository.GetByRoomId(query.RoomId);
        var chatMessages = chat.Messages.Select(m => new ChatMessageDto(m.Content, m.UserName)).ToList();
        var pdf = _pdfRepository.GetByRoomId(query.RoomId);

        var pdfDto = new PDFDto(pdf.Id, pdf.Content);

        return new RoomDetailsDto(room.Id, room.Name, room.Professor.Name, questions, chatMessages, pdfDto);
    }
}

