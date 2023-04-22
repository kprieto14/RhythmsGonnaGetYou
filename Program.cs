using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
  class Program
  {
    static string PromptForString(string prompt, bool upper)
    {
      var useUpper = upper;
      //This method will always call for a prompt to print, then read what the user inputs into the console and returns the response
      Console.Write(prompt);
      
      //If the program calls for the string to be in all UpperCase
      if(useUpper == true)
      {
        var userInput = Console.ReadLine().ToUpper();
        return userInput;
      }
      //Otherwise, will send what the userinputs as is
      else
      {
        var userInput = Console.ReadLine();
        return userInput;
      }
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
      var bandsWithAlbums = context.Albums.Include(album => album.Artist);

      var response = PromptForString("\nWould you like to view: \n(B)ands \n(A)lbums \n", true);

      switch(response)
      {
        //Chosen to see band information
        case "B":
          response = PromptForString("\nWould you like to view \n(A)ll Bands \n(S)igned Bands \n(U)nsigned Bands \n", true);

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
                Console.WriteLine("\nThere are no bands that are unsigned.");  
              }
              break;
            
            default:
              Console.WriteLine("\nI am not sure what you are saying. Please try again.");
              break;
          }
          break;

        //Chosen to see album information
        case "A":
          response = PromptForString("\nWould you like to view \n(A)ll Albums \n(S)earch for Albums From a Specific Band \n", true);

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

              response = PromptForString("\nWhich band's albums do you want to view? ", true);
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
    static Artist NewBand()
    {
      //Method that will be called to add a new band
      string contact;
      string number;
      bool signedBand;
      Console.WriteLine("\nLets get started then! Punctuation/ format will matter for the next few questions. ");

      var name = PromptForString("\nWhat is the name of the Band? ", false);
      var country = PromptForString("What is their country of origin? ", false);
      var memberAmount = PromptForInteger("How many members does the band have? ");
      var website = PromptForString("What is their website? ", false);
      var genre = PromptForString("What genre are they? ", false);
      var sign = PromptForString("Are they still signed? (Y/N) ", true);
      //Only asks for manager information if the band is signed
      if(sign == "Y")
      {
        signedBand = true;
        contact = PromptForString("Who is their manager? ", false);
        number = PromptForString("What is their manager's phone number? (Please enter in a xxx-xxx-xxxx format) ", false);
      }
      else
      {
        signedBand = false;
        contact = null;
        number = null;
      }

      var newBand = new Artist
      {
          Name = name,
          CountryOfOrigin = country,
          NumberOfMembers = memberAmount,
          Website = website,
          Style = genre,
          IsSigned = signedBand,
          ContactName = contact,
          ContactPhoneNumber = number
      };

      return newBand;
    }
    static Album AddAlbum(Artist band, MusicDatabaseContext database)
    {
      var newBand = band;
      var context = database;

      //If the program didn't send a band, will ask user to prompt for one
      if(newBand == null)
      {
        Console.WriteLine("\nWe have the following bands in the database:\n");
        foreach (var artist in context.Artists)
        {
          Console.WriteLine($"{artist.Name}");
        }

        //While loop that keeps going until the user enters a valid band to work with
        var keepGoing = true;
        while(keepGoing == true)
        {
          var response = PromptForString("\nWhich band would you like to add an album for? ", true);
          //Locates if what the user inputted is a band in the system
          var foundArtist = context.Artists.FirstOrDefault(band => band.Name.ToUpper() == response);

          if(foundArtist != null)
          {
            Console.WriteLine($"You have chosen to work with {foundArtist.Name}.");
            newBand = foundArtist;
            keepGoing = false;
          }
          else
          {
            Console.WriteLine("Sorry that is an invalid choice. Please try again.");
          }
        }
      }

      var albumName = PromptForString("\nWhat is the name of the album? ", false);
      var warning = PromptForString("Does this album need and advisory warning? [Y/N] ", true);
      bool warn = true;
      if (warning == "Y")
      {
          warn = true;
      }
      if (warning == "N")
      {
          warn = false;
      }
      var releaseDate = PromptForString("When was this album released? (MM/DD/YYYY) ", false);

      var newAlbum = new Album
      {
          Title = albumName,
          IsExplicit = warn,
          ReleaseDate = releaseDate,
          ArtistId = newBand.Id,
      };

      Console.Write($"\nYou have added the album {newAlbum.Title} for {newBand.Name}. ");
      return newAlbum;
    }
    static Song AddSong(Album album, MusicDatabaseContext database)
    {
      var newAlbum = album;
      var context = database;

      var track = PromptForString("What is the track number? ", false);
      var songName = PromptForString("What is the name of the song? (Punctuation matters) ", false);
      var duration = PromptForString("What is the duration of the song? (MM:SS format) ", false);

      var newSong = new Song
      {
          TrackNumber = track,
          Title = songName,
          Duration = duration,
          AlbumId = newAlbum.Id
      };

      Console.WriteLine($"{newSong.Title} has been added to {newAlbum.Title}");

      return newSong;
    }
    static void Main(string[] args)
    {
      var context = new MusicDatabaseContext();  

      Console.WriteLine("🎶🎵 Welcome to my Music Database 🎶🎵");

      var keepGoing = true;
      while(keepGoing == true)
      {
        var response = PromptForString("\nWhat would you like to do during your visit? \n(A)dd New Information \n(V)iew Information \n(E)dit Band Information \n(Q)uit \n", true);

      
        switch(response)
        {
          //Will add bands, albums, and/ or songs
          case "A":
            response = PromptForString("\nWould you like to add a new: \n(B)and \n(A)lbum \n(S)ong \n", true);
            
            switch(response)
            {
              //To add a new Band
              case "B":
                var newBand = NewBand();
                //Calls method that will gather information about the new band and save it to the database
                context.Artists.Add(newBand);
                context.SaveChanges();
                response = PromptForString($"\nYou have chosen to add {newBand.Name} to our database! Would you like to add an album to their information? (Y/N) ", true);

                //Asks if user would like to add an album to the new band
                if(response == "Y")
                {
                  var addNewAlbum = AddAlbum(newBand, context);
                  context.Albums.Add(addNewAlbum);
                  context.SaveChanges();

                  //Asks if they would like to add songs to the new album
                  response = PromptForString($"\nWould you like to add any songs to that album? ", true);
                  //Adds new songs
                  if(response == "Y")
                  {
                    var keepAdding = true;
                    while(keepAdding == true)
                    {
                      //While loop that continues the user to keep adding as many songs to ONE album at a time
                      var newSong = AddSong(addNewAlbum, context);
                      context.Songs.Add(newSong);
                      context.SaveChanges();

                      response = PromptForString("\nWould you like to add another song? (Y/N) ", true);
                      if(response == "Y")
                      {
                        Console.WriteLine("Lets keep adding then!");
                      }
                      else if(response == "N")
                      {
                        var songCount = context.Songs.Where(song => song.AlbumId == addNewAlbum.Id);
                        Console.WriteLine($"Okay! Lets stop adding songs. {addNewAlbum.Title} has {songCount.Count()} amount of songs added.");
                        keepAdding = false;
                      }
                      else
                      {
                        var songCount = context.Songs.Where(song => song.AlbumId == addNewAlbum.Id);
                        Console.WriteLine($"\nI am not sure what you are saying. I will assume you mean 'No'. {addNewAlbum.Title} has {songCount.Count()} amount of songs added.");
                        keepAdding = false;
                      }
                    }
                  }
                  //Will not add any new songs
                  else if(response == "N")
                  {
                    Console.WriteLine("No problem! You can always add more songs by restarting the menu.");
                  }
                  else
                  {
                    Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                  }
                }
                //Will not add any new albums
                else if (response == "N")
                {
                  Console.WriteLine("No problem! You can always add more albums by restarting the menu.");
                }
                else
                {
                  Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                }
                break;

              //To add a new album
              case "A":
                var newAlbum = AddAlbum(null, context);
                context.Albums.Add(newAlbum);
                context.SaveChanges();

                //Asks if they would like to add songs to the new album
                response = PromptForString($"\nWould you like to add any songs to that album? ", true);

                if(response == "Y")
                {
                  var keepAdding = true;
                  while(keepAdding == true)
                  {
                    //While loop that continues the user to keep adding as many songs to ONE album at a time
                    var newSong = AddSong(newAlbum, context);
                    context.Songs.Add(newSong);
                    context.SaveChanges();

                    response = PromptForString("\nWould you like to add another song? (Y/N) ", true);
                    if(response == "Y")
                    {
                      Console.WriteLine("Lets keep adding then!");
                    }
                    else if(response == "N")
                    {
                      var songCount = context.Songs.Where(song => song.AlbumId == newAlbum.Id);
                      Console.WriteLine($"Okay! Lets stop adding songs. {newAlbum.Title} has {songCount.Count()} amount of songs added.");
                      keepAdding = false;
                    }
                    else
                    {
                      var songCount = context.Songs.Where(song => song.AlbumId == newAlbum.Id);
                      Console.WriteLine($"\nI am not sure what you are saying. I will assume you mean 'No'. {newAlbum.Title} has {songCount.Count()} amount of songs added.");
                      keepAdding = false;
                    }
                  }
                }
                //Will not add any new songs
                else if(response == "N")
                {
                  Console.WriteLine("No problem! You can always add more songs by restarting the menu.");
                }
                else
                {
                  Console.WriteLine("\nI am not sure what you are saying. Please try again.");
                }
                break;

              //To add a new song
              case "S":
                //Goes through and lists all the albums in the database so user knows what albums they can add to 
                var bandsWithAlbums = context.Albums.Include(album => album.Artist);
                if(context.Albums.Count() > 0)
                {
                  Console.WriteLine("\nWe have the following albums on file: \n");
                  foreach(var album in bandsWithAlbums)
                  {
                    Console.WriteLine($"{album.Title} \n   made by {album.Artist.Name}");
                  }

                  //Asks user which album they want to add to
                  response = PromptForString("\nWhich album would you like to add to? ", true);
                  var foundAlbum = context.Albums.FirstOrDefault(album => album.Title.ToUpper() == response);

                  //Only starts to add songs if code finds what the user inputted
                  if(foundAlbum != null)
                  {
                    var keepAdding = true;
                    while(keepAdding == true)
                    {
                      //While loop that continues the user to keep adding as many songs to ONE album at a time
                      var newSong = AddSong(foundAlbum, context);
                      context.Songs.Add(newSong);
                      context.SaveChanges();

                      response = PromptForString("\nWould you like to add another song? (Y/N) ", true);
                      if(response == "Y")
                      {
                        Console.WriteLine("Lets keep adding then!");
                      }
                      else if(response == "N")
                      {
                        var songCount = context.Songs.Where(song => song.AlbumId == foundAlbum.Id);
                        Console.WriteLine($"Okay! Lets stop adding songs. {foundAlbum.Title} has {songCount.Count()} amount of songs added.");
                        keepAdding = false;
                      }
                      else
                      {
                        var songCount = context.Songs.Where(song => song.AlbumId == foundAlbum.Id);
                        Console.WriteLine($"\nI am not sure what you are saying. I will assume you mean 'No'. {foundAlbum.Title} has {songCount.Count()} amount of songs added.");
                        keepAdding = false;
                      }
                    }
                  }
                  //Otherwise does not start adding process
                  else
                  {
                    Console.WriteLine($"{response} could not be found, please try again.");
                  }
                }
                //Otherwise code will not bother adding songs to the database
                else
                {
                  Console.WriteLine("Oops, looks like there are no albums in this database!");
                }
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
            response = PromptForString("\nWould you like to \n(L)et a Band Go \n(R)e-sign a Band? ", true);

            switch(response)
            {
              //Unsign a band
              case "L":
                //Makes a list of all of the signed bands
                var signedBands = context.Artists.Where(band => band.IsSigned == true);

                //If there is 1 or more, it will print out each band 
                if(signedBands.Count() > 0)
                {
                  Console.WriteLine("\nHere is a list of our signed bands:");
                  foreach (var band in context.Artists)
                  {
                    if(band.IsSigned == true)
                    {
                      Console.WriteLine($"\n{band.Name}");
                    }
                  }
                  response = PromptForString("\nWhich band would you like to unsign? ", true);

                  //Locates if what the user inputted is a band in the system
                  var foundArtist = context.Artists.FirstOrDefault(band => band.Name.ToUpper() == response);
                  //First checks if the band exists
                  if(foundArtist == null)
                  {
                    Console.WriteLine("\nSorry, no such band exists. Please try again.");
                  }
                  //Then it double-checks if the band entered is NOT signed
                  else if(foundArtist.IsSigned == false)
                  {
                    Console.WriteLine($"\nUh oh, looks like {foundArtist.Name} is already let go. Please try again.");
                  }
                  //Otherwise if the band is not let go, will prompt user to double check if they want to let them go
                  else
                  {
                    response = PromptForString($"\nAre you sure you want to un-sign {foundArtist.Name}? (Y/N) ", true);

                    if(response == "Y")
                    {
                      foundArtist.IsSigned = false;
                      context.SaveChanges();
                      Console.WriteLine($"\n{foundArtist.Name} is now un-signed, hopefully they make more music in the future.");
                    }
                    else if (response == "N")
                    {
                      Console.WriteLine($"\nI will leave {foundArtist.Name} alone.");
                    }
                    else
                    {
                      Console.WriteLine($"\nI am not sure what you are saying. I will leave {foundArtist.Name} alone.");
                    }
                  }
                }
                //Otherwise prints no bands and does not give a chance for you to unsign them
                else
                {
                  Console.WriteLine("\nThere are no bands to let go.");  
                }
                break;
              
              //Re-sign band
              case "R":
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
                  response = PromptForString("\nWhich band would you like to sign back up? ", true);

                  //Locates if what the user inputted is a band in the system
                  var foundArtist = context.Artists.FirstOrDefault(band => band.Name.ToUpper() == response);
                  //First checks if the band exists
                  if(foundArtist == null)
                  {
                    Console.WriteLine("\nSorry, no such band exists. Please try again.");
                  }
                  //Then it double-checks if the band entered is NOT signed
                  else if(foundArtist.IsSigned == true)
                  {
                    Console.WriteLine($"\nUh oh, looks like {foundArtist.Name} is still signed and making music. Please try again.");
                  }
                  //Otherwise if the band is not signed, will prompt user to double check if they want to sign them back up
                  else
                  {
                    response = PromptForString($"\nAre you sure you want to re-sign {foundArtist.Name}? (Y/N) ", true);

                    if(response == "Y")
                    {
                      //Checks if the band being signed has their Contact Information stored
                      if (foundArtist.ContactName == null)
                      {
                        foundArtist.ContactName = PromptForString($"\nUh oh! Looks like {foundArtist.Name} is missing their Contact's name, could you please tell me what it is? Please remember punctuation matters :) ", false);
                      }
                      if(foundArtist.ContactPhoneNumber == null)
                      {
                        foundArtist.ContactPhoneNumber = PromptForString($"\nUh oh! Looks like {foundArtist.Name} is missing their contact phone number. Can you please enter their number using entering xxx-xxx-xxxx: ", false);
                      }

                      //Otherwise signs the band back up
                      foundArtist.IsSigned = true;
                      context.SaveChanges();
                      Console.WriteLine($"\n{foundArtist.Name} is now re-signed, I can't wait to hear their new music!");
                    }
                    else if (response == "N")
                    {
                      Console.WriteLine($"\nI will leave {foundArtist.Name} alone.");
                    }
                    else
                    {
                      Console.WriteLine($"\nI am not sure what you are saying. I will leave {foundArtist.Name} alone.");
                    }
                  }
                }
                //Otherwise prints no bands
                else
                {
                  Console.WriteLine("\nThere are no bands to sign-up.");  
                }
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
