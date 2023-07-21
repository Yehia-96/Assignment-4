using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class Participant
    { 
        public int participantId { get; set; }
        public string name { get; set; }

        public List<Kitty> kitties { get; set; }
        public Participant(string name)
        {
            this.name = name;
            this.kitties = new List<Kitty>();
        }
        public static Participant CreateParticipant(MyDbContext dbContext)
        {

            string name;
            bool validInput = false;
            do
            {
                Console.WriteLine("Enter the name of the participant");
                name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("The name you have entered is invalid or empty");
                }
                else
                {
                    validInput = true;
                }
            } while (validInput == false);

            Participant participant = new Participant(name);
           

                dbContext.Participant.Add(participant);
                dbContext.SaveChanges();
            
                
            
            
            return participant;
        }
    }
    public class SubParticipant
    {
        public int subParticipantId { get; set;}

        public Kitty Kitty { get; set; }
        public Expenses Expense { get; set; }
        public int expenseId { get; set; }
        public int kittyId { get; set; } 
        
        public decimal owe;

        public string name;

        public SubParticipant(string name)
        {
            this.name = name;
        }

    }
}
