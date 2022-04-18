using System.Globalization;

namespace Universe2D
{
    class Universe
    {
        public List<Body> Bodies {get; set;} = new List<Body>();
        public List<Body> NewBodies { get; set; } = new List<Body>();
        public const double G = 0.00000000006674184;

        public Universe(string[] bodies, int iteration, float time)
        {
            foreach(var body in bodies)
            {
                string[] bodyStr = body.Split(";");
                Body x = new Body(bodyStr);
                Bodies.Add(x);
            }

            NewBodies = Bodies;

            for (int i = 0; i < iteration; i++)
            {
                this.Movement(time);
            }
        }

        private void Movement(float time)
        {
            double force;
            double distance;
            double aceleration;

            for (int i = 0; i < Bodies.Count; i++)
            {
                force = 0;

                for (int j = 0; j < Bodies.Count; j++)
                {
                    if (j == i)
                        j += 1;

                    if (j >= (Bodies.Count))
                        continue;

                    distance = Distance(Bodies[i].Position[0], Bodies[j].Position[0], Bodies[i].Position[1], Bodies[j].Position[1]);
                    force += (G * Bodies[i].Mass * Bodies[j].Mass / Math.Pow(distance, 2));
                }

                aceleration = Bodies[i].Mass/force;

                // position //
                NewBodies[i].Position[0] = Bodies[i].Position[0] + Bodies[i].Velocity[0] * time + (aceleration/2) * Math.Pow(time, 2);
                NewBodies[i].Position[1] = Bodies[i].Position[1] + Bodies[i].Velocity[1] * time + (aceleration/2) * Math.Pow(time, 2);

                // velocity //
                NewBodies[i].Velocity[0] = Bodies[i].Velocity[0] + aceleration * time;
                NewBodies[i].Velocity[1] = Bodies[i].Velocity[1] + aceleration * time;
            }
        }

        private double Distance(double x1, double x2, double y1, double y2)
        {
            double x = x1 - x2;
            double y = y1 - y2;
            x = Math.Pow(x, 2);
            y = Math.Pow(y, 2);
            return Math.Sqrt(x + y);
        }

        public void WriteBodies()
        {
            foreach(var body in NewBodies)
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                /*
                string[] str = new String[1];
                str.Append(Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider));
                
                string str = Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider) + Environment.NewLine;
                File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse.data", str);
                */

                string str = body.Name + ";" + body.Mass + ";" + body.Radius + ";" + body.Position[0] + ";" + body.Position[1] + ";" + body.Velocity[0] + ";" + body.Velocity[1] + Environment.NewLine;
                File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse.data", str);
            }
        }
    }
}