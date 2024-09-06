using System;
using SistemaCadeteria;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var cadeteria = new Cadeteria("PedidosYa", "123456789");
        string rutaCorrecta = @"C:\Users\Usuario\OneDrive\Documentos\2024\Taller2\tl2-tp1-2024-LucianaKhalil\archivos\cadetes.csv";//el @ para que se pueda leer barrra
        cadeteria.CargarCadetesDesdeCSV(rutaCorrecta);


        while (true)
        {
            Console.WriteLine("1. Dar de alta un pedido");
            Console.WriteLine("2. Asignar pedido a cadete");
            Console.WriteLine("3. Cambiar estado de pedido");
            Console.WriteLine("4. Reasignar pedido a otro cadete");
            Console.WriteLine("5. Mostrar informe de pedidos");
            Console.WriteLine("0. Salir");

            var opcion = Console.ReadLine();

            if (opcion == "0") break;

            switch (opcion)
            {
                case "1":
                    AltaPedido(cadeteria);
                    break;
                case "2":
                    AsignarPedido(cadeteria);
                    break;
                case "3":
                    CambiarEstadoPedido(cadeteria);
                    break;
                case "4":
                    ReasignarPedido(cadeteria);
                    break;
                case "5":
                    cadeteria.MostrarInforme();
                    break;
            }
        }
    }

    static void AltaPedido(Cadeteria cadeteria)
    {
        Console.WriteLine("Ingrese el número del pedido:");
        int nro = int.Parse(Console.ReadLine());
        Console.WriteLine("Ingrese la observación del pedido:");
        string obs = Console.ReadLine();
        Console.WriteLine("Ingrese el nombre del cliente:");
        string nombreCliente = Console.ReadLine();
        Console.WriteLine("Ingrese la dirección del cliente:");
        string direccionCliente = Console.ReadLine();
        Console.WriteLine("Ingrese el teléfono del cliente:");
        string telefonoCliente = Console.ReadLine();
        Console.WriteLine("Ingrese datos de referencia para la dirección del cliente:");
        string datosReferenciaDireccion = Console.ReadLine();
        
        Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccion);
        Pedido pedido = new Pedido(nro, obs, cliente);

        Console.WriteLine("Seleccione el ID del cadete para asignar el pedido:");
        foreach (var cadete in cadeteria.ListadoCadetes)
        {
            Console.WriteLine($"{cadete.Id}. {cadete.Nombre}");
        }
        int idCadete = int.Parse(Console.ReadLine());
        cadeteria.AsignarID(pedido, idCadete);
        cadeteria.AsignarPedido(pedido);
    }

    static void AsignarPedido(Cadeteria cadeteria)
    {
        Console.WriteLine("Asignando pedido...");
    }

        static void CambiarEstadoPedido(Cadeteria cadeteria)
{
    Console.WriteLine("Seleccione el ID del cadete para cambiar el estado de un pedido:");
    foreach (var cadete in cadeteria.ListadoCadetes)
    {
        Console.WriteLine($"{cadete.Id}. {cadete.Nombre}");
    }
    int idCadete = int.Parse(Console.ReadLine());
    Cadete cadeteSeleccionado = cadeteria.ObtenerCadetePorId(idCadete);

    Console.WriteLine("Seleccione el número del pedido para cambiar su estado:");
    foreach (var pedido in cadeteria.ListadoPedidos)
    {
        Console.WriteLine($"Pedido Nro: {pedido.Nro}, Estado: {pedido.Estado}");
    }
    int nroPedido = int.Parse(Console.ReadLine());
    
    Pedido pedidoSeleccionado = null;

    foreach (var pedido in cadeteria.ListadoPedidos)
    {
        if (pedido.Nro == nroPedido)
        {
            pedidoSeleccionado = pedido;
            break; 
        }
    }

    if (pedidoSeleccionado != null)
    {
        Console.WriteLine("Ingrese el nuevo estado del pedido:");
        string nuevoEstado = Console.ReadLine();
        pedidoSeleccionado.CambiarEstado(nuevoEstado);
        Console.WriteLine($"Estado del pedido {nroPedido} cambiado a {nuevoEstado}");
    }
    else
    {
        Console.WriteLine("Pedido no encontrado.");
    }
}
    static void ReasignarPedido(Cadeteria cadeteria)
    {
        Console.WriteLine("Reasignando pedido...");
    }
}


