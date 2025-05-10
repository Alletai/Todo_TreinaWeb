using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TWTodos.Validators;

namespace TWTodos.Models;

public class Todo
{
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "{0} não pode ser vazio.")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres")]
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Data de entrega")]
    [Required(ErrorMessage = "Selecione uma {0}")]
    [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureOrPresente]
    public DateOnly Deadline { get; set; }
    public DateOnly ? FinishedAt { get; set; }
    
    public void Finish()
    {
        FinishedAt = DateOnly.FromDateTime(DateTime.Now);
    }
}