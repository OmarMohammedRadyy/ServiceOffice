namespace ServiceOffice.Models
{
    public class WorkTask
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int AssignedEmployeeId { get; set; }
        public Employee AssignedEmployee { get; set; }
        public int? ReceivingEmployeeId { get; set; }
        public Employee ReceivingEmployee { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
