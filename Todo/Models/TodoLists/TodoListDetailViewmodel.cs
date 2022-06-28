using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public string AlertSortSuccessMsg { get; } = "Success! Rank updated.";
        public string AlertSortErrorMsg { get; } = "Error! Rank update failed";

        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public string SortBy { get; set; }

        public TodoItemCreateFields TodoItemCreateFields { get; set; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, string sortBy)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            SortBy = sortBy;
            TodoItemCreateFields = new TodoItemCreateFields();
        }
    }
}