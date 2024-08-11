namespace SettlementService.Domain.Primitives
{

    /// <summary>
    /// Entity created with the purpose of being inherited by other entities
    /// </summary>
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
