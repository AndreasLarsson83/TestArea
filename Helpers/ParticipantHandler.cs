using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArea.Helpers
{
    internal class ParticipantHandler
    {
        public static List<Participant> participantList = new List<Participant>();

        public static Participant CreateParticipant(string firstName, string lastName, string email)
        {
            Participant participant = new Participant();
            return participant;
        }
        public static Participant GetParticipantList()
        {
            foreach (var p in participantList)
            {
                Console.WriteLine("");
                Console.WriteLine($"Deltagarens namn: {p.FullName}");
                Console.WriteLine($"Deltagarens email: {p.Email}");
            }

        }

        public static void AddToList(Participant participant)
        {
            participantList.Add(participant);
            Console.WriteLine($"Participant {participant.FullName} was added to the participant list");
        }

        public static void RemoveFromList(Participant participant)
        {
            var index = participantList.IndexOf(participant);
            participantList.RemoveAt(index);

            Console.WriteLine($"Participant {participant.FullName} was removed form the participant list");
        }

        public static void ViewList(Participant participant)
        {
            Console.WriteLine("Lots of participants in here... Realy crowded right now...");
        }
    }
}
