using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_TSP.AGClass
{
    public static class TablePoints
    {
        private static ArrayList X = new ArrayList();     //Array de valores de X
        private static ArrayList Y = new ArrayList();     //Array de valores de Y

        private static double[,] tableDist;               //Tabela com distancias entre pontos
        public static int pointCount = 0;                 //Quantidade de pontos na tabela

        //Adicionar um ponto
        public static void AddPoint(int x, int y)
        {
            X.Add(x);
            Y.Add(y);
            pointCount++;
            generateTable();
        }
        //Gerar a tabela com os valores de distancia entre dois pontos
        public static void generateTable()
        {
            tableDist = new double[pointCount, pointCount];

            for(int i = 0; i < pointCount; i++)//para X
            {
                for(int j = 0; j < pointCount; j++)//para y
                {
                    //Calculo de distancia entre dois pontos
                    tableDist[i, j] = Math.Sqrt(Math.Pow(   int.Parse(X[i].ToString()) 
                                                          - int.Parse(X[j].ToString()), 2)
                                                          + Math.Pow(int.Parse(Y[i].ToString())
                                                          - int.Parse(Y[j].ToString()), 2));
                }
            }
            //Atualizar o tamanho do cromossomo
            ConfigurationGA.tamCromossomo = pointCount;
        }
        
        //Retornar a tablela de distancia
        public static double[,] getTableDist()
        {
            return tableDist;
        }

        //Retornar a distancia entre dois pontos
        public static double getDist(int pointOne, int pointTwo)
        {
            return tableDist[pointOne, pointTwo];
        }

        //Retornar quantidade de pontos dentro da classe
        public static int Count()
        {
            return pointCount;
        }   

        //Retornar a coordenada de um ponto, [X, Y]
        public static int[] getCoordenadas(int point)
        {

            int[] arrayCoordenadas = new int[2];
            arrayCoordenadas[0] = int.Parse(X[point].ToString());
            arrayCoordenadas[1] = int.Parse(Y[point].ToString());
            return arrayCoordenadas;
        }

        //Limpar dados da tabela
        public static void clear()
        {
            X.Clear();
            Y.Clear();
            pointCount = 0;
            tableDist = null;
        }
    }
}
