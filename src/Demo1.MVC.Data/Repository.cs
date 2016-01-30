using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Demo1.MVC.Data.Models;
namespace Demo1.MVC.Data {
    
    public class Repository {
        
        
        public Repository() {
        }
        
        
        public IQueryable GetTodoItems() {
            using(var _context = new ApplicationDbContext())
            {
                return _context.TodoItems;
            }
        }
        
        public void SaveTodo(TodoItem item)
        {
            using(var _context = new ApplicationDbContext())
            {
                bool itemExists = _context.TodoItems.Any(t => t.Id == item.Id);
                item.DateModified = DateTime.UtcNow;
                
                _context.Entry(item).State = itemExists ? EntityState.Modified : EntityState.Added;
                _context.SaveChanges();
            }
        }
    }
}