using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG_TSP.AGClass
{
    public class Population
    {
        public Individuo[] population;           //Grupo de individuos(População)

        //Construtor 
        public Population()
        {
            this.population = new Individuo[ConfigurationGA.tamPopulacao]; //Seta o tamanho da população para a configurada no algoritmo

            try
            {
                for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
                {
                    this.population[i] = new Individuo();    //Cria um individuo na posição "i" do vetor
                    this.population[i].indexDoVetor = i;      //Salva a posição onde o individuo foi adicionado ( também podemos chamar de INDEX do individuo )
                }

                //Avaliar
                CalculateFitness();    //Calcula o Fitness da população
            }
            catch (Exception e)
            {
                MessageBox.Show("Ops você tentou importar um arquivo vazio!");
            }
        }

        //Calcular o fitness todos os individuos
        public void CalculateFitness()
        {
            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                population[i].CalcFitness();
            }
        }

        //Avaliar toda a populacao
        public void Avaliacao()
        {
            AtualizarIndexInd();
            CalculateFitness();
        }

        public void AtualizarIndexInd()
        {

            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                population[i].indexDoVetor = i;
            }

        }

        //Retornar um vetor de individuos
        public Individuo[] GetPopulation()
        {
            return this.population;
        }

        //Altera o individuo
        public void SetIndividuos(int position, Individuo Individuo)
        {
            this.population[position] = Individuo;
        }

        //Soma o fitness da população e dividi pela quantidade de individuos
        public double GetMediaPop()
        {
            double sum = 0;

            foreach (Individuo ind in this.population)
            {
                sum += ind.GetFitness();
            }

            return (sum / ConfigurationGA.tamPopulacao);


        }

        //Metodo para ordenar a populacao do melhor para o pior
        public void OrderPopulation()
        {
            Individuo aux;

            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                for (int j = 0; j < ConfigurationGA.tamPopulacao; j++)
                {
                    if (population[i].GetFitness() < population[j].GetFitness())
                    {
                        aux = population[i];
                        population[i] = population[j];
                        population[j] = aux;
                    }
                }
            }
        }

        //Retornar melhor individuo
        public Individuo GetBest()
        {
            OrderPopulation();
            return population[0];
        }

        //Retornar o pior ind
        public Individuo GetBad()
        {
            OrderPopulation();
            return population[ConfigurationGA.tamPopulacao - 1];
        }

        public override string ToString()
        {
            base.ToString();

            string result = string.Empty;

            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                result += population[i].ToString() + "\n";
            }

            return result;
        }
    }
}
