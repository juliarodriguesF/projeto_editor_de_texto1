using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace projeto_editor_de_texto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //criar funções
        private void Novo() //função que abre novo arquivo
        {
            rtb_text.Clear(); //limpa a caixa de texto 
            rtb_text.Focus();
        }
        private void Abrir()
        {
            this.openFileDialog1.Title = "Abir Arquivo"; //pega o objeto "open file"(que abre a caixa para o explorador
            openFileDialog1.InitialDirectory = @"C:\Users\autologon"; //indica o diretorio que deve ser aberto inicialemnte
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "*.TXT| *.txt| Todos Arquivos (*.*) | *.* ";// filtra o formato do arquivo 

            try
            {
                if (this.openFileDialog1.ShowDialog() == DialogResult.OK) //abre a janela pra selecionar o arquivo, e quando o
                                                                          //usuário clicar em ok, ele executa o if
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read); //abre o arquivo
                                                                                                                   //seleciondo em modo de leitura
                    StreamReader sr = new StreamReader(arquivo);//permite que o conteudo seja lido
                    sr.BaseStream.Seek(0, SeekOrigin.Begin); //vai pro inicio do arquivo
                    this.rtb_text.Text = " "; //limpa
                    string linha = sr.ReadLine(); //le a primeira linha do arquivo e armazena 

                    while (linha != null) //le as linhas do arquivo até que não haja mais linhas para ler 
                    {
                        this.rtb_text.Text += linha + "\n";
                        linha = sr.ReadLine();
                    }
                    sr.Close(); //fecha o stream reader (leitor) 


                }

            }
            catch (Exception ex)//mensagem de erro 
            {
                MessageBox.Show("Erro ao abrir o arquivo" + ex.Message, "Sistema Informa", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Salvar() //criando função para salvar os arquivos
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)//abre uma janela para salvar o arquivo
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(arquivo);
                    sw.Flush();
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    sw.Write(this.rtb_text.Text);
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Sistema Informa", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Copiar() //função de copiar 
        {
            if (rtb_text.SelectionLength > 0) //condição, se selecionado um texto
            {
                rtb_text.Copy();
            }
        }
        private void Colar() //função de colar
        {
            rtb_text.Paste();
        }

        private void Negrito()// função de negrito 
        {
            string nomeFonte = rtb_text.SelectionFont.Name;
            float tamanhoFonte = rtb_text.SelectionFont.Size;
            bool resp = rtb_text.SelectionFont.Bold;

            if (resp == false)
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Bold);
            }
            else
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }

        }

        private void Italico() //função de itálico
        {
            string nomeFonte = rtb_text.SelectionFont.Name;
            float tamanhoFonte = rtb_text.SelectionFont.Size;
            bool resp = rtb_text.SelectionFont.Italic;

            if (resp == false)
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Italic);
            }
            else
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }
        }

        private void Underline() //função para sublinhar 
        {
            string nomeFonte = rtb_text.SelectionFont.Name;
            float tamanhoFonte = rtb_text.SelectionFont.Size;
            bool resp = rtb_text.SelectionFont.Underline;

            if (resp == false)
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Underline);
            }
            else
            {
                rtb_text.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }
        }
        private void Esquerda() //função para alinhar a esquerda 
        {
            rtb_text.SelectionAlignment = HorizontalAlignment.Left;
        }
        
        private void Centralizar()
        {
            rtb_text.SelectionAlignment = HorizontalAlignment.Center;

        }

        private void Direita()
        {
            rtb_text.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void MudarFonte()
        {
            // Verifica se há algum texto selecionado
            if (rtb_text.SelectedText.Length > 0)
            {
                // Cria um novo FontDialog
                using (FontDialog fontDialog = new FontDialog())
                {
                    // Define a fonte inicial do FontDialog como a fonte da seleção atual no RichTextBox
                    fontDialog.Font = rtb_text.SelectionFont ?? rtb_text.Font;

                    // Exibe o FontDialog e verifica se o usuário clicou em OK
                    if (fontDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Se o usuário clicar em OK, altera a fonte do texto selecionado para a fonte selecionada
                        rtb_text.SelectionFont = fontDialog.Font;
                    }
                }
            }
            else
            {
                // Se não houver texto selecionado, exibe uma mensagem informando ao usuário
                MessageBox.Show("Por favor, selecione o texto que deseja alterar a fonte.", "Nenhuma Seleção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Novo();
        }
      
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void btn_colar_Click(object sender, EventArgs e)
        {
            Colar();
        }

        private void btn_bold_Click(object sender, EventArgs e)
        {
            Negrito();
        }

        private void btn_italico_Click(object sender, EventArgs e)
        {
            Italico();
        }

        private void btn_sublinhado_Click(object sender, EventArgs e)
        {
            Underline();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            Esquerda();
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            Direita();
        }

        private void btn_centro_Click(object sender, EventArgs e)
        {
            Centralizar();
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_text.Undo();
        }

        private void refazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_text.Redo();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colar();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Novo();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Associa o documento de impressão ao diálogo de impressão
                printDialog1.Document = printDocument1;

                // Obtém o texto do controle RichTextBox chamado 'rtb_Texto' e o armazena em uma string
                string strTexto = this.rtb_text.Text;

                // Cria um objeto StringReader para ler o texto armazenado em 'strTexto'
                StringReader ler = new StringReader(strTexto);

                // Abre a caixa de diálogo de impressão e verifica se o usuário clicou em OK
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Se o usuário clicar em OK, inicia a impressão do documento
                    printDocument1.Print();
                }

                /*
                // Código comentado para visualizar a impressão antes de realmente imprimir
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                */
            }
            catch (Exception erroP)
            {
                // Se ocorrer um erro, exibe uma mensagem de erro com a descrição do problema
                MessageBox.Show("Erro de Impressão:" + erroP.Message);
            }
            try
            {
                // Obtém o texto do controle RichTextBox chamado 'rtb_Texto' e o armazena em uma string
                string strTexto = this.rtb_text.Text;

                // Cria um objeto StringReader para ler o texto armazenado em 'strTexto'
                StringReader ler = new StringReader(strTexto);

                // Cria um objeto PrintPreviewDialog para visualizar a impressão
                //PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                var ppd = printPreviewDialog1;

                // Associa o documento de impressão ao PrintPreviewDialog
                ppd.Document = printDocument1;

                // Define o título da janela de visualização de impressão
                ppd.Text = "CCS - Visualizando a impressão";

                // Maximiza a janela de visualização de impressão
                ppd.WindowState = FormWindowState.Maximized;

                // Define o nível de zoom da visualização para 100%
                ppd.PrintPreviewControl.Zoom = 1;

                // Define o estilo da borda da janela de visualização de impressão
                ppd.FormBorderStyle = FormBorderStyle.Fixed3D;

                // Mostra a janela de visualização de impressão
                ppd.ShowDialog();
            }
            catch (Exception erroP)
            {
                // Se ocorrer um erro, exibe uma mensagem de erro com a descrição do problema
                MessageBox.Show("Erro de Impressão:" + erroP.Message);
            }

            // O método a seguir é chamado quando uma página precisa ser impressa
            
                

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Inicializa variáveis para controle de linhas e posição de impressão
            float linhasPorPagina = 0;
            float Posicao_Y = 0;
            int contador = 0;

            // Cria um StringReader para ler o texto do RichTextBox 'rtb_Texto'
            StringReader ler = new StringReader(rtb_text.Text);

            // Define as margens esquerda e superior com valores mínimos
            float MargemEsquerda = e.MarginBounds.Left - 50;
            float MargemSuperior = e.MarginBounds.Top - 50;

            // Ajusta as margens se estiverem muito pequenas
            if (MargemEsquerda < 5)
                MargemEsquerda = 20;
            if (MargemSuperior < 5)
                MargemSuperior = 20;

            // Define a fonte a ser usada na impressão, utilizando a mesma fonte do RichTextBox
            Font FonteDeImpressao = this.rtb_text.Font;

            // Define um pincel preto para desenhar o texto
            SolidBrush meupincel = new SolidBrush(Color.Black);

            // Calcula o número de linhas que cabem em uma página com base na altura das margens e da fonte
            linhasPorPagina = e.MarginBounds.Height / FonteDeImpressao.GetHeight(e.Graphics);

            // Lê a primeira linha do texto
            string linha = ler.ReadLine();

            // Loop para imprimir linha por linha até preencher a página ou acabar o texto
            while (contador < linhasPorPagina)
            {
                // Calcula a posição Y da próxima linha
                Posicao_Y = (MargemSuperior + (contador * FonteDeImpressao.GetHeight(e.Graphics)));

                // Desenha a linha atual na posição calculada
                e.Graphics.DrawString(linha, FonteDeImpressao, meupincel, MargemEsquerda, Posicao_Y, new StringFormat());

                // Incrementa o contador de linhas
                contador += 1;

                // Lê a próxima linha do texto
                linha = ler.ReadLine();
            }

            // Verifica se ainda há mais linhas para imprimir
            if ((linha != null))
            {
                // Se houver mais linhas, sinaliza que outra página é necessária
                e.HasMorePages = true;
            }
            else
            {
                // Caso contrário, sinaliza que a impressão está completa
                e.HasMorePages = false;
            }

            // Libera o recurso de pincel após a impressão
            meupincel.Dispose();
        }

        private void btn_fonte_Click(object sender, EventArgs e)
        {
            MudarFonte();
        }
    }
    }

