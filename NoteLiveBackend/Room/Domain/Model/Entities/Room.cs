using System.ComponentModel.DataAnnotations.Schema;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid CreadorId  { get; set; }
    public List<Question> Questions { get; private set; }
    public User Creador { get; internal set; }

    public bool ChatActivated { get; set; }
    [NotMapped]
    public List<Guid> UserIds { get; private set; }
        
    public PDF PDF { get; private set; }
        
    public Chat Chat { get; set; }

    public Room(string name, Guid creadorId)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreadorId  = creadorId;
        Questions = new List<Question>();
        UserIds = new List<Guid>();
        ChatActivated = true;
    }

    public void UploadPDF(PDF pdf)
    {
        PDF = pdf;
    }

    public void AskQuestion(Question question)
    {
        Questions.Add(question);
    }

    public void AddUser(Guid userId)
    {
        UserIds.Add(userId);
    }

    public void EndRoom()
    {
        ChatActivated = false;
    }
    public async Task ExportPDF(IPDFExportService pdfExportService)
    {
        if (PDF == null)
        {
            throw new PDFExportException();
        }

        await pdfExportService.ExportAsync(PDF, Questions);
    }
}