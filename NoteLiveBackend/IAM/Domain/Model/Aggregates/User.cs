namespace NoteLiveBackend.IAM.Domain.Model.Aggregates
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; } // "Esto determina si el usuario está creando actualmente o no una sala Profesor o alumno
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Correo { get; private set; }
        public long? CodigoProfesor { get; private set; } // vas a volarlo

        protected User() { }

        public User(string username, string password, string role, string name, string lastName, string correo, long? codigoProfesor = null)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Role = role;
            Name = name;
            LastName = lastName;
            Correo = correo;
            CodigoProfesor = codigoProfesor;
        }
    }
}