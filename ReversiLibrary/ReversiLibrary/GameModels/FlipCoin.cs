using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiLibrary.GameModels
{
    public class Coin
    {
        public enum Face { Head, Tails };

        public static Face Flip(double biasFactor)
        {
         
            Random random = new Random();
            if (random.NextDouble() <= biasFactor)
            {
                return Face.Head;
            }
            else
            {
                return Face.Tails;
            }
            
        }
    }
}
