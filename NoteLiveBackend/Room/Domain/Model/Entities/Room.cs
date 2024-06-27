using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Infrastructure.Persistence.EFC.Repositories;
using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
 public class Room
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid CreadorId { get; set; }
        [NotMapped]
        public User? Creador { get; internal set; }
        public bool Roomstarted { get; set; }
        public Guid? PdfId { get; set; }
        public PDF? PDF { get; set; }
        
        public Guid? ChatId { get; set; }
        public Chat Chat { get; set; }
        
        public ICollection<User> Users { get; set; } = new List<User>();

        private readonly List<Question> _questions = new List<Question>();
        public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

        public Room(string name, Guid creadorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreadorId = creadorId;
            Roomstarted = false;
            Chat = new Chat();
            Creador = new User(creadorId);
        }

        public Room(string name, User creador)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreadorId = creador.Id;
            Roomstarted = false;
            PDF = new PDF();
            Chat = new Chat();
            Creador = creador;
        }

        public void UploadPDF(byte[] content)
        {
            if (PDF == null)
            {
                PDF = new PDF(content);
                PdfId = PDF.Id;
            }
            else
            {
                PDF.Content = content;
            }
        }

        public void ActualizarPDF(PDF content)
        {
            PDF = content;
        }

        public void AskQuestion(Question question)
        {
            _questions.Add(question);
        }

        public void AddUser(User user)
        {
            if (!Users.Any(u => u.Id == user.Id))
            {
                Users.Add(user);
            }
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }

        public void EndRoom()
        {
            Roomstarted = false;
            Chat.isActivated = false;
        }

        public void StartRoom()
        {
            if (PDF?.Content != null)
            {
                throw new InvalidOperationException("Room cannot be started again when a PDF is already uploaded.");
            }
            Roomstarted = true;
            Chat.isActivated = true;
        }

        public byte[] GetPDFContent()
        {
            return PDF?.Content;
        }
    }
    