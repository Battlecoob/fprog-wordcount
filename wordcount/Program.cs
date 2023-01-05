using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCount
{
    class Program
    {
        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static IEnumerable<string> GetWordsOther(string text)
        {
            return text.Split(' ');
        }

        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static string ReplaceSymbols(string text)
        {
            // Use a regular expression to replace all non-word and non-number characters with spaces
            Regex regex = new Regex(@"[^a-zA-Z0-9]");
            return regex.Replace(text, " ");
        }
        
        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static IEnumerable<string> GetWords(string text)
        {
            // Use a regular expression to only include words and numbers in the output
            Regex regex = new Regex(@"\b[\w\d]+\b");

            return regex.Matches(text).Cast<Match>().Select(match => match.Value);
        }

        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static IEnumerable<(string Word, int Count)> CountWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .Select(group => (group.Key, group.Count()));
        }

        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static void PrintWordCounts(IEnumerable<(string Word, int Count)> wordCounts)
        {
            foreach ((string word, int count) in wordCounts.OrderByDescending(pair => pair.Count))
            {
                Console.WriteLine($"{word} -> {count}");
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Error: Please provide a directory path and file extension.");
            Console.WriteLine("Usage: dotnet run /path/to/directory .file-extension");
        }

        static void DirectoryError(string directoryPath)
        {
            Console.WriteLine($"Error: The directory '{directoryPath}' does not exist.");
        }

        static void Main(string[] args)
        {
            // Check if there are enough command line arguments
            
            if (args.Length != 2)
            {
                PrintUsage();
                return;
            }
            

            // Get the directory path and file extension from the command line arguments
            //string directoryPath = args[0];
            //string fileExtension = args[1];

            string directoryPath = "/Users/fabian/My Drive/Studium/compSci_Dual/5_Sem/FPROG/Wordcount-Projekt/fprog-wordcount/wordlists";
            string fileExtension = ".txt";
            
            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                DirectoryError(directoryPath);
                return;
            }

            // Check if the file extension is valid
            if (!fileExtension.StartsWith("."))
            {
                //Console.WriteLine($"Error: '{fileExtension}' is not a valid file extension. It should start with a '.' character.");
                fileExtension = "." + fileExtension; // maybe not functional
            }

            // Use a higher-order function to enumerate all the files in the directory that match the file extension
            // and read their contents, resulting in a list of lists of words
            IEnumerable<IEnumerable<string>> wordLists;
            try
            {
                wordLists = Directory
                    .EnumerateFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories)
                    .Select(File.ReadAllText)
                    .Select(ReplaceSymbols)
                    .Select(GetWords);
            
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Access to the directory is denied.");
                return;
            }
            catch (IOException)
            {
                Console.WriteLine("Error: An I/O error occurred.");
                return;
            }
            catch
            {
                Console.WriteLine("An unknown error occured.");
                return;
            }

            // Flatten the list of lists of words into a single list of words
            IEnumerable<string> words = wordLists.SelectMany(list => list);

            // Use a higher-order function to count the words and sort them by frequency
            IEnumerable<(string Word, int Count)> wordCounts = CountWords(words)
                .OrderByDescending(pair => pair.Count);

            Console.WriteLine("..");

            // Print the word counts
            PrintWordCounts(wordCounts);
        }
    }
}
