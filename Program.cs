using System;
using System.IO;
using PedidosYa;
class Program
{
    static void Main()
    {
        string filePath = "C:/Users/Alumno/Documents/tl2-tp1-2024-LucianaKhalil/archivos/cadetes.csv";
        var data = new List<string[]>();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                data.Add(values);
            }
        }

        // Imprimir los datos leídos
        foreach (var row in data)
        {
            Console.WriteLine(string.Join(" | ", row));
        }

        System.Console.WriteLine(data[1][1]);

        Cadete fede=new Cadete();
        

    }
}



