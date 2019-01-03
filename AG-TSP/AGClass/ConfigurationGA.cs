using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_TSP.AGClass
{
    public static class ConfigurationGA
    {
        public static int tamCromossomo = 0;               //Tamanho do cromossomo
        public static int tamPopulacao = 0;             //tamanho da população
        public static Random random = new Random((int)DateTime.Now.Ticks); // Gera um numero aleatorio
        public static bool elitismo = false;                 //Definir se ira usar elitismoo
        public static int tamElitismo = 0;                  //Quantidade de ind. para elitismoo
        public static double taxaCruzamento = 0;           //Taxa de crossover (cruzamento)
        public static double taxaMutacao = 0;           //Taxa de mutação
        public static int numCompetidor = 0;            //Numeros de competidores para torneio

        public static Mutation mutationType = Mutation.NovoInd;

        public enum Mutation //Modo de Mutação a ser escolhido
        {
            NovoInd,
            NaPopulacao            
        }
    }
}
