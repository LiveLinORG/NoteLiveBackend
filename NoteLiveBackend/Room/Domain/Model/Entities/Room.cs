using System.ComponentModel.DataAnnotations.Schema;
using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProfessorId { get; private set; }
    public List<Question> Questions { get; private set; }
    [NotMapped] // Esta propiedad no será mapeada a la base de datos

    public List<Guid> UserIds { get; private set; }

    // Propiedad PDF actualizada
    public PDF PDF { get; private set; }


    public Room(Guid id, string name, Guid professorId)
    {
        Id = id;
        Name = name;
        ProfessorId = professorId;
        Questions = new List<Question>();
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
        UserIds.Add(userId);
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