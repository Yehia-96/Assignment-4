using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Assignment_4
{
    public class Kitty
    {
        public enum currency
        {
            EUR,
            USD,
            EGP
        }
        public int kittyId { get; set; }
        public string eventName { get; set; }
        public currency money { get; set; }
        public Participant participant { get; set; }
        public int participantId { get; set; }

        public List<Expenses> expenses;
        public List<SubParticipant> subParticipantsK;

        public Kitty() { }

        public Kitty(string name, currency money = currency.EUR)
        {
            this.eventName = name;
            this.money = money;
            this.expenses = new List<Expenses>();
            this.subParticipantsK = new List<SubParticipant>();
        }
        public void AddKittyMembers()
        {
            SubParticipant currentPariticpant = new SubParticipant(this.participant.name);
            currentPariticpant.Kitty = this;
            this.subParticipantsK.Add(currentPariticpant);
            Console.WriteLine("Add members to this Kitty");
            Thread.Sleep(1000);
            bool addMore = true;
            int i = 1;
            do
            {
                Console.WriteLine("Member {0}: ", i);
                string subName = Console.ReadLine();
                SubParticipant newSub = new SubParticipant(subName);
                newSub.Kitty = this;
                this.subParticipantsK.Add(newSub);
                Console.WriteLine("Do you wish to add more?");
                Thread.Sleep(1000);
                Console.WriteLine("Press 1 if yes, press 2 if no");

                string choice = Console.ReadLine();

                int parsedChocie = int.Parse(choice);

                if (parsedChocie == 2)
                {
                    addMore = false;
                }
                i++;
            } while (addMore);

        }
        public static Kitty CreateKitty(MyDbContext dbContext, Participant participant)
        {
            Console.WriteLine("Enter a name for your Kitty!");
            string eventname = Console.ReadLine();

            Console.WriteLine("Choose Currency");
            Thread.Sleep(1000);
            Console.WriteLine("1.EUR");
            Console.WriteLine("2.USD");
            Console.WriteLine("3.EGP");

            int choice = int.Parse(Console.ReadLine());

            Kitty newKitty = new Kitty(eventname);
            newKitty.money = newKitty.GetCurrency(choice);

            newKitty.participant = participant;
            participant.kitties.Add(newKitty);

            dbContext.Kitty.Add(newKitty);
            dbContext.SaveChanges();
            return newKitty;
        }
        public currency GetCurrency (int choice)
        {
            if(choice >= 1 && choice <= 3)
            {
                return (currency)(choice - 1);
            }
            return 0;
        }

       
    }
}
