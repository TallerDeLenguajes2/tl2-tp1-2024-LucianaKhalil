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
        public void AsignarPedido(Pedido pedido)
        {
            ListadoPedidos.Add(pedido);
        }
        
        public bool AsignarCadeteAPedido(Pedido pedido, int idCADETE){//
            Cadete cadete = ObtenerCadetePorId(idCADETE); // Buscar el cadete por su ID
            if (cadete != null && ListadoPedidos.Contains(pedido))
            {
                pedido.idCadete = idCADETE; // Asignamos el cadete al pedido
                return true; // Asignación exitosa
            }
            return false; // Error en la asignación (cadete o pedido no encontrado)pedido.idCadete=idCADETE;
        }

        public bool ReasignarPedido(Pedido pedido, int idNuevoCadete)
{
            // Verificamos si el pedido existe en la lista de pedidos
            if (ListadoPedidos.Contains(pedido))
            {
                // Asignamos el nuevo ID del cadete al pedido
                pedido.idCadete = idNuevoCadete;
                return true;
            }
            else
            {
                return false;
            }
}

        public float JornalACobrar(int idCADETE)
        {
            int pedidosEntregados = ListadoPedidos.Count(p => p.idCadete == idCADETE && p.Estado.ToLower() == "entregado");// La p es una variable de referencia que representa cada objeto de tipo Pedido en ListadoPedidos.
            float jornal = pedidosEntregados * 500;

            return jornal;
        }


        public void MostrarInforme()
        {
            float totalGanado = 0;
            foreach (var cadete in ListadoCadetes)
            {
                // Contador de pedidos entregados
                    int pedidosEntregados = 0;
                    foreach (var pedido in ListadoPedidos)
                    {
                        if (pedido.Estado.ToLower() == "entregado")
                        {
                            pedidosEntregados++;
                        }
                    }

                // Calcular el jornal a cobrar
                var jornal = pedidosEntregados * 500;
                Console.WriteLine($"Cadete {cadete.Nombre} - Pedidos Entregados: {pedidosEntregados}, Jornal: ${jornal}");
                totalGanado += jornal;
            }
            Console.WriteLine($"Total Ganado por todos los cadetes: ${totalGanado}");
            int sumaDeEnvios = 0;
            foreach (Cadete cadete in ListadoCadetes)//se que esto se puede hacer mas corto pero no entiendo como
            {
                sumaDeEnvios += ListadoPedidos.Count;
            }
            double promedioDeEnvios = (double)sumaDeEnvios / ListadoCadetes.Count;

            Console.WriteLine("Cantidad promedio de envíos por cadete: " + promedioDeEnvios);

        }
    }
}
