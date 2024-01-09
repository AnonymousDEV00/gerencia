using BCrypt.Net;

namespace GerenciaVendas.Services
{
    public class HashService
    {
        // Esta constante pode ser ajustada para aumentar a segurança, dependendo das necessidades
        private const int WorkFactor = 10;

        public string HashPassword(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha não pode ser nula ou em branco.", nameof(senha));
            }

            return BCrypt.Net.BCrypt.HashPassword(senha, WorkFactor);
        }

        public bool VerifyPasswordHash(string senha, string hashSalvo)
        {
            if (string.IsNullOrWhiteSpace(hashSalvo))
            {
                throw new ArgumentException("Hash salvo não pode ser nulo ou em branco.", nameof(hashSalvo));
            }

            return BCrypt.Net.BCrypt.Verify(senha, hashSalvo);
        }
    }
}

