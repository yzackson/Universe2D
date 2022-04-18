using System;

namespace Universe2D
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\universe.data");

            string[] systemData = file[0].Split(";");
            
            file = file.Skip(1).ToArray();

            Universe universe = new Universe(file);
            Console.ReadLine();

            Console.WriteLine(universe.Bodies.Count);
            //universe.printBody();
            Console.ReadLine();            
        }
    }
}