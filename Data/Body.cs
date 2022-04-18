namespace Universe2D
{
    public class Body
    {
        public string Name {get; set;}
        public float Mass {get; set;}
        public float Radius {get; set;}
        public float[] Position { get; set; } = new float[2];
        public float[] Velocity {get; set;} = new float[2];

        public Body(string[] body)
        {
            this.Name = body[0];
            this.Mass = float.Parse(body[1]);
            this.Radius = float.Parse(body[2]);
            this.Position [0] = float.Parse(body[3]);
            this.Position [1] = float.Parse(body[4]);
            this.Velocity [0] = float.Parse(body[5]);
            this.Velocity [1] = float.Parse(body[6]);
        }
    }
}