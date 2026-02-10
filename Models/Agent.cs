namespace B2BManagement.Models
{
    public class Agent
    {
        public int AgentID { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string?  PasswordHash { get; set; }
        public string? ApiKey { get; set; }
        public DateTime? RegisteredOn { get; set; }
    }
}
