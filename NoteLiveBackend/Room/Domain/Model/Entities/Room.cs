using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProfessorId { get; private set; }
    public List<Question> Questions { get; private set; }

    // Propiedad PDF actualizada
    public PDF PDF { get; private set; }

    public List<RoomUser> RoomUsers { get; set; }

    public Room(Guid id, string name, Guid professorId)
    {
        Id = id;
        Name = name;
        ProfessorId = professorId;
        Questions = new List<Question>();
        RoomUsers = new List<RoomUser>(); // Inicializa la lista de RoomUser
    }

    // Método para cargar un PDF en la sala
    public void UploadPDF(PDF pdf)
    {
        PDF = pdf;
    }

    // Método para realizar una pregunta en la sala
    public void AskQuestion(Question question)
    {
        Questions.Add(question);
    }

    // Método para agregar un usuario a la sala
    public void AddUser(Guid userId)
    {
        // Verifica si el usuario ya está asociado a la sala
        if (!RoomUsers.Any(ru => ru.UserId == userId))
        {
            // Crea una nueva instancia de RoomUser y agrégala a la lista RoomUsers
            RoomUsers.Add(new RoomUser { RoomId = Id, UserId = userId });
        }
    }
    // Método para exportar el PDF de la sala utilizando el servicio de exportación PDF
    public async Task ExportPDF(IPDFExportService pdfExportService)
    {
        if (PDF == null)
        {
            throw new PDFExportException();
        }

        await pdfExportService.ExportAsync(PDF, Questions);
    }
}