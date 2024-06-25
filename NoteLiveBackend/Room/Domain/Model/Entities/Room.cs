using System.ComponentModel.DataAnnotations.Schema;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
 public class Room
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid CreadorId { get; set; }
        public User Creador { get; internal set; }
        public bool ChatActivated { get; set; }
        
        // Relaciones y colecciones
        public Guid? PdfId { get; set; }
        public PDF? PDF { get; private set; }
        
        public Guid? ChatId { get; set; }
        public Chat Chat { get; set; }
        
        private readonly List<User> _users = new List<User>();
        public IReadOnlyList<User> Users => _users.AsReadOnly(); 
        
        private readonly List<Question> _questions = new List<Question>();
        public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

        public Room(string name, Guid creadorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreadorId = creadorId;
            ChatActivated = true;
            PDF = new PDF(Id);
            Chat = new Chat(Id);
            
        }

        public void UploadPDF(PDF? pdf)
        {
            PDF = pdf;
        }

        public void AssociatePDF(IPDFCommandService pdfCommandService)
        {
            if (PdfId.HasValue)
            {
                PDF? pdfnew = pdfCommandService.associate(PdfId.Value); 
                UploadPDF(pdfnew);
            }
            else
            {
                throw new InvalidOperationException("No PDF ID is associated with this room.");
            }
        }

        public void AskQuestion(Question question)
        {
            _questions.Add(question);
        }

        public void AddUser(User user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
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

            await pdfExportService.ExportAsync(PDF, _questions);
        }

        public byte[] GetPDFContent()
        {
            return PDF?.Content;
        }
    }