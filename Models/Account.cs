using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameStore.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength (50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(75)]
        public string Address { get; set; }
        [Required, StringLength(75)]
        public string City { get; set; }
        [Required, StringLength(2)]
        public string State { get; set; }
        [Required, StringLength(5)]
        public string ZipCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StoreCredit { get; set; } = 0m;

    }
}
