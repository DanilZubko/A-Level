using System;
using System.Collections.Generic;

class Program
{
    class TodoItem
    {
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }
    }

    static List<TodoItem> todoList = new List<TodoItem>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter one of the commands (add-item, remove-item, mark-as, show, exit):");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "add-item":
                    AddItem();
                    break;

                case "remove-item":
                    RemoveItem();
                    break;

                case "mark-as":
                    MarkAs();
                    break;

                case "show":
                    Show();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid command...");
                    break;
            }
        }
    }

    static void AddItem()
    {
        Console.WriteLine("Enter an item or task to add:");
        string task = Console.ReadLine().ToLower();

        if (!todoList.Exists(item => item.Task.ToLower() == task))
        {
            TodoItem newItem = new TodoItem { Task = task };
            todoList.Add(newItem);
            Console.WriteLine("Task added successfully.");
        }
        else
        {
            Console.WriteLine("Task already exists...");
        }
    }

    static void RemoveItem()
    {
        Console.WriteLine("Enter an item or task to remove or '*' to remove all:");
        string task = Console.ReadLine().ToLower();

        if (task == "*")
        {
            todoList.Clear();
            Console.WriteLine("All tasks removed successfully.");
        }
        else
        {
            TodoItem itemToRemove = todoList.Find(item => item.Task.ToLower() == task);
            if (itemToRemove != null)
            {
                todoList.Remove(itemToRemove);
                Console.WriteLine("Task removed successfully.");
            }
            else
            {
                Console.WriteLine("Task not found...");
            }
        }
    }

    static void MarkAs()
    {
        Console.WriteLine("Enter an item or task to mark:");
        string task = Console.ReadLine().ToLower();

        TodoItem itemToMark = todoList.Find(item => item.Task.ToLower() == task);
        if (itemToMark != null)
        {
            Console.WriteLine("Enter status (1 to mark as completed, 0 not completed):");
            if (int.TryParse(Console.ReadLine(), out int status) && (status == 0 || status == 1))
            {
                itemToMark.IsCompleted = (status == 1);

                if (status == 1)
                {
                    Console.WriteLine("Enter completion date (leave blank for current date):");
                    string dateInput = Console.ReadLine();
                    if (DateTime.TryParse(dateInput, out DateTime completionDate))
                    {
                        itemToMark.CompletionDate = completionDate;
                    }
                    else
                    {
                        itemToMark.CompletionDate = DateTime.Now.Date;
                    }
                }
                else
                {
                    itemToMark.CompletionDate = DateTime.MinValue;
                }

                Console.WriteLine("Task status updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid status...");
            }
        }
        else
        {
            Console.WriteLine("Task not found...");
        }
    }

    static void Show()
    {
        Console.WriteLine("Enter status (1 to show completed tasks, 0 for not completed, leave blank for all):");
        string statusInput = Console.ReadLine();

        if (string.IsNullOrEmpty(statusInput))
        {
            foreach (var item in todoList)
            {
                Console.WriteLine($"{item.Task} - Status: {(item.IsCompleted ? "Completed" : "Not Completed")}, Completion Date: {item.CompletionDate}");
            }
        }
        else if (int.TryParse(statusInput, out int status) && (status == 0 || status == 1))
        {
            foreach (var item in todoList)
            {
                if (item.IsCompleted == (status == 1))
                {
                    Console.WriteLine($"{item.Task} - Completion Date: {item.CompletionDate}");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid status...");
        }
    }
}