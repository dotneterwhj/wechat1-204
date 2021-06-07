using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackgroundJob.EFModels
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset LastModifyTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Creator { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastModifer { get; set; }
    }
}