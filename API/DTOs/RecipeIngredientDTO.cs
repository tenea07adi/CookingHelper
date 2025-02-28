﻿using Core.Entities.Persisted;

namespace API.DTOs
{
    public class RecipeIngredientDTO
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public string MeasureUnitName { get; set; }
        public decimal Quantity { get; set; }
    }
}
