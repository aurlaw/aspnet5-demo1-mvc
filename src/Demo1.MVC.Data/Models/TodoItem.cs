using System;
using Microsoft.Data.Entity;

namespace Demo1.MVC.Data.Models {
    
    public class TodoItem {
        
        
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        
        public DateTime DateCreated {get; set;}
        public DateTime DateModified {get;set;}
        
        public ApplicationUser User {get; set;}
        
        
        public TodoItem() {
            this.DateCreated = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
        }
        
    }
    
    
}