using System.ComponentModel.DataAnnotations.Schema;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Infraestructure.Repositories;
using NoteLiveBackend.Room.Interfaces.REST.Transform;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
public class Chat
{
    public Guid Id { get; private set; }
    public bool isActivated { get; set; }
    public Chat()
    {
        Id = Guid.NewGuid();
    }
}
