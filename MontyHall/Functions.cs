using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    internal static class Functions
    {
        public static void RunRandomGames(int numberOfGames)
        {
                   
            // The number of games won
            int numberOfWins = 0;

            // The number of times the player switched doors
            int numberOfSwitches = 0;

            // The number of wins when the player switched doors
            int numberOfWinsWhenPlayerSwitchedDoors = 0;

            // The number of wins when the player didn't switch doors
            int numberOfWinsWhenPlayerNoSwitchedDoors = 0;;

            // Multi-threading lock object so we can output information in a thread-safe manner
            object outputLock = new object();

            lock (outputLock)
            {
                Console.WriteLine($"Running {numberOfGames.ToString("N0")}.\n");
            }

            for (int i = 0; i < numberOfGames; i++)
            {
                // Create a new Game object
                Game game = new Game(3);

                // Pick the first door
                game.PickFirstDoor();

                // Open the non-prize door
                game.OpenDoorFirstDoor();

                // Randomly decide whether or not to switch doors
                if (new Random().Next(0, 2) == 0)
                {
                    game.SwitchDoor();
                    numberOfSwitches++;
                }

                if (game.DidPlayerWin())
                {
                    numberOfWins++;

                    if (game.PlayerSwitchedDoors)
                        numberOfWinsWhenPlayerSwitchedDoors++;
                    else
                        numberOfWinsWhenPlayerNoSwitchedDoors++;
                }
            }

            float winPercentage = ((float)numberOfWins / (float)numberOfGames) * 100;
            float doorSwitchPercentage = ((float)numberOfSwitches / (float)numberOfGames) * 100;

            float winPercentageOnSwitch = ((float)numberOfWinsWhenPlayerSwitchedDoors / (float)numberOfWins) * 100;
            float winPercentageNoSwitch = ((float)numberOfWinsWhenPlayerNoSwitchedDoors / (float)numberOfWins) * 100;

            lock (outputLock)
            {
                Console.WriteLine($"We played {numberOfGames.ToString("N0")} games.\n");
                
                //Console.WriteLine($"The player won {numberOfWins.ToString("N0")} games, {winPercentage.ToString("n2")}% of the time.\n");

                Console.WriteLine($"The player switched doors in {numberOfSwitches.ToString("N0")} games, {doorSwitchPercentage.ToString("n2")}% of the time.\n");

                Console.WriteLine($"The player won {winPercentageOnSwitch.ToString("n2")}% when switching and {winPercentageNoSwitch.ToString("n2")}% when they didn't switch.\n");
            }
        }

        // Create a function to run the games
        public static void RunGames(int numberOfGames, bool switchDoor)
        {
            int numberOfWins = 0;
            int numberOfNonPrizeDoorsPickedFirst = 0;
            int numberOfLosses = 0;
            bool firstPickedDoorHasPrize = false;
            int numberOfWinsWhenFirstDoorWasNonPrize = 0; 
            int numberOfWinsWhenFirstDoorWasPrize = 0;
            int numberOfLossesWhenFirstDoorWasNonPrize = 0;
            int numberOfLossesWhenFirstDoorWasPrize = 0;

            object outputLock = new object();

            Console.WriteLine($"Running {numberOfGames.ToString("N0")} games with switchDoor set to {switchDoor}.");

            for (int i = 0; i < numberOfGames; i++)
            {
                Game game = new Game(3);
                game.PickFirstDoor();

                // Check if the door picked has the prize or not
                firstPickedDoorHasPrize = game.DoorPickedHasPrize();

                if (firstPickedDoorHasPrize == false)
                    numberOfNonPrizeDoorsPickedFirst++;

                //Console.WriteLine($"\n...Game #{i + 1}...");

                //Console.WriteLine("\nGame state before opening first door");
                //game.WriteState();
                
                game.OpenDoorFirstDoor();

                //Console.WriteLine("\nGame state after opening first door");
                //game.WriteState();

                if (switchDoor)
                    game.SwitchDoor();

                //Console.WriteLine("\nGame state after switching if needed");
                //game.WriteState();

                if (game.DidPlayerWin())
                {
                    numberOfWins++;
                    if (firstPickedDoorHasPrize)
                        numberOfWinsWhenFirstDoorWasPrize++;
                    else
                        numberOfWinsWhenFirstDoorWasNonPrize++;
                }
                else
                { 
                    numberOfLosses++;
                    if (firstPickedDoorHasPrize)
                        numberOfLossesWhenFirstDoorWasPrize++;
                    else
                        numberOfLossesWhenFirstDoorWasNonPrize++;
                }
            }

            lock (outputLock)
            {
                Console.WriteLine($"Number of wins: {numberOfWins.ToString("N0")}");
                Console.WriteLine($"  Number of wins with Non-prize picked first: {numberOfWinsWhenFirstDoorWasNonPrize.ToString("N0")}");
                Console.WriteLine($"  Number of wins with Prize picked first:     {numberOfWinsWhenFirstDoorWasPrize.ToString("N0")}");

                Console.WriteLine($"Number of losses: {numberOfLosses.ToString("N0")}");
                Console.WriteLine($"  Number of losses with Non-prize picked first: {numberOfLossesWhenFirstDoorWasNonPrize.ToString("N0")}");
                Console.WriteLine($"  Number of losses with Prize picked first:     {numberOfLossesWhenFirstDoorWasPrize.ToString("N0")}");

                float winPercentage = ((float)numberOfWins / (float)numberOfGames) * 100;
                Console.WriteLine($"Win percentage: {winPercentage.ToString("n2")}%");
                Console.WriteLine();

                // Open a file to write the results to
                string fileName = $"MontyHallResults.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true);

                // Write out the results to the file with the current date and time at the start of the line
                file.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},{numberOfGames},{switchDoor},{numberOfWins},{numberOfLosses},{winPercentage.ToString("n2")}");

                // Close the file
                file.Close();
            }
        }

    }
}
