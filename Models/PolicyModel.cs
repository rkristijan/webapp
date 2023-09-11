namespace webapp.Models
{
    public class PolicyModel
    {

        public Guid Id { get; set; }
        
        public string serialNumber { get; set; }
        
        public string insurerName { get; set; }

        public string policyType { get; set; }

        public DateTime startDate {  get; set; }

        public DateTime endDate { get; set; }

        public string premiumAmount { get; set; }

    }
}


