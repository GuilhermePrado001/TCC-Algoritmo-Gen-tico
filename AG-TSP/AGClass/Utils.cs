using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AG_TSP.AGClass
{
    public static class Utils
    {
        //Gera numeros aleatorio para iniciar o vetor de genes, formando um cromossomo.
        public static List<int> RandomNumbers(int start, int end)
        {
            List<int> numbers = new List<int>();

            for (int i = start; i < end; i++)
            {
                numbers.Add(i);
            }

            //Embaralhar a lista
            for (int i = 0; i < numbers.Count; i++)
            {
                int a = ConfigurationGA.random.Next(numbers.Count);
                int temp = numbers[i];
                numbers[i] = numbers[a];
                numbers[a] = temp;
            }
            return numbers.GetRange(0, end);
        }

        public static void TwoOpt(Individuo bestInd)
        {

            Individuo newInd = new Individuo();

            //Adiciono todos os genes no melhor individuo ao novo individuo ficando assim uma copia identica um do outro
            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                newInd.SetGene(i, bestInd.GetGene(i));
            }

            //Calcula o fitness do novo ind
            newInd.CalcFitness();

            int improve = 0;

            //Enquanto houver melhoria no TwoOpt ele continuara executando.
            while (improve == 0)
            {
                improve++;
                //Adicionamos a variavel "bestTour" o fitness do melhor individuo, esta sera usada para comparação
                double bestTour = bestInd.GetFitness();

                //Looping para fixar a casa de teste do TwoOpt
                for (int i = 1; i < ConfigurationGA.tamCromossomo - 1; i++)
                {
                    for (int k = i + 1; k < ConfigurationGA.tamCromossomo; k++)
                    {
                        //Executa a função TwoOpt passando o melhor individuo e sua copia além do i e k que são os valores do looping
                        TwoOptSwap(newInd, bestInd, i, k);

                        //Recalcula os fitness para fazer a verificação se melhorou ou nao
                        newInd.CalcFitness();
                        bestInd.CalcFitness();

                        //atribui o fitness do novo ind a variavel new distance
                        double new_distance = newInd.GetFitness();

                        if (new_distance < bestTour)
                        {
                            //Se nova distancia for melhor do que a anterior ele atualiza a variavel bestTour 
                            bestTour = new_distance;
                            improve = 0;

                            //Atribui os genes do novo ind ao antigo individuo
                            for (int j = 0; j < ConfigurationGA.tamCromossomo; j++)
                            {
                                bestInd.SetGene(j, newInd.GetGene(j));
                            }
                        }
                    }
                }
            }
        }

        public static void TwoOptSwap(Individuo newInd, Individuo ind, int i, int k)
        {
            //Responsavel por fixar as casas iniciais
            for (int c = 0; c <= i - 1; ++c)
            {
                newInd.SetGene(c, ind.GetGene(c));
            }

            //Realiza a troca dos genes do novo individuo usando o melhor como parametro.
            int dec = 1;
            for (int c = i; c <= k - 1; ++c)
            {
                newInd.SetGene(c, ind.GetGene(k - dec));
                dec++;
            }

            //Termina de preencher o vetor com os valores originais
            for (int c = k; c < ConfigurationGA.tamCromossomo; ++c)
            {
                newInd.SetGene(c, ind.GetGene(c));
            }
        }
    }
}
