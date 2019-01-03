using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG_TSP.AGClass
{
    public class Individuo
    {
        private int[] cromossomo;           //Cromossomo de inteiros - Cada gene representa uma cidade
        private double fitness;             //Valor de aptidão do ind.
        public int indexDoVetor = 0;       //Posicao dos individuos

        //Contrutor
        public Individuo()
        {
            //Cromossomo, repsenta um vetor completo pode ser nomeado também de individuo
            this.cromossomo = new int[ConfigurationGA.tamCromossomo];

            //Cria uma lista de Genes random que preenche o vetor de cromossomos, genes são responsaveis pelas caracteristicas do individuo
            List<int> genes = Utils.RandomNumbers(0, ConfigurationGA.tamCromossomo);

            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                //Atribui os genes aos cromossomos
                this.cromossomo[i] = genes[i];
            }

            //Calculo do fitness
            CalcFitness();
        }

        //Metodo responsavel pela avaliação do individuo
        public void Avaliacao()
        {
            CalcFitness();
        }

        //Metodo que retorna um vetor de cromossomo
        public int[] GetCromosome()
        {
            return this.cromossomo;
        }

        //Calcular o fitness --- AQUI TAMBÉM ENCONTRA-SE A FUNÇÃO DE AVALIAÇÃO ( DENOMINADA FITNESS )
        public void CalcFitness()
        {

            double totalDist = 0.0;

            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                if (i < (ConfigurationGA.tamCromossomo - 1))
                {
                    //Envia o gene i e o gene i + 1 que será somado dentro da função GetDist, após isso retornamos o valor e somamos a totalDist
                    totalDist += TablePoints.getDist(GetGene(i), GetGene(i + 1));     
                }
                else
                {
                    //Conforme o problema do TSP após chegar ao ultimo ponto ele volta para origem
                    totalDist += TablePoints.getDist(GetGene(i), GetGene(0));       
                }
            }

            //Altera o fitness
            SetFitness(totalDist);
        }

        //Aqui podemos alterar o gene de um individuo, passando a possição onde desejamos alterar e o novo gene como parametro
        public void SetGene(int position, int gene)
        {
            this.cromossomo[position] = gene;
        }

        //Nos retorna um gene, expecificadamente o gene da posição passada como parametro
        public int GetGene(int position)
        {
            return this.cromossomo[position];
        }

        //Metodo para alterar o fitnees do individuo
        public void SetFitness(double fitness)
        {
            this.fitness = fitness;
        }

        //Metodo para pegar o fitness do individuo, também podemos chamar de "nota" do individuo
        public double GetFitness()
        {
            return this.fitness;
        }

        //Swap de valores para mutação
        public void Mutacao(int pointOne, int pointTwo)
        {
            //Realiza a inversão de posições do vetor, o que caracteriza uma mutação 
            if (pointOne < ConfigurationGA.tamCromossomo
               && pointTwo < ConfigurationGA.tamCromossomo
               && pointOne != pointTwo)
            {
                int temp = cromossomo[pointOne];
                cromossomo[pointOne] = cromossomo[pointTwo];
                cromossomo[pointTwo] = temp;
            }
        }

        public override string ToString()
        {
            base.ToString();

            string result = string.Empty;
            result += "Rota:    ";

            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                result += (GetGene(i) + 1).ToString() + " -> ";
            }

            return result;
        }

    }
}
