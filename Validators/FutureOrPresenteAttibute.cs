using System.ComponentModel.DataAnnotations;

namespace TWTodos.Validators;

public class FutureOrPresenteAttribute : ValidationAttribute
{
    public FutureOrPresenteAttribute()
    {
        ErrorMessage = "{0}, não pode ser menor que o dia atual ";
    }

    public override bool IsValid(object? value)
    {
        if(value is null)
        { 
            return true; 
        }
        var date = (DateOnly)value;
        return date >= DateOnly.FromDateTime(DateTime.Now);
    }
}