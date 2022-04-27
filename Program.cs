namespace Universe2D
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("C:\\Dev\\Faculdade\\Universe2D\\inicalTrabalho.uni");

            string[] systemData = file[0].Split(";");

            file = file.Skip(1).ToArray();

            Universe universe = new Universe(file, int.Parse(systemData[1]), float.Parse(systemData[2]));

            universe.GenerateNewUniverse();
        }
    }
}