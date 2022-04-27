using System.Globalization;

namespace Universe2D
{
    class Universe
    {
        // Lista para armazenar os corpos importados do arquivo
        public List<Body> Bodies {get; set;} = new List<Body>();

        //Lista para receber os novos corpos após os cálculos
        public List<Body> NewBodies { get; set; } = new List<Body>();

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
            bool remove = false;

            NewBodies = Bodies;

            // Percorre cada corpo lido do arquivo
            for (int i = 0; i < Bodies.Count; i++)
            {
                // zera as forcas para calcular cada corpo
                force = 0;
                totForce = 0;

                // Percorre cada corpo lido lido do arquivo para calcular com o corpo selecionado anteriormente
                for (int j = 0; j < Bodies.Count; j++)
                {
                    // Se o corpo j for o mesmo corpo i pula para o próximo corpo j
                    if (j == i) {
                        j++; 
                        if (j == (Bodies.Count)) { continue; }
                    }
                    // distancia do corpo i em relacao ao corpo j
                    distance = Distance(Bodies[i].Position[0], Bodies[j].Position[0], Bodies[i].Position[1], Bodies[j].Position[1]);

                    // forca do corpo i em relacao ao corpo j
                    force = G * (Bodies[i].Mass * Bodies[j].Mass / Math.Pow(distance, 2));

                    // soma da forca entre i e todos os demais corpos
                    totForce += force;

                    // Checagem de colisao
                    if (distance < (NewBodies[i].Radius + NewBodies[j].Radius))
                    {
                        if (NewBodies[i].Mass > NewBodies[j].Mass)
                        {
                            NewBodies[i].Mass += NewBodies[j].Mass;
                            NewBodies.Remove(NewBodies[j]);
                            i = i == Bodies.Count ? i - 1 : i;
                        }
                        else
                        {
                            NewBodies[j].Mass += NewBodies[i].Mass;
                            remove = true;
                        }

                    }
                }
                
                // Calcula aceleração do corpo
                aceleration = totForce/Bodies[i].Mass;

                // Calcuula nova posição
                NewBodies[i].Position[0] = Bodies[i].Position[0] + Bodies[i].Velocity[0] * time + ((aceleration/2) * Math.Pow(time, 2));
                NewBodies[i].Position[1] = Bodies[i].Position[1] + Bodies[i].Velocity[1] * time + ((aceleration/2) * Math.Pow(time, 2));

                // Limita a posicao do corpo para o limite da tela de teste
                if (NewBodies[i].Position[0] >= 1382) { NewBodies[i].Position[0] = 1382; }
                if (NewBodies[i].Position[1] >= 784) { NewBodies[i].Position[1] = 784; }

                // Calcula nova velocidade
                NewBodies[i].Velocity[0] = (aceleration * time) + Bodies[i].Velocity[0];
                NewBodies[i].Velocity[1] = (aceleration * time) + Bodies[i].Velocity[1];

                if (remove)
                {
                    NewBodies.Remove(NewBodies[i]);
                    remove = false;
                }
            }

            // copia a lista de corpos para manter os valores de velocidade e posicao lidos do arquivo da primeira iteração
            Bodies = NewBodies;
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
            foreach(var body in NewBodies)
            {
                /*
                string[] str = new String[1];
                str.Append(Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider));
                
                string str = Convert.ToString(body.Name, provider) + ";" + Convert.ToString(body.Mass, provider) + ";" + Convert.ToString(body.Radius, provider) + ";" + Convert.ToString(body.Position[0], provider) + ";" + Convert.ToString(body.Position[1], provider) + ";" + Convert.ToString(body.Velocity[0], provider) + ";" + Convert.ToString(body.Velocity[1], provider) + Environment.NewLine;
                File.AppendAllText("C:\\Dev\\Faculdade\\Universe2D\\NewUniverse.data", str);
                */

                // monta a linha do corpo e o escreve no arquivo de sa�da
                string str = body.Name + ";" + body.Mass + ";" + body.Radius + ";" + body.Position[0] + ";" + body.Position[1] + ";" + body.Velocity[0] + ";" + body.Velocity[1] + Environment.NewLine;
                File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", str);
            }
        }

        public void GenerateNewUniverse()
        {
            File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", $"{Bodies.Count};{Iteration};{Time}" + Environment.NewLine);

            for (int i = 0; i < Iteration; i++)
            {

                this.Movement(Time); // executa os cálculos de movimentação dos corpos

                if (i % 10 == 0)
                {
                    File.AppendAllText("C:\\Users\\isaac\\Documents\\Faculdade\\AED2\\Universe2D\\NewUniverse.uni", $"** Interação {i} ************" + Environment.NewLine);
                    this.WriteBodies(); // escreve o resultado dos corpos no arquivo
                }
            }
        }
    }
}