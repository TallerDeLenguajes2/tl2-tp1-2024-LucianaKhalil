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
                var datos = lineas[i].Split(',');
                
                // Creamos un nuevo objeto Cadete usando los datos del archivo
                var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
                
                // Agregamos el cadete a la lista de cadetes
                ListadoCadetes.Add(cadete);
            }
        }

        public Cadete ObtenerCadetePorId(int id)
        {
            return ListadoCadetes.FirstOrDefault(c => c.Id == id);
        }

        public void MostrarInforme()
        {
            float totalGanado = 0;
            foreach (var cadete in ListadoCadetes)
            {
                var jornal = cadete.JornalACobrar();
                Console.WriteLine($"Cadete {cadete.Nombre} - Pedidos Entregados: {cadete.ListadoPedidos.Count(p => p.Estado.ToLower() == "entregado")}, Jornal: ${jornal}");
                totalGanado += jornal;
            }
            Console.WriteLine($"Total Ganado por todos los cadetes: ${totalGanado}");
            Console.WriteLine($"Cantidad promedio de envíos por cadete: {ListadoCadetes.Average(c => c.ListadoPedidos.Count)}");
        }
    }
}
