
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Validation;

public class GreaterThanZeroAttribute: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        return value switch
        {
            int v => v > 0,
            long v => v > 0,
            decimal v => v > 0,
            double v => v > 0,
            float v => v > 0,
            _ => false
        };
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be greater than 0.";
    }
}
