using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestArea.Models;

namespace TestArea.Helpers
{
    internal class MenuHandler
    {
        public static List<Participant> participantList = new List<Participant>();

        public static List<Participant> removeParticipantList = new List<Participant>();

        public static List<RebateCode> generatedRebateCodeList = new List<RebateCode>();

        public static string FilePath = "";

        public static void ParticipantMenu()
        {
            int option = -1;

            do
            {
                Console.WriteLine("######################################");
                Console.WriteLine(@"###      //PARTICIPANT MENU\\      ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("### 1. Create                      ###");
                Console.WriteLine("### 2. Remove                      ###");
                Console.WriteLine("### 3. Show all                    ###");
                Console.WriteLine("### 4. Rebate code                 ###");
                Console.WriteLine("### 0. Quit                        ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("######################################");
                Console.WriteLine("");
                Console.Write("Select an option (0-4): ");
                option = Console.ReadKey().KeyChar;

                Console.Clear();

                switch (option)
                {
                    case '1':
                        CreateParticipant();
                        break;
                    case '2':
                        RemoveParticipant();
                        break;
                    case '3':
                        ShowParticipantList();
                        break;
                    case '4':
                        GenerateRebateCode();
                        break;
                }

                Console.Clear();
            }
            while (option != '0');
        }

        public static void CreateParticipant()
        {
            int option = -1;
            
            Participant participant = new Participant();

            participant.Name = "";
            participant.Last = "";
            participant.Number = "";

            do
            {
                Console.WriteLine("######################################");
                Console.WriteLine(@"###   //ENTER PARTICIPANT INFO\\   ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine($"### 1. Name: {participant.Name.PadRight(20, ' ')}  ###");
                Console.WriteLine($"### 2. Last: {participant.Last.PadRight(20, ' ')}  ###");
                Console.WriteLine($"### 3. Number: {participant.Number.PadRight(18, ' ')}  ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("### 4. Save to list                ###");
                Console.WriteLine("### 0. Back                        ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("######################################");
                Console.WriteLine("");
                Console.Write("Select an option (0-4): ");
                option = Console.ReadKey().KeyChar;
                Console.Write("\n");

                switch (option)
                {
                    case '1':
                        Console.Write("Enter name: ");
                        participant.Name = Console.ReadLine();
                        break;
                    case '2':
                        Console.Write("Enter last: ");
                        participant.Last = Console.ReadLine();
                        break;
                    case '3':
                        Console.Write("Enter number: ");
                        participant.Number = Console.ReadLine();
                        break;
                    case '4':
                        if (participant.Name != "")
                        {
                            if (participant.Last != "")
                            {
                                if (participant.Number != "")
                                {

                                    // Look if number already exist in list
                                    //participantList = participantList.Where(x => x.Number != enteredRebateCode).ToList();

                                    /*if (participant.Number != participantList)
                                    {
                                       A
                                    }*/
                                    participantList.Add(participant);
                                    Console.WriteLine("Atempting to save...");
                                    Console.WriteLine("(press any key to continue)");
                                    Console.ReadKey();
                                    option = '0';
                                    break;
                                }
                                Console.WriteLine("Missing a number");
                                Console.WriteLine("(press any key to continue)");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Have not entered last name");
                            Console.WriteLine("(press any key to continue)");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Fill out the form pleas");
                        Console.WriteLine("(press any key to continue)");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
            while (option != '0');
        }

        public static void ShowParticipantList()
        {
            int option = -1;
            int listCount = 0;

            do 
            {
                Console.WriteLine("######################################");
                Console.WriteLine(@"###        //PARTICIPANTS\\        ###");
                Console.WriteLine("###--------------------------------###");
                foreach(var p in participantList)
                {
                    listCount++;
                    string participantInfo = listCount + ". " + p.FullName + " " + p.Number;
                    Console.WriteLine($"### {participantInfo.PadRight(31, ' ')}###");
                }
                listCount = 0;
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine($"### File: {FilePath.PadRight(24, ' ')} ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("### 1. Save to file                ###");
                Console.WriteLine("### 4. Load from file              ###");
                Console.WriteLine("### 0. Back                        ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("######################################");
                Console.WriteLine("");
                Console.Write("Select an option (0-4): ");
                option = Console.ReadKey().KeyChar;
                Console.WriteLine("");

                switch (option)
                {
                    case '1':
                        Console.Write("Enter a file name: ");
                        string userSaveFile = Console.ReadLine();
                        if (userSaveFile != "")
                        {
                            string[] lines = new string[participantList.Count];
                            for (int i = 0; i < participantList.Count; i++)
                            {
                                lines[i] = $"{participantList[i].Name},{participantList[i].Last},{participantList[i].Number}";
                            }

                            File.WriteAllLines(@$"{FilePath}{userSaveFile}.csv", lines);
                        }
                        else
                        {
                            Console.WriteLine("No filename entered... Try again");
                            Console.WriteLine("(press any key)");
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        Console.Write("Enter a file name: ");
                        string userLoadFile = Console.ReadLine();

                        if (File.Exists($@"{FilePath}{userLoadFile}.csv"))
                        {
                            using (StreamReader sr = new StreamReader($@"{FilePath}{userLoadFile}.csv"))
                            {
                                var line = "";
                                while ((line = sr.ReadLine()) != null)
                                {
                                    var values = line.Split(",");

                                    Participant participant = new Participant();
                                    participant.Name = values[0];
                                    participant.Last = values[1];
                                    participant.Number = values[2];

                                    participantList.Add(participant);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No");
                            Console.ReadKey();
                        }
                        break;
                   /* case '3':
                        break;*/
                }

                Console.Clear();
            }
            while (option != '0');
        }

        public static void RemoveParticipant()
        {
            int option = -1;
            Participant remove = new();
            var selectNumber = "";
            var yesNo = ' ';

            do
            {
                Console.WriteLine("######################################");
                Console.WriteLine(@"###        //CANCELLATION\\        ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine($"### Name: {remove.Name.PadRight(23, ' ')}  ###");
                Console.WriteLine($"### Last: {remove.Last.PadRight(23, ' ')}  ###");
                Console.WriteLine($"### Number: {remove.Number.PadRight(21, ' ')}  ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("### 1. Select                      ###");
                Console.WriteLine("### 4. Remove                      ###");
                Console.WriteLine("### 0. Back                        ###");
                Console.WriteLine("###--------------------------------###");
                Console.WriteLine("######################################");
                Console.WriteLine("");
                Console.Write("Select an option (0-4): ");
                option = Console.ReadKey().KeyChar;
                Console.Write("\n");

                switch (option)
                {
                    case '1':
                        Console.Write("Enter number: ");
                        selectNumber = Console.ReadLine();
                        removeParticipantList = participantList.Where(x => x.Number == selectNumber).ToList();
                        remove.Name = removeParticipantList.First().Name;
                        remove.Last = removeParticipantList.First().Last;
                        remove.Number = removeParticipantList.First().Number;
                        
                        break;
                    case '4':
                        Console.Write("You sure? (y/n): ");
                        yesNo = Console.ReadKey().KeyChar;
                        switch (yesNo)
                        {
                            case 'y':
                                participantList = participantList.Where(x => x.Number != selectNumber).ToList();
                                Console.Write("\n");
                                Console.WriteLine($"Have been terminated: {remove.FullName}");
                                Console.WriteLine("(press any key to continue)");
                                Console.ReadKey();
                                option = '0';
                                break;
                            case 'n':
                                break;
                        }
                        break;
                }

                Console.Clear();
            }
            while (option != '0');
        }

        public static void GenerateRebateCode()
        {
            int option = -1;
            
            do
            {
                Console.WriteLine("############################################");
                Console.WriteLine(@"###         //VALID CODE LIST\\          ###");
                Console.WriteLine("###--------------------------------------###");
                foreach (var item in generatedRebateCodeList)
                {
                    Console.WriteLine($"### {item.Code.PadRight(31, ' ')} ###");
                }
                Console.WriteLine("###--------------------------------------###");
                Console.WriteLine("### 1. Generate Code                     ###");
                Console.WriteLine("### 2. Use Code                          ###");
                Console.WriteLine("### 0. Back                              ###");
                Console.WriteLine("###--------------------------------------###");
                Console.WriteLine("############################################");
                Console.WriteLine("");
                Console.Write("Select an option (0-2): ");
                option = Console.ReadKey().KeyChar;
                Console.Write("\n");
                switch (option)
                {
                    case '1':
                        Guid guid = Guid.NewGuid();
                        RebateCode rebateCode = new();
                        rebateCode.Code = $"{guid}";
                        Console.WriteLine($"Test {rebateCode.Code}");
                        generatedRebateCodeList.Add(rebateCode);
                        break;
                    case '2':
                        Console.Write("Enter a rebatecode: ");
                        string enteredRebateCode = Console.ReadLine() ?? "";
                        Console.WriteLine($"Set to be consumed: {enteredRebateCode}");
                        Console.WriteLine("(press any key to continue)");
                        Console.ReadKey();
                        generatedRebateCodeList = generatedRebateCodeList.Where(x => x.Code != enteredRebateCode).ToList();
                        Console.Clear();
                        break;
                }
                Console.Clear();
            }
            while (option != '0');
        }
    }
}
