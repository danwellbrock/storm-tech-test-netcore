﻿using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public string SortBy { get; set; }
        public bool HideDone { get; set; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, string sortBy, bool hideDone)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            SortBy = sortBy;
            HideDone = hideDone;
        }
    }
}