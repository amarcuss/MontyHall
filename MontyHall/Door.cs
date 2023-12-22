using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    internal class Door
    {
        // Does this door contain the price?
        public bool IsPrize { get; set; }

        // Is this door open?
        public bool IsOpen { get; set; }

        // Is this door selected by the player?
        public bool IsSelected { get; set; }

        // The number of the door
        public int Number { get; set; }
        
        // Default constructor
        public Door() { }

        // Constructor with parameters
        public Door(int number, bool isPrize)
        {
            Number = number;
            IsPrize = isPrize;
            IsOpen = false;
            IsSelected = false;
        }

        
    }
}
