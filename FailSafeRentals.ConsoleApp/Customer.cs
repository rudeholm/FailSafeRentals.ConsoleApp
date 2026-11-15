using System.ComponentModel.DataAnnotations;

namespace FailSafeRentals.ConsoleApp
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Required, MaxLength(255)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(20)]
        public string DriverLicenseNumber { get; set; } = null!;

        public List<Rental> Rentals { get; set; } = new();
    }
}
