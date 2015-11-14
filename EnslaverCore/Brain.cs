using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EnslaverCore
{
    public class HeadInformation
    {
        public string Name;
        public Rectangle Head;
        public Rectangle Eye1;
        public Rectangle Eye2;
        public bool IsSmile;
    }

    public static class Brain
    {
        public static List<HeadInformation> GetInformation()
        {
            return new List<HeadInformation>();
        }
    }
}
