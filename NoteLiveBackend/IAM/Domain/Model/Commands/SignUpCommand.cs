namespace NoteLiveBackend.IAM.Domain.Model.Commands
{
    public record SignUpCommand(string Username, string Password, string Role, string Name, string LastName, string Correo)
    {
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
                throw new ArgumentException("Username must not be empty", nameof(Username));
            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("Password must not be empty", nameof(Password));
            if (string.IsNullOrWhiteSpace(Role))
                throw new ArgumentException("Role must not be empty", nameof(Role));
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name must not be empty", nameof(Name));
            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("LastName must not be empty", nameof(LastName));
            if (string.IsNullOrWhiteSpace(Correo))
                throw new ArgumentException("Correo must not be empty", nameof(Correo));
        }
    }
}