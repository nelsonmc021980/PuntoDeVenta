using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuntoDeVenta.Helpers;
using System.Windows.Input;
using PuntoDeVenta.Views;

namespace PuntoDeVenta.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _vistaActual;
        public object VistaActual
        {
            get => _vistaActual;
            set { _vistaActual = value; OnPropertyChanged(); }
        }

        public ICommand MostrarClientesCommand { get; }
        public ICommand MostrarProductosCommand { get; }
        public ICommand MostrarFacturacionCommand { get; }

        public MainViewModel()
        {
            // Vista por defecto
            VistaActual = new ClienteView();

            // Comandos de navegación
            MostrarClientesCommand = new RelayCommand(o => VistaActual = new ClienteView());
            MostrarProductosCommand = new RelayCommand(o => VistaActual = new ProductoView());
            MostrarFacturacionCommand = new RelayCommand(o => VistaActual = new FacturacionView());
        }
    }
}
