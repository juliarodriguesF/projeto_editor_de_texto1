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

                    while ( linha != null ) //le as linhas do arquivo até que não haja mais linhas para ler 
                    { 
                        this.rtb_text.Text+= linha +"\n";
                        linha = sr.ReadLine();
                    }
                    sr.Close(); //fecha o stream reader (leitor) 
                    

                }

            }
            catch (Exception ex)//mensagem de erro 
            { 
                MessageBox.Show("Erro ao abrir o arquivo" +  ex.Message, "Sistema Informa", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Salvar()
        {
            try 
            {  
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)//abre uma janela para salvar o arquivo
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sr = new StreamWriter(arquivo);
                }

            }
            catch (Exception)
            {

                
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Novo();
        }
      
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
