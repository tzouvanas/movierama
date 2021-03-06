﻿using Movierama.Server.Database.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movierama.Server.Models
{
    public class DateInThePastAttribute : ValidationAttribute
    {
        public DateInThePastAttribute()
        {
        }

        public string GetErrorMessage() =>
            $"Publication date cannot be in the future";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;

            if (movie.PublicationDate > DateTime.Now)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
