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
    static void ViewInformation(MusicDatabaseContext database)
    {
      var context = database;
      var songsWithAlbums = context.Songs.Include(song => song.Album);
      var bandsWithAlbums = context.Albums.Include(album => album.Artist);

      var response = PromptForString("\nWould you like to view: \n(B)ands \n(A)lbums \n");

      switch(response)
      {
        //Chosen to see band information
        case "B":
          response = PromptForString("\nWould you like to view \n(A)ll Bands \n(S)igned Bands \n(U)nsigned Bands \n");

          switch(response)
          {
            //View all bands
            case "A":
              foreach (var band in context.Artists)
              {
                Console.WriteLine($"\n{band.Name} is from {band.CountryOfOrigin} and has {band.NumberOfMembers} members, please visit their website at {band.Website} for more information.");
              }
              break;
            
            //View only bands that are signed
            case "S":
              //Makes a list of all of the signed bands
              var signedBands = context.Artists.Where(band => band.IsSigned == true);

              //If there is 1 or more, it will print out each band 
              if(signedBands.Count() > 0)
              {
                foreach (var band in context.Artists)
                {
                  if(band.IsSigned == true)
                  {
                    Console.WriteLine($"\n{band.Name} is signed. If you would like to book them, please contact their manager, {band.ContactName}, at {band.ContactPhoneNumber}.");
                  }
                }
              }
              //Otherwise prints no bands
              else
              {
                Console.WriteLine("\nThere are no bands that are signed.");  
              }
              break;
            
            //View only bands that are unsigned
            case "U":
              var unSignedBands = context.Artists.Where(band => band.IsSigned == false);

              //If there is 1 or more, it will print out each band 
              if(unSignedBands.Count() > 0)
              {
                Console.WriteLine("\nHere are the list of bands that are no longer signed and making music: \n");
                foreach (var band in context.Artists)
                {
                  if(band.IsSigned == false)
                  {
                    Console.WriteLine($"{band.Name}");
                  }
                }
              }
              //Otherwise prints no bands
              else
              {
                Console.WriteLine("\nThere are no bands that are signed.");  
              }
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
              if(context.Albums.Count() > 0)
              {
                Console.WriteLine("\nWe have the following albums on file: \n");
                foreach(var album in bandsWithAlbums)
                {
                  Console.WriteLine($"{album.Title} \n   made by {album.Artist.Name}");
                }
              }
              else
              {
                Console.WriteLine("Oops, looks like there are no albums in this database!");
              }
              break;
            
            //View albums from a specific band
            case "S":
              //Prints out list of available bands in database for user to choose from
              Console.WriteLine($"Here are the following bands we have in this database: \n");
              foreach(var band in context.Artists)
              {
                Console.WriteLine($"{band.Name}");
              }

              response = PromptForString("\nWhich band's albums do you want to view? ");
              //Locates if what the user inputted is a band in the system
              var foundArtist = context.Artists.FirstOrDefault(band => band.Name.ToUpper() == response);
              //If it locates a valid band, will print out their albums
              if(foundArtist != null)
              {
                var foundAlbums = context.Albums.Where(band => band.Artist == foundArtist).ToList();
                
                //Prints all albums from the found band if there is more than 1 album
                if(foundAlbums.Count() > 0)
                {
                  Console.WriteLine($"\n{foundArtist.Name} has {foundAlbums.Count()} albums. Here is the list: \n");

                   for(var count = 0; count < foundAlbums.Count(); count++)
                  {
                    var currentAlbum = foundAlbums[count];
                    //Makes a list of the songs from the current album
                    var foundSongs = context.Songs.Where(s => s.AlbumId == currentAlbum.Id).ToList();
                    Console.WriteLine($"\n{currentAlbum.Title} debuted on {currentAlbum.ReleaseDate} and has a total of {foundSongs.Count()} songs, which include: \n");
                    //Prints out all the songs from the album
                    foreach(var track in foundSongs)
                    {
                      Console.WriteLine($"{track.TrackNumber}: {track.Title} \n   Duration {track.Duration}");
                    }
                  }
                }
                //Prints if the found band has no albums
                else
                {
                  Console.WriteLine($"\n{foundArtist.Name} has no albums yet.");
                }
              }
              //Otherwise if the user inputs something incorrectly, will not do anything
              else
              {
                Console.WriteLine("No such band was found. Please try again.");
              }
              break;

            //If user inputs invalid input
            default:
              Console.WriteLine("\nI am not sure what you are saying. Please try again.");
              break;
          }
          break;
        
        default:
          Console.WriteLine("\nI am not sure what you are saying. Please try again.");
          break;
      }
    }
    static void Main(string[] args)
    {
      var context = new MusicDatabaseContext();  

      var bandsWithAlbums = context.Albums.Include(album => album.Artist);
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
          
          //Will be chosen to see information about bands and albums and calls method which will do so
          case "V":
            ViewInformation(context);
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
