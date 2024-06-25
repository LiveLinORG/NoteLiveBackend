﻿using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

public record RoomResource(
    Guid id,
    string Name,
    Guid ProfessorId,
    IReadOnlyList<Question> Questions,
    User Creador,
    IReadOnlyList<User> UserIds,
    PDF? Pdf,
    Chat Chat);