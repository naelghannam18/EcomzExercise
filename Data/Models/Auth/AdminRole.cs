using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomzExercise.Models.Auth
{
    [Table("admin_role")]
    public class AdminRole
    {
        [Column("id")]
        [Key]
        public int AdminRoles { get; set; }

        [Column("admin_role_name")]
        [Required]
        public string AdminRoleName { get; set; }

        [Column("admin_view_role")]
        [Required]
        public bool ViewRoles { get; set; }

        [Column("admin_manage_role")]
        [Required]
        public bool ManageRole { get; set; }

        [Column("admin_user_view")]
        [Required]
        public bool ViewUser { get; set; }

        [Column("admin_user_manage")]
        [Required]
        public bool ManageUser { get; set; }
    }
}