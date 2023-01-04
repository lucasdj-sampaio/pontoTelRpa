namespace PontoTelRpa.Domain.Models
{
    public class Credential
    {
        public string? User { get; set; }

        public string? Password { get; set; }

        public string? Url { get; set; }

        public ENavigationType? NavigationType { get; set; }
    }
}