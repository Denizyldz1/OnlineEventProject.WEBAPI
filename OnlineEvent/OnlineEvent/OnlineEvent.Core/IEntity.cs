﻿namespace OnlineEvent.Core
{
    public interface IEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
