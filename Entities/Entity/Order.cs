using System;
using System.Collections.Generic;

namespace Entities.Entity
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public int? AddressId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
