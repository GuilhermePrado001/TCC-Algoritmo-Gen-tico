using AG_TSP.AGClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;

namespace AG_TSP
{
    public partial class AG_Principal : Form
    {
        #region Variaveis Globais
        Graphics g;                                 //Desenhar elementos na tela
        int count = 0;                              //Contador para verificar quantidade de pontos na tela
        int countAux = 0;
        int pointCount = 0;                         //Sequenciador para indentificar pontos na tela
        int count_exec = 0;

        //ZedGraph
        private GraphPane paneMedia;
        private PointPairList mediaPopulacao = new PointPairList();


        //Objeto Populacao
        Population pop;
        int popTamAux = int.MaxValue;

        int evolucoes = 0;
        int i = 0;
        int iTemp = 0;          //iterações
        double bestAux = double.PositiveInfinity;
        #endregion

        public AG_Principal()
        {
            InitializeComponent();
            panelLeft.Visible = false;

            paneMedia = zedMedia.GraphPane;
            paneMedia.Title.Text = Resources.Resource.media_pop;
            paneMedia.XAxis.Title.Text = Resources.Resource.evolucao;
            paneMedia.YAxis.Title.Text = Resources.Resource.media_fitness;
            zedMedia.Refresh();
        }

        private void AG_Principal_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        //Metodos Visuais

        private void BtnTelaPrincipal_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnTelaPrincipal.Height;
            panelLeft.Top = btnTelaPrincipal.Top;

            #region Seta os componentes do menu com Visible TRUE ou FALSE

            AlteraViewStateConf(false);
            panelMenu.Visible = false;
            hideForeGround.Visible = false;
            panelLeft.Visible = true;
            zedMedia.Visible = false;
            panelMenu.Visible = true;

            #endregion

            btnTelaPrincipal.Enabled = false;
            btnGrafico.Enabled = true;
            btnInformacao.Enabled = true;
            btnConfiguracao.Enabled = true;

            PlotPoints();

            try
            {
                if (pop != null && count_exec != 0)
                {

                    PlotLines(pop, Color.Blue);

                }
            }
            catch (Exception)
            {

            }

        }

        private void BtnGrafico_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnGrafico.Height;
            panelLeft.Top = btnGrafico.Top;

            btnTelaPrincipal.Enabled = true;
            btnGrafico.Enabled = false;
            btnInformacao.Enabled = true;
            btnConfiguracao.Enabled = true;

            #region Seta os componentes do menu com Visible TRUE ou FALSE

            imgPanel.Visible = false;
            panelMenu.Visible = false;
            zedMedia.Visible = true;
            panelLeft.Visible = true;
            panelInformacoes.Visible = false;
            hideForeGround.Visible = true;
            AlteraViewStateConf(false);

            #endregion
        }

        private void BtnInformacao_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnInformacao.Height;
            panelLeft.Top = btnInformacao.Top;

            btnTelaPrincipal.Enabled = true;
            btnGrafico.Enabled = true;
            btnInformacao.Enabled = false;
            btnConfiguracao.Enabled = true;


            #region Seta os componentes do menu com Visible TRUE ou FALSE

            panelMenu.Visible = false;
            AlteraViewStateConf(false);

            #endregion

            #region aplicão do Resources nas labels
            //Labels
            tipoCruzamento.Text = Resources.Resource.tipo_cruzamento;
            tipoMutacao.Text = Resources.Resource.tipo_mutacao;
            tipoSelecao.Text = Resources.Resource.tipo_selecao;
            elitismo.Text = Resources.Resource.elitismo;
            EvolucaoInfo.Text = Resources.Resource.evolucao;
            txMutacaoInfo.Text = Resources.Resource.taxa_mutacao;
            txCruzamentoInfo.Text = Resources.Resource.taxa_cruzamento;

            //Texto
            lbtipoCruzamento.Text = "PMX";
            lbltipomutacao.Text = ConfigurationGA.mutationType.ToString();
            lbInfoSelecao.Text = Resources.Resource.torneio + Resources.Resource.qtnd_ind_torneio + ConfigurationGA.numCompetidor + " " + Resources.Resource.individuo;
            lbInfoEvolucao.Text = lbEvolucoes.Text;
            lbInfoTaxaMutacao.Text = txtTaxaMutacao.Text;
            lbInfoTaxaCruzamento.Text = txtTaxaCrossover.Text;


            if (checkElitismoo.Checked)
            {
                lbInfoElitismo.Text = Resources.Resource.qtnd_ind_elitismo + ConfigurationGA.tamElitismo + " " + Resources.Resource.individuo;
            }
            else
            {
                lbInfoElitismo.Text = Resources.Resource.desabilitado;
            }

            try
            {
                if (pop != null)
                {
                    txtInd.Text = pop.GetBest().ToString();
                }
            }
            catch (Exception)
            {

            }

            #endregion

            #region Seta os componentes do menu com Visible TRUE ou FALSE

            zedMedia.Visible = false;
            hideForeGround.Visible = true;
            panelLeft.Visible = true;
            panelInformacoes.Visible = true;

            #endregion

        }

        private void BtnConfiguracao_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnConfiguracao.Height;
            panelLeft.Top = btnConfiguracao.Top;

            btnTelaPrincipal.Enabled = true;
            btnGrafico.Enabled = true;
            btnInformacao.Enabled = true;
            btnConfiguracao.Enabled = false;

            #region Seta os componentes do menu com Visible TRUE ou FALSE

            imgPanel.Visible = false;
            hideForeGround.Visible = true;
            zedMedia.Visible = false;
            panelLeft.Visible = true;
            panelMenu.Visible = false;
            panelInformacoes.Visible = false;
            AlteraViewStateConf(true);

            #endregion        
        }

        private void BtnDicas_Click(object sender, EventArgs e)
        {
            Ajuda newForm2 = new Ajuda();
            newForm2.ShowDialog();

            btnTelaPrincipal.Enabled = true;
            btnGrafico.Enabled = true;
            btnInformacao.Enabled = true;
            btnConfiguracao.Enabled = true;


            PlotPoints();
            try
            {
                if (pop != null && count_exec != 0)
                {
                    PlotLines(pop, Color.Blue);

                }
            }
            catch (Exception)
            {

            }

        }

        private string AplicaLinguagem(string language)
        {
            if (language.Equals("en-US"))
            {
                CultureInfo cultura = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = cultura;
            }
            else if (language.Equals("pt-BR"))
            {
                CultureInfo cultura = new CultureInfo("pt-BR");
                Thread.CurrentThread.CurrentUICulture = cultura;
            }

            #region aplicão do Resources nas labels do Menu

            //Botões do menu
            btnTelaPrincipal.Text = Resources.Resource.tela_principal;
            btnGrafico.Text = Resources.Resource.grafico;
            btnInformacao.Text = Resources.Resource.informacao;
            btnConfiguracao.Text = Resources.Resource.idioma;

            #endregion

            #region aplicão do Resources nas labels da aba tela principal

            //Labels da tela principal
            btnCriarPop.Text = Resources.Resource.btn_criar_pop;
            btnExecutar.Text = Resources.Resource.btn_executar;
            btnLimpar.Text = Resources.Resource.btn_limpar;
            btnInserir.Text = Resources.Resource.btn_inserir;

            btnImportar.Text = Resources.Resource.importar;
            btnSalvar.Text = Resources.Resource.salvar;
            labelTamPop.Text = Resources.Resource.tamanho_pop;
            labelTxCruzamento.Text = Resources.Resource.taxa_cruzamento;
            labelTxMutacao.Text = Resources.Resource.taxa_mutacao;
            labelEvo.Text = Resources.Resource.evolucao;
            checkElitismoo.Text = Resources.Resource.elitismo;
            labelTorneio.Text = Resources.Resource.torneio_dois_pontos;
            rbNovoIndividuo.Text = Resources.Resource.novo_ind;
            rbPopulacao.Text = Resources.Resource.geral_populacao;
            labelEvolucao.Text = Resources.Resource.evolucao;
            labelDist.Text = Resources.Resource.menor_dist;
            gbMutacao.Text = Resources.Resource.mutacao;
            labelQtndCidade.Text = Resources.Resource.qtnd_cidades;
            labelalgoritmo.Text = Resources.Resource.algoritmo;
            btnDicas.Text = Resources.Resource.ajuda;
            btnSair.Text = Resources.Resource.sair;
            btn_language.Text = Resources.Resource.alterar;

            #endregion

            #region aplicão do Resources nas labels da aba informações

            //Labels
            tipoCruzamento.Text = Resources.Resource.tipo_cruzamento;
            tipoMutacao.Text = Resources.Resource.tipo_mutacao;
            tipoSelecao.Text = Resources.Resource.tipo_selecao;
            elitismo.Text = Resources.Resource.elitismo;
            EvolucaoInfo.Text = Resources.Resource.evolucao;
            txMutacaoInfo.Text = Resources.Resource.taxa_mutacao;
            txCruzamentoInfo.Text = Resources.Resource.taxa_cruzamento;
            lblMenorDist.Text = Resources.Resource.menor_dist;

            //Texto
            lbtipoCruzamento.Text = "PMX";
            lbltipomutacao.Text = ConfigurationGA.mutationType.ToString();
            lbInfoSelecao.Text = Resources.Resource.torneio + Resources.Resource.qtnd_ind_torneio + ConfigurationGA.numCompetidor + " " + Resources.Resource.individuo;
            lbInfoEvolucao.Text = lbEvolucoes.Text;
            lbInfoTaxaMutacao.Text = txtTaxaMutacao.Text;
            txtTaxaCrossover.Text = txtTaxaCrossover.Text;
            lblBestIndInfo.Text = Resources.Resource.melhor_ind;

            if (checkElitismoo.Checked)
            {
                lbInfoElitismo.Text = Resources.Resource.qtnd_ind_elitismo + ConfigurationGA.tamElitismo + " " + Resources.Resource.individuo;
            }
            else
            {
                lbInfoElitismo.Text = Resources.Resource.desabilitado;
            }

            #endregion

            #region labels graficos
            paneMedia.Title.Text = Resources.Resource.media_pop;
            paneMedia.XAxis.Title.Text = Resources.Resource.evolucao;
            paneMedia.YAxis.Title.Text = Resources.Resource.media_fitness;
            #endregion

            #region aba configurações

            lbIdioma.Text = Resources.Resource.escolha_idioma;
            rbIngles.Text = Resources.Resource.ingles;
            rbPortugues.Text = Resources.Resource.portugues;

            #endregion

            return null;
        }

        private void AlteraViewStateConf(bool statusConf)
        {
            rbIngles.Visible = statusConf;
            rbPortugues.Visible = statusConf;
            lbIdioma.Visible = statusConf;
            btn_language.Visible = statusConf;
        }

        // Metodos de excusão

        private void BtnCriarPop_Click(object sender, EventArgs e)
        {
            if (txtTamPop.Text.Equals("") || txtTamPop.Text.Equals("0"))
                txtTamPop.Text = "1";

            ConfigurationGA.tamPopulacao = int.Parse(txtTamPop.Text);
            popTamAux = int.Parse(txtTamPop.Text);

            pop = new Population();
            btnExecutar.Enabled = true;
            countAux = count;
            bestAux = double.PositiveInfinity;
        }

        private void BtnExecutar_Click(object sender, EventArgs e)
        {
            count_exec++;

            #region  Desativar os botões enquanto executa

            btnDicas.Enabled = false;
            btnInformacao.Enabled = false;
            btnGrafico.Enabled = false;
            btnDicas.Enabled = false;
            btnCriarPop.Enabled = false;
            btnExecutar.Enabled = false;
            btnSair.Enabled = false;
            btnConfiguracao.Enabled = false;
            btnLimpar.Enabled = false;
            btnInserir.Enabled = false;
            btnImportar.Enabled = false;
            btnSalvar.Enabled = false;

            #endregion

            var deuErro = false;

            //Valida se o campo esta nulo
            txtTamPop.Text = txtTamPop.Text.Equals("") ? "1" : txtTamPop.Text;

            //Altera o tamanho da população sem a necessida de pressionar o botão criar pop
            if (popTamAux != int.Parse(txtTamPop.Text))
            {
                ConfigurationGA.tamPopulacao = int.Parse(txtTamPop.Text);
                //Caso a população esteja em 0 ou Nulla é setada como 1
                if (ConfigurationGA.tamPopulacao == 0)
                {
                    ConfigurationGA.tamPopulacao = 1;
                    txtTamPop.Text = "1";
                }
                popTamAux = int.Parse(txtTamPop.Text);
                pop = new Population();
            }

            #region Configuração do AG // Tratamento de entrada de dados nos campos

            //Configurar AG
            txtTaxaMutacao.Text = txtTaxaMutacao.Text.Equals(" ,") ? "0" : txtTaxaMutacao.Text;
            float taxaMutacao = float.Parse(txtTaxaMutacao.Text);

            txtTaxaCrossover.Text = txtTaxaCrossover.Text.Equals(" ,") ? "0" : txtTaxaCrossover.Text;
            float taxaCrossover = float.Parse(txtTaxaCrossover.Text);

            txtQtdeTorneio.Text = txtQtdeTorneio.Text.Equals("") ? "0" : txtQtdeTorneio.Text;
            int torneio = int.Parse(txtQtdeTorneio.Text);

            txtEvolucao.Text = txtEvolucao.Text.Equals("") ? "1" : txtEvolucao.Text;
            evolucoes += int.Parse(txtEvolucao.Text);

            ConfigurationGA.taxaCruzamento = taxaCrossover;
            ConfigurationGA.taxaMutacao = taxaMutacao;
            ConfigurationGA.numCompetidor = torneio;
            ConfigurationGA.Mutation mutacao = ConfigurationGA.Mutation.NovoInd;

            #endregion


            //Verifica o tipo de mutação selecionado
            if (rbNovoIndividuo.Checked)
            {
                mutacao = ConfigurationGA.Mutation.NovoInd;
            }
            else if (rbPopulacao.Checked)
            {
                mutacao = ConfigurationGA.Mutation.NaPopulacao;
            }
            ConfigurationGA.mutationType = mutacao;

            //Verifica se o elitismo foi checkado
            if (checkElitismoo.Checked)
            {
                ConfigurationGA.elitismo = true;

                txtQtdeelitismoo.Text = txtQtdeelitismoo.Text.Equals("") ? "0" : txtQtdeelitismoo.Text;
                ConfigurationGA.tamElitismo = int.Parse(txtQtdeelitismoo.Text);
            }
            else
            {
                ConfigurationGA.elitismo = false;
            }

            //Inicia uma instancia do AG (Metodo responsavel por aplicar o ciclo de evolução)
            AlgoritmoGenetico AG = new AlgoritmoGenetico();

            for (i = iTemp; i < evolucoes; i++)
            {
                iTemp++;
                lbEvolucoes.Text = i.ToString();
                lbEvolucoes.Refresh();

                try
                {
                    //Recebe a população evoluida
                    pop = AG.ExecuteGA(pop);
                }
                catch (IndexOutOfRangeException)
                {
                    //Mostra a mensagem de erro apenas uma vez
                    if (deuErro == false)
                    {
                        MessageBox.Show("Favor, adicionar mais que uma cidade", "AG - TSP");
                        deuErro = true;
                    }
                    //Limpa o necessario para recomeçar
                    ForcarLimpeza();
                    break;
                }

                //Pega o individuo com o melhor fitness
                pop.GetBest().CalcFitness();

                //Verifica se o usario optou pelo 2opt
                if (twoOptCheck.Checked)
                {
                    Utils.TwoOpt(pop.GetBest());
                }

                //Ápos o 2opt pega o melhor individuo novamente, pois o 2opt fez uma nova melhoria
                pop.GetBest().CalcFitness();

                //Limpa o grafico da media da população
                zedMedia.GraphPane.CurveList.Clear();
                zedMedia.GraphPane.GraphObjList.Clear();

                //Fazemos o calcula da media do fitness da população e jogamos no grafico
                double mediaPop = pop.GetMediaPop();
                mediaPopulacao.Add(i, mediaPop);

                //Pegamos o fitness do melhor individuo
                double bestFitness = pop.GetBest().GetFitness();

                //Desenha no grafico
                LineItem media = paneMedia.AddCurve("Média", mediaPopulacao, Color.Red, SymbolType.None);

                //Print linhas a cada 6 evolucoes, entra no if apenas quando o individuo da evolução corrente for melhor do que os individuos das evoluções anteriores
                if (bestFitness < bestAux)
                {
                    bestAux = bestFitness;
                    g.Clear(Color.White);
                    PlotLines(pop, Color.Blue);
                    PlotPoints();

                    //Atualiza laber com a distancia do melhor individuo 
                    lbMenorDistancia.Text = bestFitness.ToString("0.0");
                    lbMenorDistancia.Refresh();

                    lbMenorDistancia2.Text = bestFitness.ToString("0.0");
                    lbMenorDistancia2.Refresh();
                }

                //Redesenhamos o grafico
                zedMedia.AxisChange();
                zedMedia.Invalidate();
                zedMedia.Refresh();
            }

            #region Ativa os botoes novamente

            btnDicas.Enabled = true;
            btnInformacao.Enabled = true;
            btnGrafico.Enabled = true;
            btnDicas.Enabled = true;
            btnExecutar.Enabled = true;
            btnSair.Enabled = true;
            btnConfiguracao.Enabled = true;
            btnLimpar.Enabled = true;
            btnInserir.Enabled = true;
            btnImportar.Enabled = true;
            btnSalvar.Enabled = true;

            #endregion
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            count_exec = 0;
            pointCount = 0;
            i = 0;
            iTemp = 0;
            evolucoes = 0;
            lbEvolucoes.Text = "00";
            lbMenorDistancia.Text = "00";
            lbMenorDistancia2.Text = "00";

            ConfigurationGA.tamPopulacao = 0;
            TablePoints.clear();
            pop = null;

            lbQtdeCidades.Text = "--";

            btnCriarPop.Enabled = false;
            btnExecutar.Enabled = false;
            btnLimpar.Enabled = false;

            g.Clear(Color.White);
            count = 0;

            mediaPopulacao.Clear();
            zedMedia.Refresh();
        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            btnExecutar.Enabled = false;

            Random rnd = new Random();

            maskedCity.Text = maskedCity.Text.Equals("") ? "0" : maskedCity.Text;
            var cities = Convert.ToInt32(maskedCity.Text.ToString());

            for (int k = 0; k <= cities - 1; k++)
            {
                Pen blackPen = new Pen(Color.Red, 3);
                int X = rnd.Next(256, 1279);
                int Y = rnd.Next(31, 520);

                TablePoints.AddPoint(X, Y);

                Rectangle rect = new Rectangle(X - 5, Y - 5, 10, 10);
                g.DrawEllipse(blackPen, rect);
                g.DrawString((pointCount + 1).ToString(), new Font("Arial Black", 11), Brushes.Black, X + 3, Y);
                g.DrawString("X:" + X.ToString(), new Font("Arial Black", 6), Brushes.Black, X - 20, Y - 25);
                g.DrawString("Y:" + Y.ToString(), new Font("Arial Black", 6), Brushes.Black, X - 20, Y - 18);

                pointCount++;
                lbQtdeCidades.Text = pointCount.ToString();
            }
            btnCriarPop.Enabled = true;
            btnLimpar.Enabled = true;
            bestAux = double.PositiveInfinity;
        }

        private void PlotPoints()
        {
            //Vericando se a tabela possui pontos
            if (TablePoints.pointCount > 0)
            {

                for (int i = 0; i < TablePoints.pointCount; i++)
                {
                    //Criar um lapis
                    Pen blackPen = new Pen(Color.Red, 3);
                    //Vetor de coordenadas (x, y) (0, 1)
                    int[] coo = TablePoints.getCoordenadas(i);

                    Rectangle rect = new Rectangle(coo[0] - 5, coo[1] - 5, 10, 10);
                    g.DrawEllipse(blackPen, rect);
                    g.DrawString((i + 1).ToString(), new Font("Arial Black", 11), Brushes.Black, coo[0] + 3, coo[1]);
                    g.DrawString("X:" + coo[0].ToString(), new Font("Arial Black", 6), Brushes.Black, coo[0] - 20, coo[1] - 25);
                    g.DrawString("Y:" + coo[1].ToString(), new Font("Arial Black", 6), Brushes.Black, coo[0] - 20, coo[1] - 18);
                }
            }
        }

        private void PlotLines(Population pop, Color color)
        {
            Pen penBest = new Pen(color, 4);
            int genA, genB;

            Individuo best = pop.GetBest();

            for (int i = 0; i < ConfigurationGA.tamCromossomo; i++)
            {
                if (i < ConfigurationGA.tamCromossomo - 1)
                {
                    genA = best.GetGene(i);
                    genB = best.GetGene(i + 1);
                }
                else
                {
                    genA = best.GetGene(i);
                    genB = best.GetGene(0);
                }

                int[] vetA = TablePoints.getCoordenadas(genA);
                int[] vetB = TablePoints.getCoordenadas(genB);

                g.DrawLine(penBest, vetA[0], vetA[1], vetB[0], vetB[1]);

            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            //Criar um lapis
            Pen blackPen = new Pen(Color.Red, 3);
            int X = e.X;
            int Y = e.Y;

            TablePoints.AddPoint(X, Y);

            Rectangle rect = new Rectangle(X - 5, Y - 5, 10, 10);
            g.DrawEllipse(blackPen, rect);
            g.DrawString((pointCount + 1).ToString(), new Font("Arial Black", 11), Brushes.Black, X + 3, Y);
            g.DrawString("X:" + X.ToString(), new Font("Arial Black", 6), Brushes.Black, X - 20, Y - 25);
            g.DrawString("Y:" + Y.ToString(), new Font("Arial Black", 6), Brushes.Black, X - 20, Y - 18);

            pointCount++;
            lbQtdeCidades.Text = pointCount.ToString();

            if (++count >= 1)
            {
                btnCriarPop.Enabled = true;
            }

            if (++count >= 1)
            {
                btnLimpar.Enabled = true;
            }
            else
            {
                btnLimpar.Enabled = false;
            }

            if (countAux != count)
            {
                btnExecutar.Enabled = false;
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            LerArquivo leitor = new LerArquivo();

            if (!leitor.CancelouOperação())
            {
                g.Clear(Color.White);
                ForcarLimpeza();
                leitor.PlotarCoordenadas();

                #region Cria a população assim que importa

                //Cria a população assim que importa e já gera os ponto na tela
                txtTamPop.Text = txtTamPop.Text.Equals("") ? "1" : txtTamPop.Text;
                ConfigurationGA.tamPopulacao = int.Parse(txtTamPop.Text);

                pop = new Population();
                countAux = count;
                btnCriarPop.Enabled = true;
                btnExecutar.Enabled = false;
                btnLimpar.Enabled = true;

                pointCount += TablePoints.pointCount;
                lbQtdeCidades.Text = pointCount.ToString();
                PlotPoints();

                #endregion
            }
            else
            {
                PlotPoints();

                if (pop != null || count_exec != 0)
                    PlotLines(pop, Color.Blue);
            }
        }

        private void BtnAlterarLingua_Click(object sender, EventArgs e)
        {
            if (rbIngles.Checked)
            {
                AplicaLinguagem("en-US");
            }
            else if (rbPortugues.Checked)
            {
                AplicaLinguagem("pt-BR");
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            //Criar "Dados" para colocar os conteudos dentro dar get nos dados depois ... 

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //define o titulo
            saveFileDialog1.Title = "Salvar Arquivo Texto";
            //Define as extensões permitidas
            saveFileDialog1.Filter = "tsp File|.tsp";
            //define o indice do filtro
            saveFileDialog1.FilterIndex = 0;
            //Atribui um valor vazio ao nome do arquivo
            saveFileDialog1.FileName = "arq_" + lbQtdeCidades.Text;
            //Define a extensão padrão como .txt
            saveFileDialog1.DefaultExt = ".tsp";
            //define o diretório padrão
            saveFileDialog1.InitialDirectory = @"c:\";
            //restaura o diretorio atual antes de fechar a janela
            saveFileDialog1.RestoreDirectory = true;
            //exibe aviso se o usuario informar um caminho que nao existe
            saveFileDialog1.CheckPathExists = true;

            //Abre a caixa de dialogo e determina qual botão foi pressionado
            DialogResult resultado = saveFileDialog1.ShowDialog();

            PlotPoints();
            if (pop != null && count_exec != 0)
            {
                PlotLines(pop, Color.Blue);
            }
            //Se o ousuário pressionar o botão Salvar
            if (resultado == DialogResult.OK)
            {
                //Cria um stream usando o nome do arquivo
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);
                //Colocar informacoes dos dados...
                writer.WriteLine("Name: TC_AG ");
                writer.WriteLine("Type: TSP ");
                writer.WriteLine("DIMENSION: " + pointCount);
                writer.WriteLine("EDGE_WEIGHT_TYPE: EUC_2D");
                writer.WriteLine("NODE_COORD_SECTION");
                for (int i = 0; i < TablePoints.pointCount; i++)
                {
                    int[] coo = TablePoints.getCoordenadas(i);
                    writer.Write(i + 1 + " ");
                    writer.Write(coo[0] + " ");
                    writer.Write(coo[1] + "\r\n");
                }
                writer.WriteLine("EOF");
                writer.Close();
            }
        }

        private void ForcarLimpeza()
        {
            i = 0;
            iTemp = 0;
            evolucoes = 0;
            count_exec = 0;
            lbEvolucoes.Text = "00";
            lbMenorDistancia.Text = "00";
            lbMenorDistancia2.Text = "00";

            lbQtdeCidades.Text = "--";

            btnCriarPop.Enabled = false;
            btnExecutar.Enabled = false;
            btnLimpar.Enabled = false;

            mediaPopulacao.Clear();
            zedMedia.Refresh();
            btnLimpar.Enabled = true;

        }
    }
}

