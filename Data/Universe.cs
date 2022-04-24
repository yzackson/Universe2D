using System.Globalization;

namespace Universe2D
{
    class Universe
    {
        public List<Body> Bodies {get; set;} = new List<Body>();
        public List<Body> NewBodies { get; set; } = new List<Body>();
        public const double G = 0.00000000006673;

        public Universe(string[] bodies, int iteration, float time)
        {
            // pega cada linha e cria um novo corpo (uma nova instancia de Corpo e adiciona na lista de corpos
            foreach(var body in bodies)
            {
                string[] bodyStr = body.Split(";");
                Body x = new Body(bodyStr);
                Bodies.Add(x);
            }

            // copia a lista de corpos para manter os valores lidos inicialmente e trabalhar somente com eles e não com os valores alterados
            NewBodies = Bodies;

            File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", $"{bodies.Length};{iteration}" + Environment.NewLine);

            for (int i = 0; i < iteration; i++)
            {
                this.Movement(time); // executa os cálculos de movimentação dos corpos

                if(i % 10 == 0)
                {
                    File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", $"** Interação {i} ************" + Environment.NewLine);
                    this.WriteBodies(); // escreve o resultado dos corpos no arquivo
                }
            }
        }

        private void Movement(float time)
        {
            double force;
            double totForce;
            double distance = 0;
            double aceleration;

            for (int i = 0; i < Bodies.Count; i++)
            {
                force = 0;
                totForce = 0;

                
                for (int j = 0; j < Bodies.Count; j++)
                {
                    // tratamento para o caso em que for calcular a posição e velocidade do corpo em relação a ele mesmo
                    if (j == i) {
                        j++; 
                        if (j == (Bodies.Count)) { continue; }
                    }

                    distance = Distance(Bodies[i].Position[0], Bodies[j].Position[0], Bodies[i].Position[1], Bodies[j].Position[1]);
                    force = G * (Bodies[i].Mass * Bodies[j].Mass / Math.Pow(distance, 2));
                    totForce += force;
                }

                aceleration = totForce/Bodies[i].Mass;

                // nova posição //
                NewBodies[i].Position[0] = Bodies[i].Position[0] + Bodies[i].Velocity[0] * time + ((aceleration/2) * Math.Pow(time, 2));
                NewBodies[i].Position[1] = Bodies[i].Position[1] + Bodies[i].Velocity[1] * time + ((aceleration/2) * Math.Pow(time, 2));

                // limita a posição //
                if (NewBodies[i].Position[0] >= 1382) { NewBodies[i].Position[0] = 1382; }
                if (NewBodies[i].Position[1] >= 784) { NewBodies[i].Position[1] = 784; }

                // nova velocidade //
                NewBodies[i].Velocity[0] = (aceleration * time) + Bodies[i].Velocity[0];
                NewBodies[i].Velocity[1] = (aceleration * time) + Bodies[i].Velocity[1];

                for (int j = 0; j < NewBodies.Count; j++)
                {
                    if (distance < (NewBodies[i].Radius + NewBodies[j].Radius))
                    {
                        if (NewBodies[i].Mass > NewBodies[j].Mass)
                        {
                            NewBodies[i].Mass += NewBodies[j].Mass;
                            NewBodies.Remove(NewBodies[j]);
                        }
                        else
                        {
                            NewBodies[j].Mass += NewBodies[i].Mass;
                            NewBodies.Remove(NewBodies[i]);
                        }

                    }
                }
            }
                
            Bodies = NewBodies;
        }

        private double Distance(double x1, double x2, double y1, double y2)
        {
            //double x = x2 - x1;
            //double y = y2 - y1;
            double x = Math.Pow(x2 - x1, 2);
            double y = Math.Pow(y2 - y1, 2);
            return Math.Sqrt(x + y);
        }

        public void WriteBodies()
        {
            foreach(var body in NewBodies)
            {
                /*
                string[] str = new String[1];
                str.Append(Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider));
                
                string str = Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider) + Environment.NewLine;
                File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse.data", str);
                */

                // monta a linha do corpo e o escreve no arquivo de saída
                string str = body.Name + ";" + body.Mass + ";" + body.Radius + ";" + body.Position[0] + ";" + body.Position[1] + ";" + body.Velocity[0] + ";" + body.Velocity[1] + Environment.NewLine;
                File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", str);
            }
        }
    }
}