namespace PontoTelRpa.Domain.Models
{
    public class ProcessLog
    {
        public string? Message { get; set; }

        public DateTime ProcessTime { get; set; }

        public ENavigationType NavegationType { get; set; }
    }
}