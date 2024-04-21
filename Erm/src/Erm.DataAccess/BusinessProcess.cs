namespace Erm.src.Erm.DataAccess
{
    public class BusinessProcess
    {
        public int Id { get; set; }
        public required string Name { get; set; } = null!;
        public required string Domain { get; set; } = null!;

        public ICollection<RiskProfile> RiskProfiles { get; set; } = null!;
    }

}