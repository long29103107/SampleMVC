using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMVC.Data;
using SampleMVC.Dtos;
using SampleMVC.Models;
using SampleMVC.Services;

namespace SampleMVC.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _todoService.GetListAsync());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var todo = await _todoService.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: todo/create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CreateTodoRequest request)
        {
            var todo = new TodoResponse();

            if (ModelState.IsValid)
            {
                todo = await _todoService.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var todo = await _todoService.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UpdateTodoRequest request)
        {
            var todo = new TodoResponse();
           
            if (ModelState.IsValid)
            {
                if (!await _todoService.ExistedTodoAsync(id))
                {
                    return NotFound();
                }

                todo = await _todoService.UpdateAsync(id, request);

                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var todo = await _todoService.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _todoService.ExistedTodoAsync(id))
            {
                await _todoService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
