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

        public Cadeteria(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            ListadoCadetes = new List<Cadete>();
        }
        public void CargarCadetesDesdeCSV(string archivoCadetes)
        {
            // Leemos todas las líneas del archivo CSV
            var lineas = File.ReadAllLines(archivoCadetes);
            
            // Ignoramos la primera línea (el encabezado)
            for (int i = 1; i < lineas.Length; i++)
            {
                var datos = lineas[i].Split(',');//separamos en las comas en cada linea
                
                // Creamos un nuevo objeto Cadete usando los datos del archivo
                var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
                
                // Agregamos el cadete a la lista de cadetes
                ListadoCadetes.Add(cadete);
            }
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

        public void MostrarInforme()
        {
            float totalGanado = 0;
            foreach (var cadete in ListadoCadetes)
            {
                // Contador de pedidos entregados
                    int pedidosEntregados = 0;
                    foreach (var pedido in cadete.ListadoPedidos)
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
                sumaDeEnvios += cadete.ListadoPedidos.Count;
            }
            double promedioDeEnvios = (double)sumaDeEnvios / ListadoCadetes.Count;

            Console.WriteLine("Cantidad promedio de envíos por cadete: " + promedioDeEnvios);

        }
    }
}
