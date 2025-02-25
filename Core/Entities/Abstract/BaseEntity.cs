﻿namespace Core.Entities.Abstract
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
