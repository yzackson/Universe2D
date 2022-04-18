namespace Universe2D
{
    class Universe
    {
        public List<Body> Bodies {get; set;} = new List<Body>();

        public float G {get;} = 0;

        public Universe(string[] bodies)
        {
            foreach(var body in bodies)
            {
                string[] bodyStr = body.Split(";");
                Body x = new Body(bodyStr);
                Bodies.Add(x);
            }
        }

        public void Force()
        {
            double force;
            double distance;
            double aceleration;

            for (int i = 0; i < Bodies.Count; i++)
            {
                force = 0;

                for (int j = 0; j < Bodies.Count; j++)
                {
                    if(i == j)
                    {
                        force = 1;
                        break;
                    }
                    distance = Distance(Bodies[i].Position[0], Bodies[j].Position[0], Bodies[i].Position[1], Bodies[j].Position[1]);
                    force += (G * (Bodies[i].Mass / Bodies[j].Mass) / Math.Pow(distance, 2));
                }
                aceleration = Aceleration(Bodies[i].Mass, force);

                //velocity

                //newPos
            }
        }

        private double Distance(double x1, double x2, double y1, double y2)
        {
            double x = x1 - x2;
            double y = y1 - y2;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private double Aceleration(double mass, double force)
        {
            if (force == 0)
            {
                return 0;
            }
            return mass / force;
        }
    }
}