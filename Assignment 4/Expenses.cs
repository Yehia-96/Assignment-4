using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class Expenses
    {
        public int expenseId;

        public Kitty kitty;
        public int kittyId { get; set; }
        public string type { get; set; }
        public decimal amount { get; set; }
        public string purpose { get; set; }
        public List<SubParticipant> subParticipantsE { get; set; }
     
        public Expenses(decimal amount, string purpose, string type) {
            this.type = type;
            this.amount = amount;
            this.purpose = purpose;
            this.subParticipantsE = new List<SubParticipant>();
        }
        public static Expenses CreateExpense(Kitty kitty, MyDbContext dbContext)
        {
            
            Console.WriteLine("What type of Expense? (ex. Groceries from Supermarket)");
            string expenseName = Console.ReadLine();
            Console.WriteLine("How much was spent on this Expense?");
            string amountSpent = Console.ReadLine();
            Console.WriteLine("Add a description to this Expense");
            string description = Console.ReadLine();
             
            Expenses newExpense = new Expenses(decimal.Parse(amountSpent), description, expenseName);
          
            newExpense.subParticipantsE = kitty.subParticipantsK.ToList();
            newExpense.kitty = kitty;
            kitty.expenses.Add(newExpense);
            dbContext.Expenses.Add(newExpense);

            dbContext.SaveChanges();

            return newExpense;
        }   


    }
}
