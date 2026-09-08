using System;
using System.ComponentModel.DataAnnotations;


namespace ParkingSystem.Validation
{
    public class NotPassAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return value is DateTime dateTimeValue
                && dateTimeValue <= DateTime.Now;
        }
    }
}
