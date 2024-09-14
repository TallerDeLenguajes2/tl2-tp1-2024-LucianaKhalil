using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using SistemaCadeteria;

public abstract class AccesoADatos
{
    public abstract List<Cadete> CargarCadetes(string nombreArchivo);
    public abstract Cadeteria CargarCadeteria(List<Cadete> listaCadetes, string nombreArchivo2);
    public abstract void EscribirDatos(List<Cadete> cadetes); // Añadido para escritura
}

public class AccesoCSV : AccesoADatos
{
    public override List<Cadete> CargarCadetes(string nombreArchivo)
{
    List<Cadete> cadetes = new List<Cadete>();
    using (StreamReader sr = new StreamReader(nombreArchivo))
    {
        string linea;
        while ((linea = sr.ReadLine()) != null)
        {
            string[] valores = linea.Split(',');
            if (valores.Length >= 4)
            {
                int id;
                if (int.TryParse(valores[0], out id))
                {
                    string nombre = valores[1];
                    string direccion = valores[2];
                    string telefono = valores[3];

                    Cadete cadete = new Cadete(id, nombre, direccion, telefono);
                    cadetes.Add(cadete);
                }
            }
        }
    }
    return cadetes;
}


   public override Cadeteria CargarCadeteria(List<Cadete> listaCadetes, string nombreArchivo2)
{
    Cadeteria cadeteria;
    using (StreamReader sr = new StreamReader(nombreArchivo2))
    {
        // Suponiendo formato CSV: Nombre,Telefono
        string linea = sr.ReadLine();
        string[] valores = linea.Split(',');
        if (valores.Length >= 2)
        {
            string nombre = valores[0];
            string telefono = valores[1];
            
            cadeteria = new Cadeteria(nombre, telefono);
            cadeteria.ListadoCadetes = listaCadetes; // Asignar la lista de cadetes cargada
        }
        else
        {
            throw new Exception("No se porque sino no anda");
        }
    }
    return cadeteria;
}


    public override void EscribirDatos(List<Cadete> cadetes)
    {
        // Implementar escritura de cadetes a CSV
    }
}

public class AccesoJSON : AccesoADatos
    {
        public class CadeteriaDeserialize
        {
            [JsonPropertyName("nombre")]
            public string Nombre { get; set; }

            [JsonPropertyName("telefono")]
            public string Telefono { get; set; }
        }

        public override List<Cadete> CargarCadetes(string nombreArchivo)
        {
            List<Cadete> listaDeCadetes;
            using (StreamReader strReader = new StreamReader(nombreArchivo))
            {
                string listaCadetesJson = strReader.ReadToEnd();
                listaDeCadetes = JsonSerializer.Deserialize<List<Cadete>>(listaCadetesJson);
            }
            return listaDeCadetes;
        }

        public override Cadeteria CargarCadeteria(List<Cadete> listaCadetes, string nombreArchivo2)
        {
            CadeteriaDeserialize cadeteriaDeserialize;
            using (StreamReader strReader = new StreamReader(nombreArchivo2))
            {
                string cadeteriaJSON = strReader.ReadToEnd();
                cadeteriaDeserialize = JsonSerializer.Deserialize<CadeteriaDeserialize>(cadeteriaJSON);
            }
            Cadeteria cadeteria = new Cadeteria(cadeteriaDeserialize.Nombre, cadeteriaDeserialize.Telefono);
            return cadeteria;
        }

        public override void EscribirDatos(List<Cadete> cadetes)
        {
            // Implementar escritura de cadetes a JSON
        }
    }



