using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static List<string> reports = new List<string>();
    static string filePath = "report_it.csv";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        LoadReports();

        int option;
        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           REPORT LOG SYSTEM            ║");
            Console.WriteLine("║              IT SUPPORT                ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.WriteLine("Choose an Option \n");
            Console.WriteLine("1 - New Report");
            Console.WriteLine("2 - Show all reports");
            Console.WriteLine("3 - Change a report status");
            Console.WriteLine("4 - Exit");

            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    NewReport();
                    break;

                case 2:
                    ShowReports();
                    break;

                case 3:
                    ChangeStatus();
                    break;

                case 4:
                    Console.WriteLine("Leaving...");
                    break;

                default:
                    Console.WriteLine("Invalid Option.");
                    break;
            }

            if (option != 4)
            {
                Console.WriteLine("\nPress Any Key to Continue...");
                Console.ReadKey();
            }

        } while (option != 4);
    }

    static void LoadReports()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                reports.Add(lines[i]);
            }
        }
    }

    // New Report
    static void NewReport()
    {
        Console.Clear();
        Console.WriteLine("New Report \n");

        Console.Write("Requester Name: ");
        string requester = Console.ReadLine();

        Console.Write("Sector: ");
        string sector = Console.ReadLine();

        Console.Write("Reported Problem: ");
        string problem = Console.ReadLine();

        Console.Write("Priority (Low, Medium, High): ");
        string priority = Console.ReadLine();

        string status = "Open";

        string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        // CSV
        string CSVline = $"{date},{requester},{sector},{problem},{priority},{status}";

        if (!File.Exists(filePath))
        {
            string header = "Date,Caller,Sector,Problem,Priority,Status\n";
            File.WriteAllText(filePath, header);
        }

        File.AppendAllText(filePath, CSVline + "\n");

        reports.Add(CSVline);

        // Success Message
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nReport registered with success");
        Console.ResetColor();

        Console.WriteLine($"File saved in: {Path.GetFullPath(filePath)}");
    }

    static void ShowReports()
    {
        Console.Clear();
        Console.WriteLine("Reports List\n");

        if (reports.Count == 0)
        {
            Console.WriteLine("No reports registered");
            return;
        }

        for (int i = 0; i < reports.Count; i++)
        {
            string[] data = reports[i].Split(',');

            Console.WriteLine($"[{i + 1}] - {data[0]} | {data[1]} | {data[2]} | Priority: {data[4]} | Status: {data[5]}");
            Console.WriteLine($"     Problem: {data[3]}\n");
        }
    }

    static void ChangeStatus()
    {
        Console.Clear();
        Console.WriteLine("Change Status \n");

        if (reports.Count == 0)
        {
            Console.WriteLine("No reports registered");
            return;
        }

        ShowReports();

        Console.Write("\nReport Number: ");
        int num = int.Parse(Console.ReadLine()) - 1;

        if (num >= 0 && num < reports.Count)
        {
            Console.WriteLine("\nStatus Options:");
            Console.WriteLine("1 - Open");
            Console.WriteLine("2 - In Progress");
            Console.WriteLine("3 - Solved");

            Console.Write("\nChoose a new status: ");
            int optionStatus = 0;
bool validStatus = false;

while (!validStatus)
{
    Console.Write("Choose new status (1, 2 or 3): ");
    string input = Console.ReadLine();
    
    if (int.TryParse(input, out optionStatus) && optionStatus >= 1 && optionStatus <= 3)
    {
        validStatus = true;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid option! Please enter 1, 2, or 3.");
        Console.ResetColor();
    }
}
}
    }
        }