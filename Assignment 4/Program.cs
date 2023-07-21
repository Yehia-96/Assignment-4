using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Assignment_4
{
    public class KittyMimic
    {
        public static void Main(string[] args)
        {
            
        
        Console.WriteLine("Welcome to KittyMimic!");
            Thread.Sleep(2000);
            bool exitRequest = false;
            while (!exitRequest)
            {
                Console.WriteLine("Please enter your name");
                string user = Console.ReadLine();
                using var dbContext = new MyDbContext();
                bool isNewUser = false;
                isNewUser = Initiate(dbContext, user);
                if (isNewUser)
                {
                    int thisParticipantId = dbContext.Participant
                                          .Where(p => p.name == user)
                                          .Select(p => p.participantId)
                                          .First();
                    Participant newParticiapnt = dbContext.Participant.Single(p => p.participantId == thisParticipantId);
                    Kitty newKitty = new Kitty();
                    Console.WriteLine("Choose an option!");
                    Thread.Sleep(1000);
                    Console.WriteLine("1.View Expense record");
                    Console.WriteLine("2.New Kitty");
                    Console.WriteLine("3.New Expense");
                    Console.WriteLine("4.Delete Kitty");
                    Console.WriteLine("5.Delete Expense");
                    int switchChoice = int.Parse(Console.ReadLine());
                    bool flagIt = false;
                    switch (switchChoice)
                    {

                        case 1:

                            ViewChosenExpense(dbContext, thisParticipantId); break;

                        case 2:
                            flagIt = true;
                            newKitty = Kitty.CreateKitty(dbContext, newParticiapnt);
                            newKitty.AddKittyMembers();
                            goto case 3;
                        case 3:
                            if (flagIt)
                            {
                                Console.WriteLine("Add an Expense to the newly created Kitty");
                                Expenses addExpense = Expenses.CreateExpense(newKitty, dbContext);
                                ChooseSplitMethod(addExpense, dbContext);
                                Console.WriteLine("New updated list");
                                var display = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Choose which Kitty you would like to add an Expense to");
                                var fetchKitty = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
                                int kittychoice = int.Parse(Console.ReadLine());
                                Kitty thisKitty = dbContext.Kitty.Single(k => k.eventName == fetchKitty[kittychoice - 1]);

                                List<Expenses> fetchExpenses = dbContext.Kitty.Where(k => k.kittyId == thisKitty.kittyId )
                                                                              .SelectMany(k => k.expenses)
                                                                              .ToList();
                                
                                thisKitty.expenses = fetchExpenses;
                                List<SubParticipant> fetchSubs = dbContext.Kitty.Where(k => k.kittyId == thisKitty.kittyId)
                                                                              .SelectMany(k => k.subParticipantsK)
                                                                              .ToList();
                               
                                thisKitty.subParticipantsK = fetchSubs;

                                Expenses newExpense = Expenses.CreateExpense(thisKitty, dbContext);
                                ChooseSplitMethod(newExpense, dbContext);
                                Console.WriteLine("New updated list");
                                var display = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
                                break;
                            }

                        case 4:
                            
                            DeletionOfKitty(dbContext, thisParticipantId); 

                            break;

                        case 5:
                            DeletionOfExpense(dbContext, thisParticipantId); break;

                        default:
                            Console.WriteLine("Invalid choice");
                            break;

                    }
                    Console.WriteLine("Do you wish to continue? y/n");
                    string exitChoice = Console.ReadLine();
                    if (exitChoice.Equals("n"))
                    {
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(1000);
                        exitRequest = true;
                    }
                }

                else
                {
                    Participant newParticipant = Participant.CreateParticipant(dbContext);
                    Console.WriteLine("Create a new Kitty");
                    Thread.Sleep(1000);

                    Kitty newKittyForNewParticipant = Kitty.CreateKitty(dbContext, newParticipant);

                    newKittyForNewParticipant.AddKittyMembers();
                    Expenses expense = Expenses.CreateExpense(newKittyForNewParticipant, dbContext);
                    ChooseSplitMethod(expense, dbContext);

                    Console.WriteLine("Added Successfully!");
                    Thread.Sleep(1000);

                    Console.WriteLine("Do you wish to continue? y/n");
                    string exitChoice = Console.ReadLine();
                    if (exitChoice.Equals("n"))
                    {
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(1000);
                        exitRequest = true;
                    }
                }
            }
           
        }
       

        public static bool Initiate(MyDbContext dbContext, string user)
        {
           
                Participant searchDatabase = dbContext.Participant.SingleOrDefault(p => p.name == user);
                if (searchDatabase == null)
                {
                    Console.WriteLine("User doesn't exist please create a new user");
                    Thread.Sleep(1000);
                    return false;
                }
                return true;
            

            

        }

        public static void ChooseSplitMethod(Expenses expense, MyDbContext dbContext)
        {
            Console.WriteLine("Choose Split 1.SplitEqually 2.SplitByAmount");
            int choiceSplit = int.Parse(Console.ReadLine());
            if (choiceSplit == 1)
            {
                SplitEqually eqaully = new SplitEqually();
                eqaully.Splitting(expense);
               
                dbContext.SaveChanges();
            }
            else if (choiceSplit == 2)
            {
                SplitByAmount amount = new SplitByAmount();
                amount.Splitting(expense);
                dbContext.SaveChanges();
            }
        }
        public static void ViewChosenExpense(MyDbContext dbContext, int thisParticipantId)
        {
            List<string> usersKitties = dbContext.Kitty
                                                   .Where(k => k.participantId == thisParticipantId)
                                                   .Select(k => k.eventName)
                                                   .ToList();
            if (usersKitties == null)
            {
                Console.WriteLine("Please create a new Kitty first!");
                Thread.Sleep(1000);
                Console.WriteLine("Exiting program");
                Environment.Exit(1);

            }
            for (int i = 0; i < usersKitties.Count; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, usersKitties[i]);
            }
            string kittyChoice = usersKitties[int.Parse(Console.ReadLine()) - 1];
            int chosenKittyId = dbContext.Kitty
                                .Where(k => k.eventName == kittyChoice)
                                .Select(k => k.kittyId)
                                .First();
            List<string> thisKittyExpenses = dbContext.Expenses
                                             .Where(e => e.kittyId == chosenKittyId)
                                             .Select(e => e.type)
                                             .ToList();
            for (int i = 0; i < thisKittyExpenses.Count; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, thisKittyExpenses[i]);
            }
            string expenseChoice = thisKittyExpenses[int.Parse(Console.ReadLine()) - 1];
            int chosenExpenseId = dbContext.Expenses.Where(e => e.type == expenseChoice)
                                                   .Select(e => e.expenseId)
                                                   .First();
            Expenses thisExpense = dbContext.Expenses.Single(e => e.expenseId == chosenExpenseId);

            List<SubParticipant> thisExpenseSubs = dbContext.Expenses.Where(e => e.expenseId == chosenExpenseId)
                                                      .SelectMany(e => e.subParticipantsE)
                                                      .ToList();

            Console.WriteLine("{0} Total Amount: {1}", thisExpense.type, thisExpense.amount);
            foreach (SubParticipant sub in thisExpenseSubs)
            {
                Console.WriteLine("{0} owes: {1}", sub.name, sub.owe);
            }
            Console.WriteLine("Description: {0}", thisExpense.purpose);
        }
        public static void DeletionOfKitty(MyDbContext dbContext, int thisParticipantId)
        {
            Console.WriteLine("Choose which Kitty you'd like to delete");
            List<string> fetchKittyToDelete = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
            int kittychoiceToDelete = int.Parse(Console.ReadLine());
            Kitty kittyRemoval = dbContext.Kitty.Single(k => k.eventName == fetchKittyToDelete[kittychoiceToDelete - 1]);
            dbContext.Remove(kittyRemoval);
            dbContext.SaveChanges();
            List<string> fetchKittyUpdated = dbContext.Kitty.Where(k => k.participantId == thisParticipantId)
                                                   .Select(k => k.eventName)
                                                   .ToList();
            for (int i = 0; i < fetchKittyUpdated.Count; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, fetchKittyUpdated[i]);
            }
            Console.WriteLine("New updated list");
            fetchKittyToDelete = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
            Console.WriteLine("Delete Succeeded!");
        }

        public static void DeletionOfExpense(MyDbContext dbContext, int thisParticipantId)
        {
            Console.WriteLine("Choose which Kitty you would like to Delete an Expense of");
            List<string> fetchKittyToDeleteExpense = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
            int kittyChoiceToDeleteExpense = int.Parse(Console.ReadLine());
            Kitty selectedKitty = dbContext.Kitty.Single(k => k.eventName == fetchKittyToDeleteExpense[kittyChoiceToDeleteExpense - 1]);
            List<string> fetchExpensesToDelete = dbContext.Expenses.Where(e => e.kittyId == selectedKitty.kittyId)
                                                                  .Select(e => e.type)
                                                                  .ToList();
            for (int i = 0; i < fetchExpensesToDelete.Count; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, fetchExpensesToDelete[i]);
            }
            int expenseChoiceToDelete = int.Parse(Console.ReadLine());
            Expenses expenseToDelete = dbContext.Expenses.Single(e => e.type == fetchExpensesToDelete[expenseChoiceToDelete - 1]);
            
            dbContext.Remove(expenseToDelete);
            dbContext.SaveChanges();

            List<string> fetchExpensesUpdated = QueryAndDisplayOptionsKitties(dbContext, thisParticipantId);
            Console.WriteLine("Expense Removed Successfully!"); 
        }

        public static List<string> QueryAndDisplayOptionsKitties(MyDbContext dbContext, int thisParticipantId)
        {
            List<string> QueryIt = dbContext.Kitty.Where(k => k.participantId == thisParticipantId)
                                                     .Select(k => k.eventName)
                                                     .ToList();
            
            Thread.Sleep(1000);
            for (int i = 0; i < QueryIt.Count; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, QueryIt[i]);
            }
            return QueryIt; 
        }
    }
    
}