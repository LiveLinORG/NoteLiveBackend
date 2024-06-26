using System.ComponentModel.DataAnnotations.Schema;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;

namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Question
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    [NotMapped]
    public Guid RoomId { get; private set; } 
    public string Text { get; private set; }
    public int Likes { get; private set; }

    public User User { get; set; } 
    public Room Room { get; set; }


    public Question(Guid userId, Guid roomId, string text) 
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RoomId = roomId;
        Text = text;
        Likes = 0;
    }
    private Question(){}
    public void Like()
    {
        Likes++;
    }
}
