using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TWTodos.Contexts;
using TWTodos.Models;

namespace TWTodos.Controllers;

public class TodoController : Controller
{
    private readonly TWTodosContext _context;

    public TodoController(TWTodosContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Lista de Tarefas";
        var todos = _context.Todos.ToList();
        return View(todos);
    }

    public IActionResult Create()
    {
        ViewData["Title"] =  "Nova Tarefa";
        return View("Form");
    }

    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        ViewData["Title"] = "Nova Tarefa";
        if(ModelState.IsValid)
        {
            todo.CreatedAt = DateTime.Now;
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Form", todo);

    }

    public IActionResult Edit(int id)
    {
        
        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        ViewData["Title"] = "Editar Tarefa";
        return View("Form", todo);

    }

[HttpPost]
public IActionResult Edit(Todo todo)
{
    ViewData["Title"] = "Editar Tarefa";

    if (ModelState.IsValid)
    {
        var originalTodo = _context.Todos.AsNoTracking().FirstOrDefault(t => t.Id == todo.Id);
        if (originalTodo == null)
        {
            return NotFound();
        }

        todo.CreatedAt = originalTodo.CreatedAt;

        _context.Todos.Update(todo);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View("Form", todo);
}

    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        ViewData["Title"] = "Deletar Tarefa";
        return View(todo);    
    }

    [HttpPost]
    public IActionResult Delete(Todo todo)
    {
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Finish(int id)
    {
        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        todo.Finish();
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}