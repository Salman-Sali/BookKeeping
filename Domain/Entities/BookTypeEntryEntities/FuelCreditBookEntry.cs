﻿using Domain.Enums;

namespace Domain.Entities.BookTypeEntryEntities
{
    public interface FuelCreditBookEntry
    {
        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public decimal? Litre { get; set; }
        public ItemType? ItemType { get; set; }
    }
}
