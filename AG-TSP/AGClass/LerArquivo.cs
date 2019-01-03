using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AG_TSP.AGClass;
using ZedGraph;

namespace AG_TSP.AGClass
{
    class LerArquivo
    {
        private string arquivo;
        private string mensagem;
        private List<string> linha;
        private StreamReader texto;

        public LerArquivo()
        {
            List<string> mensagemLinha = new List<string>();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Importar TSPLIB";
                openFileDialog.InitialDirectory = @"C:\Users\bfgz\Desktop\Arquivos teste"; //Se ja quiser em abrir em um diretorio especifico
                openFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    arquivo = openFileDialog.FileName;
            }
            if (!String.IsNullOrEmpty(arquivo))
            {
                using (texto = new StreamReader(arquivo))
                {
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem);
                    }
                }
                //total de linhas do arquivo.
                int registro = mensagemLinha.Count;
            }
            else
            {
                CancelouOperação();
            }
            linha = mensagemLinha;
        }
        
        public bool CancelouOperação()
        {
            if (String.IsNullOrEmpty(arquivo))
            {          
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetMensagemLinha(int i)
        {
            return linha[i];
        }

        public string GetMensagem()
        {
            return mensagem;
        }

        public string GetTipoProblema()
        {
            string frase;
            string palavra = "TYPE";

            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i].Contains(palavra))
                {
                    frase = linha[i];


                    string[] indicePalavra = frase.Split();
                    return indicePalavra[indicePalavra.Length - 1];
                }
            }
            return "Tipo do problema não encontrado";
        }

        public string GetNome()
        {
            string frase;
            string palavra = "NAME";

            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i].Contains(palavra))
                {
                    frase = linha[i];


                    string[] indicePalavra = frase.Split();
                    return indicePalavra[indicePalavra.Length - 1];
                }
            }
            return "Nome não encontrado";
        }

        public int GetDimensao()
        {
            string frase;
            string palavra = "DIMENSION";

            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i].Contains(palavra))
                {
                    frase = linha[i];

                    string[] indicePalavra = frase.Split();
                    int numero = Convert.ToInt32(indicePalavra[indicePalavra.Length - 1]);
                    return numero;
                }
            }

            return -1;
        }

        public string GetTipoCoordenada()
        {
            string frase;
            string palavra = "EDGE_WEIGHT_TYPE";

            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i].Contains(palavra))
                {
                    frase = linha[i];


                    string[] indicePalavra = frase.Split();
                    return indicePalavra[indicePalavra.Length - 1];
                }
            }
            return "Nome não encontrado";
        }

        public int[] GetCoordenada(int i)
        {
            string frase;
            string palavra = "NODE_COORD_SECTION";

            int[] coord = new int[2];

            frase = linha[i];


            string[] indicePalavra = frase.Split();
            Console.WriteLine(indicePalavra[indicePalavra.Length - 1]);

                    
            if (linha[i].Contains("EOF"))
            {
                return coord;
            }
            else
            {
                frase = linha[i];
                indicePalavra = frase.Split();

                int cx = Convert.ToInt32(indicePalavra[1]);
                int cy = Convert.ToInt32(indicePalavra[2]);
                

                coord[0] = cx;
                coord[1] = cy;

                return coord;
            }
        }

        public void PlotarCoordenadas()
        {
            string frase;
            string palavra = "NODE_COORD_SECTION";
            TablePoints.clear();


            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i].Contains(palavra))
                {
                    frase = linha[i];


                    string[] indicePalavra = frase.Split();
                    Console.WriteLine(indicePalavra[indicePalavra.Length - 1]);

                    for (int x = i + 1; x < linha.Count; x++)
                    {
                        if (linha[x].Contains("EOF"))
                        {
                           
                        }
                        else
                        {
                            frase = linha[x];
                            indicePalavra = frase.Split();

                            int ponto = Convert.ToInt32(indicePalavra[0]);
                            int cx = Convert.ToInt32(indicePalavra[1]);
                            int cy = Convert.ToInt32(indicePalavra[2]);
                            TablePoints.AddPoint(cx, cy);
                        }
                    }
                }
            }
 
        }    
    }
}
