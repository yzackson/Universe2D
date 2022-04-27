using System.Globalization;

namespace Universe2D
{
    class Universe
    {
        // Lista para armazenar os corpos importados do arquivo
        public List<Body> Bodies {get; set;} = new List<Body>();

        //Lista para receber os novos corpos após os cálculos
        //public List<Body> NewBodies { get; set; } = new List<Body>();

        // Constante gravitacional
        public const double G = 0.00000000006673;

        public int Iteration;

        public float Time;

        // Construtor do novo universo
        public Universe(string[] bodies, int iteration, float time)
        {
            // Pega cada linha importada do arquivo e cria um novo corpo adicionando a lista de corpos
            foreach(var body in bodies)
            {
                string[] bodyStr = body.Split(";");
                Body x = new Body(bodyStr);
                Bodies.Add(x);
            }

            Iteration = iteration;
            Time = time;
        }

        // FUNCAO PARA CALCULAR A MOVIMENTACAO DOS CORPOS
        private void Movement(float time)
        {
            double force; // forca 
            double totForce;
            double distance = 0;
            double aceleration;


            Bodies.ForEach(bodyA =>
            {
                force = 0;
                totForce = 0;

                Bodies.ForEach(bodyB =>
                {
                    if(bodyA == bodyB)
                    {
                        return;
                    }

                    // distancia do corpo i em relacao ao corpo j
                    distance = Distance(bodyA.Position[0], bodyB.Position[0], bodyA.Position[1], bodyB.Position[1]);

                    // forca do corpo i em relacao ao corpo j
                    force = G * (bodyA.Mass * bodyB.Mass / Math.Pow(distance, 2));

                    // soma da forca entre i e todos os demais corpos
                    totForce += force;

                    // Calcula aceleração do corpo
                    aceleration = totForce / bodyA.Mass;

                    // Calcuula nova posição
                    bodyA.Position[0] = bodyA.Position[0] + bodyA.Velocity[0] * time + ((aceleration / 2) * Math.Pow(time, 2));
                    bodyA.Position[1] = bodyA.Position[1] + bodyA.Velocity[1] * time + ((aceleration / 2) * Math.Pow(time, 2));

                    // Limita a posicao do corpo para o limite da tela de teste
                    if (bodyA.Position[0] >= 1382) { bodyA.Position[0] = 1382; }
                    if (bodyA.Position[1] >= 784) { bodyA.Position[1] = 784; }

                    // Calcula nova velocidade
                    bodyA.Velocity[0] = (aceleration * time) + bodyA.Velocity[0];
                    bodyA.Velocity[1] = (aceleration * time) + bodyA.Velocity[1];

                    // Checagem de colisao
                    if (distance < (bodyA.Radius + bodyB.Radius))
                    {
                        if (bodyA.Mass > bodyB.Mass)
                        {
                            bodyA.Mass += bodyB.Mass;
                            Bodies.Remove(bodyB);
                        }
                        else
                        {
                            bodyB.Mass += bodyA.Mass;
                            Bodies.Remove(bodyA);
                        }

                    }
                });
            });
        }

        // calcula a distancia
        private double Distance(double x1, double x2, double y1, double y2)
        {
            //double x = x2 - x1;
            //double y = y2 - y1;
            double x = Math.Pow(x2 - x1, 2);
            double y = Math.Pow(y2 - y1, 2);
            return Math.Sqrt(x + y);
        }

        // Escreve a lista de corpos no arquivo de saida
        private void WriteBodies()
        {
            Bodies.ForEach(body =>
            {
                // monta a linha do corpo e o escreve no arquivo de sa�da
                string str = body.Name + ";" + body.Mass + ";" + body.Radius + ";" + body.Position[0] + ";" + body.Position[1] + ";" + body.Velocity[0] + ";" + body.Velocity[1] + Environment.NewLine;
                File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse_teste.uni", str);
            });
        }

        public void GenerateNewUniverse()
        {
            File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse_teste.uni", $"{Bodies.Count};{Iteration};{Time}" + Environment.NewLine);

            for (int i = 0; i < Iteration; i++)
            {

                this.Movement(Time); // executa os cálculos de movimentação dos corpos

                if (i % 10 == 0)
                {
                    File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse_teste.uni", $"** Interação {i} ************" + Environment.NewLine);
                    this.WriteBodies(); // escreve o resultado dos corpos no arquivo
                }
            }
        }
    }
}