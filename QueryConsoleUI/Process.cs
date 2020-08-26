using Rest_with_json_linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueryConsoleUI
{
    public static class Process
    {
        /// <summary>
        /// Handles the automation of the console interaction
        /// </summary>
        /// <returns></returns>
        public static async Task ProcessRequest()
        {

            //Assigned a new instance of Receiver to carrier
            Receiver carrier = new Receiver();


            Begin:
            var rootUrl = "https://jsonmock.hackerrank.com/api/article_users/search?page=";
            Console.WriteLine("Please enter the page you want query: starting from '1' or type 'E' to exit");
            var choice = Console.ReadLine().ToLower();
            if (choice == "e")
                goto End;
            int numSelection;
            bool numericSelection = int.TryParse(choice, out numSelection);
            if (!numericSelection || numSelection < 1)
                goto Begin;
            var completeUrl = rootUrl + choice;
            Console.WriteLine("Please wait for few seconds...........\n");
            var returnedData = await carrier.MethodStore.getDataTask(completeUrl);
            if (returnedData == null)
            {
                Console.WriteLine("The Page you are trying to access does not exist");
                goto Begin;
            }
            else if (returnedData.Data.Count < 1)
            {
                Console.WriteLine("This page does not contain author data");
                goto Begin;
            }
            else if (returnedData.Data.Count > 0)
            {
                Console.WriteLine("What would you like to query?");
                Query:
                Console.WriteLine("\t\tTo retrieve the name of the author with the highest comment type 'A'");
                Console.WriteLine("\t\tTo retrieve the names of authors sorted by when their record was created 'B'");
                Console.WriteLine("\t\tTo retrieve the names of the most active authors 'C'");
                Console.WriteLine("\t\tTo Exit 'E'");
                var option = Console.ReadLine().ToLower();
                if (option != "a" && option != "b" && option != "c" && option != "d" && option != "e")
                {
                    Console.WriteLine("Please Chose a correct option");
                    goto Query;
                }
                if (option == "a")
                {
                    Console.WriteLine($"The user with the higest comment count is\n\t\t\t{carrier.MethodStore.GetUsernameWithHighestCommentCount()}");
                    Console.WriteLine();
                    Console.WriteLine("Do you want to perform another query?");
                    goto Query;
                }
                if (option == "b")
                {
                    Console.WriteLine("Please enter a threshold number of integer value");
                    Record:
                    int threshold;
                    var thresholdIsValid = int.TryParse(Console.ReadLine(), out threshold);
                    if (!thresholdIsValid)
                    {
                        Console.WriteLine("Please enter a valid ineger");
                        goto Record;
                    }
                    var authors = carrier.MethodStore.GetUsernamesSortedByRecordDate(threshold); if (authors.Count == 0)
                    {
                        Console.WriteLine("\n\nThere are no authors within this threshold\n\n");
                        goto Query;
                    }
                    Console.WriteLine("The following are authors that fall within this threshold\n\n");

                    foreach (var author in authors)
                    {
                        Console.WriteLine($"\t\t\t\t{author}");
                    }
                    Console.WriteLine("Do you want to perform another query?");
                    goto Query;
                }
                if (option == "c")
                {
                    Console.WriteLine("Please enter a threshold number of integer value");
                    Record:
                    int threshold;
                    var thresholdIsValid = int.TryParse(Console.ReadLine(), out threshold);
                    if (!thresholdIsValid)
                    {
                        Console.WriteLine("Please enter a valid ineger");
                        goto Record;
                    }
                    var authors = carrier.MethodStore.GetUsernames(threshold); if (authors.Count == 0)
                    {
                        Console.WriteLine("\n\nThere are no authors within this threshold\n\n");
                        goto Query;
                    }
                    Console.WriteLine("The following are the most active authors that fall within this threshold");

                    foreach (var author in authors)
                    {
                        Console.WriteLine($"\t\t\t\t{author}");
                    }
                    Console.WriteLine("Do you want to perform another query?");
                    goto Query;
                }
                if (option == "e")
                {
                    goto Begin;
                }
            }
            End:
            Console.WriteLine("Goodbye.......");

        }
    }
}
