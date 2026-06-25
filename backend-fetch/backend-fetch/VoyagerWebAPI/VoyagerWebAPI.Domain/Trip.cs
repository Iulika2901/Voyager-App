using System;
 using System.Collections.Generic;
 using System.Linq;
 
 namespace VoyagerWebAPI.Domain;
 
 public class Trip
 {
     public int Id { get; set; } 
     public string Name { get; set; }
     public Destination Destination { get; set; }
     public DateTime StartDate { get; set; }
     public DateTime EndDate { get; set; }
     public TripStatus Status { get; set; } 
     public decimal Budget { get; set; }
     
     public List<Expense> Expenses { get; set; } = new List<Expense>();
 
     public decimal TotalExpenses => Expenses.Sum(e => e.Amount);
     public decimal RemainingBudget => Budget - TotalExpenses;
     public int DurationInDays => (EndDate - StartDate).Days;
 
     private Trip() { }
     
     public Trip(string name, Destination destination, DateTime startDate, DateTime endDate, TripStatus status, decimal budget)
     {
         if (string.IsNullOrWhiteSpace(name))
             throw new ArgumentException("Name cannot be empty");
         
         if (endDate < startDate)
             throw new ArgumentException("End date cannot be before start date");
 
         if (destination == null)
             throw new ArgumentNullException(nameof(destination), "Destination cannot be null");
         
         Name = name;
         Destination = destination;
         StartDate = startDate;
         EndDate = endDate;
         Status = status;
         Budget = budget;
     }
 }