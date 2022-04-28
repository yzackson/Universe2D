namespace Universe2D
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Caminho do arquivo de leitura: ");
            string path = Console.ReadLine();
            string[] file = File.ReadAllLines(path + "\\BodiesList.uni");

            string[] systemData = file[0].Split(";");

            file = file.Skip(1).ToArray();

            path += "\\NewUniverse.uni";
            Universe universe = new Universe(file, int.Parse(systemData[1]), float.Parse(systemData[2]), path);

            universe.GenerateNewUniverse();
        }
    }
}