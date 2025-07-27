using PuntoDeVenta.Helpers;
using PuntoDeVenta.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PuntoDeVenta.ViewModels
{
    public class ClientesViewModel : INotifyPropertyChanged
    {
        private AppDbContext _context;

        public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();

        private Cliente _clienteSeleccionado;
        public Cliente ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set
            {
                _clienteSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public ICommand NuevoCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }

        public ClientesViewModel()
        {
            _context = new AppDbContext();

            NuevoCommand = new RelayCommand(NuevoCliente);
            GuardarCommand = new RelayCommand(GuardarCliente, PuedeGuardar);
            EliminarCommand = new RelayCommand(EliminarCliente, PuedeEliminar);

            CargarClientes();
        }

        private void CargarClientes()
        {
            Clientes.Clear();
            foreach (var c in _context.Clientes.ToList())
                Clientes.Add(c);
        }

        private void NuevoCliente()
        {
            ClienteSeleccionado = new Cliente();
        }

        private void GuardarCliente()
        {
            if (ClienteSeleccionado.ClienteId == 0)
            {
                _context.Clientes.Add(ClienteSeleccionado);
            }
            else
            {
                _context.Entry(ClienteSeleccionado).State = EntityState.Modified;
            }

            _context.SaveChanges();
            CargarClientes();
        }

        private void EliminarCliente()
        {
            if (ClienteSeleccionado != null)
            {
                _context.Clientes.Remove(ClienteSeleccionado);
                _context.SaveChanges();
                CargarClientes();
                ClienteSeleccionado = null;
            }
        }

        private bool PuedeGuardar() => ClienteSeleccionado != null;
        private bool PuedeEliminar() => ClienteSeleccionado != null && ClienteSeleccionado.ClienteId > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
