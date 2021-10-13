using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Console.WriteLine("to add enter 1: ");
            Console.WriteLine("to update enter 2: ");
            Console.WriteLine("to show enter 3: ");
            Console.WriteLine("to show list enter 4: ");
            Console.WriteLine("to exit enter 5: ");
            while (choice != 5)
            {
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("to add a station enter 1: ");
                        Console.WriteLine("to add a drone enter 2: ");
                        Console.WriteLine("to add a customer enter 3: ");
                        Console.WriteLine("to add a parcial enter 4: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            default:
                                break;
                        }

                        break;
                    case 2:
                        Console.WriteLine("to conect a parcial to a drone enter 1: ");
                        Console.WriteLine("to collect a parcial by a drone enter 2: ");
                        Console.WriteLine("to supply a parcial to a customer enter 3: ");
                        Console.WriteLine("to send a drone to charge in a station enter 4: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("to display a station enter 1: ");
                        Console.WriteLine("to display a drone enter 2: ");
                        Console.WriteLine("to display a customer enter 1: ");
                        Console.WriteLine("to display a parcial enter 1: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        Console.WriteLine("to display the station list enter 1: ");
                        Console.WriteLine("to display the drone list enter 2: ");
                        Console.WriteLine("to display the customer list enter 3: ");
                        Console.WriteLine("to display the parcial list enter 4: ");
                        Console.WriteLine("to display the list of parcials that are free enter 5: ");
                        Console.WriteLine("to display the list of station that have free chargers enter 5: ");
                        break;
                    case 5:

                    default:
                        Console.WriteLine("not a good choice try again");
                        break;
                }
            }
        }
    }
}
