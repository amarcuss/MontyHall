using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    internal class Game
    {
        // The number of Doors in the game
        public int NumberOfDoors { get; set; }

        // A list of the doors in the game
        public List<Door> Doors { get; set; }

        // Was the first door picked a prize door?
        public bool FirstPickedDoorHasPrize { get; set; }

        // Did the player switch doors?
        public bool PlayerSwitchedDoors { get; set; }

        // Default constructor
        public Game(int numberOfDoors)
        {
            // Store the number of doors
            NumberOfDoors = numberOfDoors;

            // Set the default values for the game
            FirstPickedDoorHasPrize = false;

            // Set the default values for the game
            PlayerSwitchedDoors = false;

            // Create a list of doors
            Doors = new List<Door>();

            // Pick the door to be selected
            int prizeDoor = new Random().Next(0, NumberOfDoors);

            // Create a new door for each door in the game
            for (int i = 0; i < NumberOfDoors; i++)
            {
                if (i == prizeDoor)
                    Doors.Add(new Door(i, true));
                else
                    Doors.Add(new Door(i, false));
            }
        }

        // Pick the first door
        public void PickFirstDoor()
        {
            // Generate a random number between 0 and the number of doors
            int selectedDoor = new Random().Next(0, NumberOfDoors);

            // Mark the selected door as selected
            Doors[selectedDoor].IsSelected = true;

            // Set whether or not the the first picked door has the prize
            FirstPickedDoorHasPrize = Doors[selectedDoor].IsPrize;
        }

        // Check if the door picked has the prize or not
        public bool DoorPickedHasPrize()
        {
            // Get the door that is selected
            Door selectedDoor = Doors.Where(d => d.IsSelected == true).First();

            // Return true if the selected door contains the prize
            return selectedDoor.IsPrize;
        }

        // Open a door that is not selected and does not contain the prize
        public void OpenDoorFirstDoor()
        {
            // Create a list of doors that are not selected and do not contain the prize
            List<Door> doorsToOpen = Doors.Where(d => d.IsSelected == false && d.IsPrize == false).ToList();

            // Pick a random door from the list of doors to open
            int doorToOpen = new Random().Next(0, doorsToOpen.Count);
            
            // Open the door
            doorsToOpen[doorToOpen].IsOpen = true;
        }

        // Write state of the game to the console
        public void WriteState()
        {
            // Write out an header before writing out the state of the game
            Console.WriteLine("State of the game:");

            // Write the state of each door to the console
            foreach (Door door in Doors)
            {
                Console.WriteLine($"Door {door.Number} is {(door.IsOpen ? "open" : "closed")} and {(door.IsSelected ? "selected" : "not selected")} and {(door.IsPrize ? "contains the prize" : "does not contain the prize")}");
            }
        }

        // Switch the selected door
        public void SwitchDoor()
        {
            // Find the door that is selected and not open
            Door selectedDoor = Doors.Where(d => d.IsSelected == true && d.IsOpen == false).First();

            // Find the door that is not selected and not open
            Door otherDoor = Doors.Where(d => d.IsSelected == false && d.IsOpen == false).First();

            // Switch the selected door to the other door
            selectedDoor.IsSelected = false;
            otherDoor.IsSelected = true;

            // Set whether or not the player switched doors
            PlayerSwitchedDoors = true;
        }

        // Determine if the player won the game
        public bool DidPlayerWin()
        {
            // Find the door that is selected and not open
            Door selectedDoor = Doors.Where(d => d.IsSelected == true && d.IsOpen == false).First();

            // Return true if the selected door contains the prize
            return selectedDoor.IsPrize;
        }
    }
}
