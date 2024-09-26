using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaCadeteria
{
    public class Cadeteria
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public List<Cadete> ListadoCadetes { get; set; }
        
        public List<Pedido> ListadoPedidos { get; set; }//mover a cadeteria
        public Cadeteria(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            ListadoCadetes = new List<Cadete>();
            ListadoPedidos = new List<Pedido>(); // Inicializamos ListadoPedidos en cadeteria
        }

        public Cadete ObtenerCadetePorId(int id)
        {
            foreach(var cadete in ListadoCadetes){
                if(cadete.Id==id){
                    return cadete; //encontro el cadete :)
                }
            }
            return null;//si no ecnuentra ninguno
        }
        
        public bool AsignarCadeteAPedido(int nroPedido, int idCadete){
            var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
            var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

            if (cadete != null && pedido != null)
            {
                pedido.idCadete = idCadete; // Asigna el cadete al pedido
                pedido.Estado = "asignado"; // Cambia el estado del pedido a "asignado"
                return true;
            }
            return false; // Retorna false si no se encuentra el cadete o pedido
        }
        
         

        public bool ReasignarPedido(int nroPedido, int idNuevoCadete){
            var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

            if (pedido != null)
            {
                pedido.idCadete = idNuevoCadete; // Reasigna el pedido al nuevo cadete
                return true;
            }
            return false;
        }

        public float JornalACobrar(int idCADETE)
        {
            int pedidosEntregados = ListadoPedidos.Count(p => p.idCadete == idCADETE && p.Estado.ToLower() == "entregado");// La p es una variable de referencia que representa cada objeto de tipo Pedido en ListadoPedidos.
            float jornal = pedidosEntregados * 500;

            return jornal;
        }


        public List<string> ObtenerInformeCadetes()
        {
            List<string> informes = new List<string>();
            foreach (var cadete in ListadoCadetes)
            {
                int pedidosEntregados = ListadoPedidos.Count(p => p.idCadete == cadete.Id && p.Estado.ToLower() == "entregado");
                float jornal = pedidosEntregados * 500;
                informes.Add($"Cadete {cadete.Nombre} - Pedidos Entregados: {pedidosEntregados}, Jornal: ${jornal}");
            }
            return informes;
        }

        public double CalcularPromedioEnvios()
        {
            if (ListadoCadetes.Count == 0) return 0;

            int sumaDeEnvios = ListadoPedidos.Count;
            double promedioDeEnvios = (double)sumaDeEnvios / ListadoCadetes.Count;
            return promedioDeEnvios;
        }

        public float CalcularTotalGanadoPorTodos()
        {
            float totalGanado = 0;
            foreach (var cadete in ListadoCadetes)
            {
                int pedidosEntregados = ListadoPedidos.Count(p => p.idCadete == cadete.Id && p.Estado.ToLower() == "entregado");
                totalGanado += pedidosEntregados * 500;
            }
            return totalGanado;
        }
    }
}
