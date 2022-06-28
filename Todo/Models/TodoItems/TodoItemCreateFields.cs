using System.ComponentModel;
using Todo.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields
    {
        public string AlertCreateItemSuccessMsg { get; } = "Success! New item created.";
        public string AlertCreateItemErrorMsg { get; } = "Error! New item not created!";
        public int TodoListId { get; set; }

        public string Title { get; set; }
        public string TodoListTitle { get; set; }

        [DisplayName("Assigned to")]
        public string ResponsiblePartyId { get; set; }

        public Importance Importance { get; set; } = Importance.Medium;

        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            ResponsiblePartyId = responsiblePartyId;
        }
    }
}