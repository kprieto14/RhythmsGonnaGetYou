using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
//using CsvHelper;
//using CsvHelper.Configuration;

namespace RhythmsGonnaGetYou
{
  class Program
  {
    static string PromptForString(string prompt)
    {
      //This method will always call for a prompt to print, then read what the user inputs into the console and returns the response
      Console.Write(prompt);
      var userInput = Console.ReadLine().ToUpper();

      return userInput;
    }
    static int PromptForInteger(string prompt)
    {
      //This method will always call for a prompt to print, then read what the user inputs into the console, check if it is an integer, and either return a correct response or a 0
      Console.Write(prompt);
      int userInput;
      var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);
      
      if (isThisGoodInput)
      {
        return userInput;
      }
      else
      {
        Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
        return 0;
      }
    }
    static void Main(string[] args)
    {
      var context = new MusicDatabaseContext();  

      var bandsWithAlbums = context.Albums.Include(album => album.Band);
      var songsWithAlbums = context.Songs.Include(song => song.Album);

      Console.WriteLine("🎶🎵 Welcome to my Music Database 🎶🎵");

      var keepGoing = true;
      while(keepGoing == true)
      {
        var response = PromptForString("\nWhat would you like to do during your visit? \n(A)dd New Information \n(V)iew Information \n(E)dit Band Information \n(Q)uit \n");

      
        switch(response)
        {
          //Will add bands, albums, and/ or songs
          case "A":
            response = PromptForString("\nWould you like to add a new: \n(B)and \n(A)lbum \n(S)ong \n");
            
            switch(response)
            {
              //To add a new Band
              case "B":
                Console.WriteLine("\nYou have chosen to add a new Band!");
                break;

              //To add a new album
              case "A":
                Console.WriteLine("\nYou have chosen to add a new album!");
                break;

              //To add a new song
              case "S": 
                Console.WriteLine("\nYou have chosen to add a new song!");
                break;
              
              default:
                Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                break;
            }
            break;
          
          //Will be chosen to see information about bands and albums
          case "V":
            response = PromptForString("\nWould you like to view: \n(B)ands \n(A)lbums \n");

            switch(response)
            {
              //Chosen to see band information
              case "B":
                response = PromptForString("\nWould you like to view \n(A)ll Bands \n(S)igned Bands \n(U)nsigned Bands \n");

                switch(response)
                {
                  //View all albums
                  case "A":
                    foreach (var band in context.Artists)
                    {
                      Console.WriteLine($"\n{band.Name} is from {band.CountryOfOrigin} and has {band.NumberOfMembers} members.");
                    }
                    break;
                  
                  //View only bands that are signed
                  case "S":
                    Console.WriteLine("\nYou have chosen to view signed bands!");
                    break;
                  
                  //View only bands that are unsigned
                  case "U":
                    Console.WriteLine("\nYou have chosen to view unsigned bands!");
                    break;
                  
                  default:
                    Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                    break;
                }
                break;

              //Chosen to see album information
              case "A":
                response = PromptForString("\nWould you like to view \n(A)ll Albums \n(S)earch for Albums From a Specific Band \n");

                switch(response)
                {
                  //View all albums
                  case "A":
                    Console.WriteLine("\nYou have chosen to view all albums!");
                    break;
                  
                  //View albums from a specific band
                  case "S":
                    Console.WriteLine("\nYou have chosen to view albums from a certain band!");
                    break;
                  
                  default:
                    Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                    break;
                }
                break;
              
              default:
                Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                break;
            }
            break;
          
          //Chosen to either Sign or Unsign a band
          case "E":
            response = PromptForString("\nWould you like to \n(L)et a Band Go \n(R)esign a Band");

            switch(response)
            {
              //Let Unsign band
              case "L":
                Console.WriteLine("\nTime to put a band out of commission.");
                break;
              
              //Re-sign band
              case "R":
                Console.WriteLine("\nLets sign them out of retirement!");
                break;
              
              default:
                Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                break; 
            }
            break;
          
          //Quit Program
          case "Q":
            Console.WriteLine("\nCome back and see us soon!");
            keepGoing = false;
            break;
          
          //If user enters anything other than A, V, E, or Q.
          default:
            Console.WriteLine("\nI am not sure what you are saying. Please try again.");
            break;
        }
      }
    }
  }
}
