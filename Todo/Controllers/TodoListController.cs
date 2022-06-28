using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, string sortBy)
        {
            var todoList = dbContext.SingleTodoList(todoListId);

            // filter items by importance or rank
            todoList.Items = sortBy switch
            {
                "rankAsc" => todoList.Items.OrderBy(item => item.Rank).ToList(),
                "rankDesc" => todoList.Items.OrderByDescending(item => item.Rank).ToList(),
                _ => todoList.Items.OrderBy(item => item.Rank).ToList(),
            };

            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, sortBy);
            return View(viewmodel);
        }

        public IActionResult DetailTodoListItemsPartial(int todoListId, string sortBy)
        {
            var todoList = dbContext.SingleTodoList(todoListId);

            todoList.Items = sortBy switch
            {
                "rankAsc" => todoList.Items.OrderBy(item => item.Rank).ToList(),
                "rankDesc" => todoList.Items.OrderByDescending(item => item.Rank).ToList(),
                _ => todoList.Items.OrderBy(item => item.Rank).ToList(),
            };

            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, sortBy);
            return PartialView("_TodoListItemsPartial", viewmodel);
        }

        public IActionResult GravatarProfilePartial(string email)
        {
            return PartialView(new TodoListGravatarViewmodel(email));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new { todoList.TodoListId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRank(int[] todoItemIDs)
        {
            try
            {
                var rank = 1;

                foreach (var id in todoItemIDs)
                {
                    var todoItem = dbContext.SingleTodoItem(id);
                    todoItem.Rank = rank;
                    dbContext.Update(todoItem);
                    await dbContext.SaveChangesAsync();

                    rank++;
                }

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}