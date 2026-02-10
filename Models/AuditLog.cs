using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace B2BManagement.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int AgentID { get; set; }
        public string? Action { get; set; } = string.Empty;
        public string? EntityType { get; set; } = string.Empty;
        public string? Details { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditID { get; set; }
        public string? Description { get; set; }


    }
}
