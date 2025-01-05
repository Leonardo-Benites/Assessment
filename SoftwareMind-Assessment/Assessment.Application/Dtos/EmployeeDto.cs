namespace Assessment.Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
