using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_TSP.AGClass
{
    public class AlgoritmoGenetico
    {
        private double taxaCrossover;               //Taxa de cruzamento
        private double taxaMutacao;                //Taxa de mutação

        public delegate Individuo[] Crossover(Individuo pai1, Individuo pai2); 
        public Crossover crossover;

        public delegate Individuo Selection(Population pop);
        public Selection selection;


        public AlgoritmoGenetico()
        {
            //Parametros iniciais (Construtor da classe)
            this.crossover = CrossoverPMX;  //Usando o Delegate
            this.selection = Torneio;     //Usando o Delegate

            //Setamos a taxa de crossover e mutação puxando da classe configuration
            this.taxaCrossover = ConfigurationGA.taxaCruzamento;   
            this.taxaMutacao = ConfigurationGA.taxaMutacao;       
        }

        //Execução do AG
        public Population ExecuteGA(Population pop)
        {
            #region Executa algoritmo genético
                     
            //Starta a nova pop e cria a lista de ind temporario
            Population newPopulation = new Population();
            List<Individuo> popTemp = new List<Individuo>();

            //Atribuir individuos a população temporaria
            for(int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                popTemp.Add(pop.GetPopulation()[i]);
            }
            
            //Inicia o elitismo com o tamanho informado em Configuration GA
            Individuo[] IndElit = new Individuo[ConfigurationGA.tamElitismo];

            //Caso optar por elistimo
            if(ConfigurationGA.elitismo)
            {
                //Orderna a população (selection Sort)
                pop.OrderPopulation();

                //Pega os primeiros individuos da população ordenada (por isso o i vai ate menor que o tamanho do elitismo)
                for(int i = 0; i < ConfigurationGA.tamElitismo; i++)
                {
                    //Armazena os melhores individuos da população corrente na variavel IndElit
                    IndElit[i] = pop.GetPopulation()[i];
                }
            }
            
            //Faz um for que rodara apenas metade da população, pois tem que liberar espaço para os filhos
            for (int i = 0; i < (ConfigurationGA.tamPopulacao/2); i++)
            {
                //Seleciona os pais após o  torneio
                Individuo pai1 = selection(pop);
                Individuo pai2 = selection(pop);

                //Variavel random que define se vai ou não haver cruzamento
                double sortCrossNum = ConfigurationGA.random.NextDouble();

                //Se o numero sorteado for menor que a taxa escolhida (padrão 60%) os pais serão colocados para cruzamento
                if (sortCrossNum <= taxaCrossover)
                {
                   //Cria um vetor de filhos, que serão criados após o crossover dos pais
                   Individuo[] children =  crossover(pai1, pai2);
                    
                    //Caso a mutação em noovo ind estiver selecionada, o algoritmo entrara neste if para mutar o novos individuos
                    if (ConfigurationGA.mutationType == ConfigurationGA.Mutation.NovoInd)
                    {
                        children[0] = Mutation(children[0]);
                        children[1] = Mutation(children[1]);
                    }
                    
                    //Rearranjar os filhos no vetor, ou seja passa o index do vetor dos pais para os filhos
                    children[0].indexDoVetor = pai1.indexDoVetor;
                    children[1].indexDoVetor = pai2.indexDoVetor;

                    //Após isso os filhos são armazenados na população temporario , no index do vetor dos pais
                    popTemp[pai1.indexDoVetor] = children[0]; 
                    popTemp[pai2.indexDoVetor] = children[1];
                    
                }
                else
                {
                    //Caso a mutação nao aconteça, voltamos os pais para a população
                    popTemp[pai1.indexDoVetor] = pai1;
                    popTemp[pai2.indexDoVetor] = pai2;
                }
            }

            //Inserir os novos membros
            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
                //Inserimos a populaçao temporaria na nova população
                newPopulation.SetIndividuos(i, popTemp[i]);
            }
            
            //Limpamos a população temporaria
            popTemp = null;

            //Aplicação de mutacao na população, caso o usario opte pelo mesmo
            if(ConfigurationGA.mutationType == ConfigurationGA.Mutation.NaPopulacao)
            {
                newPopulation = MutationThePopulation(newPopulation);
            }
            
            //Inserindo os individuos do elitismoo na nova pop
            if (ConfigurationGA.elitismo)
            {
                //Avaliar a pop
                newPopulation.Avaliacao();
                //Orderar a pop
                newPopulation.OrderPopulation();
         
                //A mesma quantidade selecionada para elitismo é retirada da população
                int initPoint = ConfigurationGA.tamPopulacao - ConfigurationGA.tamElitismo;
                int count = 0;

                for (int i = initPoint; i < ConfigurationGA.tamPopulacao; i++)
                {
                    //Percorremos o que se inicia quase no fim da população e retiramos os ultimos individuos (piores individuos) e colocamos os individuos 
                    //selecionados pelo elitismo
                    newPopulation.GetPopulation()[i] = IndElit[count];
                    count++;
                }
            }
            #endregion
            //Avaliação da nova população preparando para retorna-la
            newPopulation.Avaliacao();
            return newPopulation;
        }

        //Cruzamento PMX Partial Mapped Crossoveer
        public Individuo[] CrossoverPMX(Individuo pai1, Individuo pai2)
        {

            //Variavel de retorno do individuo que sera gerado
            Individuo[] newInd = new Individuo[2];

            int[] parent1 = new int[ConfigurationGA.tamCromossomo];
            int[] parent2 = new int[ConfigurationGA.tamCromossomo];

            int[] offspring1Vector = new int[ConfigurationGA.tamCromossomo];
            int[] offspring2Vector = new int[ConfigurationGA.tamCromossomo];

            int[] replacement1 = new int[ConfigurationGA.tamCromossomo];
            int[] replacement2 = new int[ConfigurationGA.tamCromossomo];

            //Seleção dos pontos para cruzamento aleatoriamente
            int pontoUm = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);
            int pontoDois = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);

            //Verifica se o ponto dois é menor que o ponto um, caso for ele inverte os pontos
            //Se  os pontos forem iguais ele adicionada +1 no ponto dois fazendo-o ficar diferente 
            if(pontoDois < pontoUm)
            {
                int tmp = pontoDois;
                pontoDois = pontoUm;
                pontoUm = tmp;
            }
            else if(pontoUm == pontoDois)
            {
                pontoDois++;
            }

            //Cria a instancia dos dois novos filhos 
            newInd[0] = new Individuo();
            newInd[1] = new Individuo();

            //Transferir os genes dos pais para um parente e para um vetor de descendencia 
            for(int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                parent1[i] = offspring1Vector[i] = pai1.GetGene(i);
                parent2[i] = offspring2Vector[i] = pai2.GetGene(i);
            }

            //popular o replacemente em valores abaixo de 0
            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                replacement1[i] = replacement2[i] = -1;
            }

            //Passo de cruzamento
            for(int i = pontoUm; i <= pontoDois; i++)
            {
                //Passa para o offspring os valores da faixa de cruzamento
                offspring1Vector[i] = parent2[i];
                offspring2Vector[i] = parent1[i];
                 
                //O parent1 passa os valores da faixa de cruzamento para o vetor replacement1 utilizando o index do parent2 
                replacement1[parent2[i]] = parent1[i];
                replacement2[parent1[i]] = parent2[i];
            }

            //troca de genes repetidos
            for(int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                if ((i >= pontoUm) && (i <= pontoDois))
                    continue;

                //Pega os valore fora da faixa selecionada para cruzamento e armazena em n1 depois pega n1 e usa como index para o replacemente(onde possuimos os numeros repetidos)
                int n1 = parent1[i];
                int m1 = replacement1[n1];

                int n2 = parent2[i];
                int m2 = replacement2[n2];

                //Se o M1 for igual a -1 segnifica que o valore do vetor sera trocado
                while(m1 != -1)
                {
                    //N1 recebe o m1 um que é um valor do parent2 passado ao replacement1
                    n1 = m1;
                    m1 = replacement1[m1];
                }

                while(m2 != -1)
                {
                    n2 = m2;
                    m2 = replacement2[m2];
                }

                //Apos pegar os valores para troca, ele desfaz os genes repetidos
                offspring1Vector[i] = n1;
                offspring2Vector[i] = n2;
            }

            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                //Seta os novos individuos usando o vetor de descendencia que esta sem repetições
                newInd[0].SetGene(i, offspring1Vector[i]);
                newInd[1].SetGene(i, offspring2Vector[i]);
            }

            //Calcula o fitness dos novos indiviuos e os retorna loga em seguida 
            newInd[0].CalcFitness();
            newInd[1].CalcFitness();
            return newInd;
        }

        //Mutação tipo SWAP
        public Individuo Mutation(Individuo ind)
        {
            //Verifica se vai mutar
            if(ConfigurationGA.random.NextDouble() <= taxaMutacao)
            {
                
                //escolher os pontos de mutação aleatoriamente
                int genePosition1 = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);
                int genePosition2 = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);

                //Se os genes forem iguais ele joga a posição do segundo para frente
                if (genePosition1 == genePosition2)
                {
                    //Faz -2 para previnir que o gene nao saia do index do vetor
                    if(genePosition2 < ConfigurationGA.tamCromossomo - 2)
                    {
                        genePosition2++;
                    }
                }

                //Envia os individuos e as posições selecionadas para mutação 
                ind.Mutacao(genePosition1, genePosition2);
                return ind;
            }
            return ind;
        }

        //Mutar cada individuo da populacao
        public Population MutationThePopulation(Population pop)
        {
            //Roda toda a população
            for (int i = 0; i < ConfigurationGA.tamPopulacao; i++)
            {
				
				//Verifica se vai mutar
				if (ConfigurationGA.random.NextDouble() <= taxaMutacao)
                {
                    //escolher os pontos de mutação
                    int genePosition1 = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);
                    int genePosition2 = ConfigurationGA.random.Next(0, ConfigurationGA.tamCromossomo - 1);

                    //Caso os genes seejam iguais jogamos a posição dele para cima ou para baixo apenas para nao sair do vetor
                    if (genePosition1 == genePosition2)
                    {
                        if (genePosition2 < ConfigurationGA.tamCromossomo - 2)
                        {
                            genePosition2++;
                        }
                        else
                        {
                            genePosition2 -= 2;
                        }
                    }
                    //Passa pegando os individuos da população e enviando para mutação
                    pop.GetPopulation()[i].Mutacao(genePosition1, genePosition2);
                }
            }

            //Retornamos a população ja mutada
            return pop;
        }
    
		//Seleção por torneio
        public Individuo Torneio(Population pop)
        {
            //Vetor de competidores, definido pela condiguranção GA
            Individuo[] competidores = new Individuo[ConfigurationGA.numCompetidor];

            //individuo auxiliar
            Individuo aux = new Individuo();
            //O fitness dele é setado para infinito, pois sempre na primeira iteração ele precisará ser alterado
            aux.SetFitness(float.PositiveInfinity);

            //Selecao de competidores
            for(int i = 0; i < ConfigurationGA.numCompetidor; i++)
            {
                //Aqui instanciamos e logo apos selecionamos os competidores aleatoriamente
                competidores[i] = new Individuo();
                competidores[i] = pop.GetPopulation()[ConfigurationGA.random.Next(0, ConfigurationGA.tamPopulacao - 1)];
            }

            //for responsavel por fazer a comparação entre os competidores (torneio ou competição entre eles)
            for (int i = 0; i < ConfigurationGA.numCompetidor; i++)
            {
                if(competidores[i].GetFitness() < aux.GetFitness())
                {
                    aux = competidores[i];
                    aux.CalcFitness();
                }
            }
            return aux;
        }
    }
}
