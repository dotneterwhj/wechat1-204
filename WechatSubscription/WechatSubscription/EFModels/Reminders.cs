using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WechatSubscription.EFModels
{
    [Table("Reminder")]
    public class Reminder : BaseModel
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public DateTimeOffset PreRemindTime { get; set; }

        public DateTimeOffset NextRemindTime { get; set; }

        public int IntervalDays { get; set; }
    }
}