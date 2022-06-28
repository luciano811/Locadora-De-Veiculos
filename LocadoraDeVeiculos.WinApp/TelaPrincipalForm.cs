﻿using LocadoraDeVeiculos.Infra.BancoDados.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.BancoDados.ModuloPessoaJuridica;
using LocadoraDeVeiculos.Infra.BancoDados.ModuloPessoaFisica;
using LocadoraDeVeiculos.WinApp.Compartilhado;
using LocadoraDeVeiculos.WinApp.ModuloFuncionario;
using LocadoraDeVeiculos.WinApp.ModuloPessoaJuridica;
using LocadoraDeVeiculos.WinApp.ModuloPessoaFisica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraDeVeiculos.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;

        public TelaPrincipalForm()
        {
            InitializeComponent();

            Instancia = this;

            labelRodape.Text = string.Empty;
            labelTipoCadastro.Text = string.Empty;

            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void pessoaJurídicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnAdicionarItens.Enabled = configuracao.AdicionarItensHabilitado;
            btnAtualizarItens.Enabled = configuracao.AtualizarItensHabilitado;
            btnFiltrar.Enabled = configuracao.FiltrarHabilitado;
            btnAgrupar.Enabled = configuracao.AgruparHabilitado;
            btnVisualizar.Enabled = configuracao.VisualizarHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnAdicionarItens.ToolTipText = configuracao.TooltipAdicionarItens;
            btnAtualizarItens.ToolTipText = configuracao.TooltipAtualizarItens;
            btnFiltrar.ToolTipText = configuracao.TooltipFiltrar;
            btnAgrupar.ToolTipText = configuracao.TooltipAgrupar;
            btnVisualizar.ToolTipText = configuracao.TooltipVisualizar;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            var repositorioPessoaJuridica = new RepositorioPessoaJuridicaEmBancoDados();
            var repositorioPessoaFisica = new RepositorioPessoaFisicaEmBancoDados();

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Funcionário", new ControladorFuncionario(repositorioFuncionario));
            controladores.Add("Pessoa Jurídica", new ControladorPessoaJuridica(repositorioPessoaJuridica));
            controladores.Add("Pessoas Físicas", new ControladorPessoaFisica(repositorioPessoaFisica));
        }

        private void pessoasFísicasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }
    }
}
