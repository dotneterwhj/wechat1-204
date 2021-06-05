using System;
using System.ComponentModel.DataAnnotations;

namespace WechatSubscription.EFModels
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset LastModifyTime { get; set; }

        public int Creator { get; set; }

        public int LastModifer { get; set; }
    }
}