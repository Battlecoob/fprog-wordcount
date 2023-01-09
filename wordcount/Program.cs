using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Immutable;

namespace WordCount
{
    class Program
    {
        /*
         * 
         * Functional Concepts:
         *      Purity                  ... our functions are pure bc they do not have any side effects and always returns the same
         *                                      result for the same input
         *      Immutability            ... IEnumerable<T> are readonly
         *                                  functions param / variable don't change but aren't immutable
         *      Higher-Order Functions  ... wordLists
         *                                  wordCounts  
         *      Side-effects            ... limited as much as possible
         *                                  besides "fileextension" no variable is changed / reused; that is done for errorhandling
         *      Lamda                   ... used throughout the program                
         */

        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static IEnumerable<string> GetWordsOther(string text)
        {
            return text.Split(" ");
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
        static IEnumerable<(string Word, int Count)> CountWords(in IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .Select(group => (group.Key, group.Count()));
        }

        // This function is pure because it does not have any side effects and always returns the same
        // result for the same input. It also does not depend on any external state.
        static void PrintWordCounts(in IEnumerable<(string Word, int Count)> wordCounts)
        {
            foreach ((string word, int count) in wordCounts.OrderByDescending(pair => pair.Count))
            {
                Console.WriteLine($"{word} -> {count}");
            }
        }

        static void DirectoryError(in string directoryPath)
        {
            Console.WriteLine($"Error: The directory '{directoryPath}' does not exist.");
        }

        static void Main(string[] args)
        {
            // Check if there are the right amount of command line arguments
            if (args.Length != 2)
            {
                Console.WriteLine("Error: Please provide a directory path and file extension."); // not functional?
                return;
            }

            // Get the directory path and file extension from the command line arguments
            string directoryPath = args[0];
            string fileExtension = args[1];


            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                DirectoryError(directoryPath);
                return;
            }

            // Check if the fileextension starts with "."
            if (!fileExtension.StartsWith("."))
            {
                fileExtension = "." + fileExtension;
            }

            //bool isText = fileExtension == ".txt" ? true : false;

            // Use a higher-order function to enumerate all the files in the directory that match the file extension
            // and read their contents, resulting in a list of lists of words
            IEnumerable<IEnumerable<string>> wordLists;

            //if(isText)
            //{
            wordLists = Directory
                .EnumerateFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories)
                .Select(File.ReadAllText)
                .Select(ReplaceSymbols)
                .Select(GetWords); // calling only ".Select(GetWords)" leaves us with a " " entry in the wordLists
            //}
            //else
            //{
            //    wordLists = Directory
            //        .EnumerateFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories)
            //        .Select(File.ReadAllText)
            //        .Select(GetWordsOther);
            //}

            // Flatten the list of lists of words into a single list of words
            IEnumerable<string> words = wordLists.SelectMany(list => list);

            // Use a higher-order function to count the words and sort them by frequency
            IEnumerable<(string Word, int Count)> wordCounts = CountWords(words)
                .OrderByDescending(pair => pair.Count);

            // Print the word counts
            PrintWordCounts(wordCounts);
        }
    }
}
