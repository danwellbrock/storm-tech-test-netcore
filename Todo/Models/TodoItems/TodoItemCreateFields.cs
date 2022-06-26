using System.ComponentModel;
using Todo.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields
    {
        public int TodoListId { get; set; }

        public string Title { get; set; }
        public string TodoListTitle { get; set; }

        [DisplayName("Assigned to")]
        public string ResponsiblePartyId { get; set; }
        
        public Importance Importance { get; set; } = Importance.Medium;

        [Required()]
        public int Rank { get; set; }

        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            ResponsiblePartyId = responsiblePartyId;
        }
    }
}