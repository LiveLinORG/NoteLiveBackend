using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class PDF
{
    public Guid Id { get; private set; }
    public byte[]? Content { get; set; }


    public PDF(byte[] content)
    {
        Id = Guid.NewGuid();
        Content = content;
    }
    public PDF()
    {
        Id = Guid.NewGuid();
    }
}