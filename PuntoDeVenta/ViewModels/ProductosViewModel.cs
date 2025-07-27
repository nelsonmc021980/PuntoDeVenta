using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using PuntoDeVenta.Models;
using PuntoDeVenta.Helpers;
using System.ComponentModel;
using System.Data.Entity.Validation;

namespace PuntoDeVenta.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        private Producto _productoActual = new Producto();

        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        public Producto ProductoActual
        {
            get => _productoActual;
            set
            {
                _productoActual = value;
                OnPropertyChanged(nameof(ProductoActual));
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand NuevoCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProductosViewModel()
        {
            CargarProductos();

            GuardarCommand = new RelayCommand(o => Guardar());
            NuevoCommand = new RelayCommand(o => Nuevo());
            EliminarCommand = new RelayCommand(o => Eliminar());
        }

        private void CargarProductos()
        {
            using (var context = new AppDbContext())
            {
                var lista = context.Productos.ToList();
                Productos.Clear();
                foreach (var prod in lista)
                    Productos.Add(prod);
            }
        }

        private void Guardar()
        {
            using (var context = new AppDbContext())
            {
                if (ProductoActual.ProductoId == 0)
                    context.Productos.Add(ProductoActual);
                else
                    context.Entry(ProductoActual).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            Console.WriteLine($"Propiedad: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                        }
                    }
                    throw;
                }
            }

            CargarProductos();
            Nuevo();
        }

        private void Nuevo()
        {
            ProductoActual = new Producto();
        }

        private void Eliminar()
        {
            if (ProductoActual.ProductoId == 0) return;

            using (var context = new AppDbContext())
            {
                var producto = context.Productos.Find(ProductoActual.ProductoId);
                if (producto != null)
                {
                    context.Productos.Remove(producto);
                    context.SaveChanges();
                }
            }

            CargarProductos();
            Nuevo();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
