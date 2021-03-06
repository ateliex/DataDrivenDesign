﻿using Ateliex.Data;
using Ateliex.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ateliex.Windows
{
    public partial class ModelosWindow
    {
        private readonly AteliexDbContext db;

        public ModelosWindow(AteliexDbContext db)
        {
            this.db = db;

            //modelosService.StatusChanged += SetStatusBar;

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            var modelos = await ObtemModelosAsync();

            foreach (var modelo in modelos)
            {
                modelo.RecursosChanged += Modelo_RecursosChanged;
            }

            modelosViewSource.Source = modelos;
        }

        private void Modelo_RecursosChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var modelo = sender as Modelo;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var recurso = e.NewItems[0] as Recurso;

                recurso.Modelo = modelo;

                var total = recurso.Modelo.Recursos.Count;

                recurso.Id = total;
            }
        }

        public async Task<List<Modelo>> ObtemModelosAsync()
        {
            try
            {
                var modelos = await db.Modelos
                    .Include(p => p.Recursos)
                    .ToListAsync();

                //var observable = modelos.ToObservable();

                return modelos;
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException("Erro ao obter modelos.", ex);
            }
        }

        private void SetStatusBar(string value)
        {
            statusBarLabel.Content = value;

            //statusBarTimer.Enabled = true;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            //var observableCollection = (ModelosCollection)modelosViewSource.Source;

            try
            {
                await db.SaveChangesAsync();

                SetStatusBar("Modelos salvos com sucesso.");
            }
            catch (Exception ex)
            {
                SetStatusBar(ex.Message);
            }
        }

        private void AdicionarModeloButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    //public class ModeloValidationRule : ValidationRule
    //{
    //    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    //    {
    //        ModeloViewModel viewModel = (value as BindingGroup).Items[0] as ModeloViewModel;

    //        if (viewModel.HasErrors)
    //        {
    //            return new ValidationResult(false, viewModel.Error);
    //        }
    //        else
    //        {
    //            return ValidationResult.ValidResult;
    //        }
    //    }
    //}
}
