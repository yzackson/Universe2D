using System.Globalization;

namespace Universe2D
{
    public class Body
    {
        public string Name {get; set;}
        public double Mass {get; set;}
        public double Radius {get; set;}
        public double[] Position { get; set; } = new double[2];
        public double[] Velocity {get; set;} = new double[2];

        public Body(string[] body)
        {

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.Name = body[0];
            this.Mass = Convert.ToDouble(body[1], provider);
            this.Radius = Convert.ToDouble(body[2], provider);
            this.Position [0] = Convert.ToDouble(body[3], provider);
            this.Position [1] = Convert.ToDouble(body[4], provider);
            this.Velocity [0] = Convert.ToDouble(body[5], provider);
            this.Velocity [1] = Convert.ToDouble(body[6], provider);
        }
    }
}