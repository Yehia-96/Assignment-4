using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public abstract class Split
    {
        public static decimal CaptureValidateUserInput()
        {
            do
            {
                string userInput = Console.ReadLine();
                bool Validator = decimal.TryParse(userInput, out decimal convertedValue);
                if (!Validator)
                {
                    Console.WriteLine("The input must be a decimal!!!");
                }
                else
                {
                    return decimal.Parse(userInput);
                }
            } while (true);
        }
        public abstract void Splitting(Expenses expense);
        
    }

    public class SplitEqually : Split
    {
        public override void Splitting(Expenses expense)
        {
            decimal totalAmount = expense.amount;
            int numberOfParticipants = expense.subParticipantsE.Count;
            decimal equalAmount = totalAmount / numberOfParticipants;

            foreach (SubParticipant sub in expense.subParticipantsE)
            {
                sub.owe = equalAmount;
            }
        }
    }

    public class SplitByAmount : Split 
    {

        public override void Splitting(Expenses expense)
        {
            foreach (SubParticipant sub in expense.subParticipantsE)
            {
                UserInput(sub);
            }
        }
        public void UserInput(SubParticipant sub)
        {
            Console.WriteLine("{0} owes: ", sub.name);

            sub.owe = SplitByAmount.CaptureValidateUserInput(); 

        }
    }
}
