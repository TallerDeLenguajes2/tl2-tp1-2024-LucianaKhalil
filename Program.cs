using System;
using SistemaCadeteria;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        AccesoADatos accesoDatos;

        Console.WriteLine("Seleccione el tipo de acceso a datos:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        var opcion = Console.ReadLine();

        if (opcion == "1")
        {
            string rutaCSV = @"../cadcsv";
            accesoDatos = new AccesoCSV(rutaCSV);
        }
        else if (opcion == "2")
        {
            string rutaJSON = @"ruta_del_archivo_json";
            accesoDatos = new AccesoJSON(rutaJSON);
        }
        else
        {
            Console.WriteLine("Opción no válida.");
            return;
        }

        var cadetes = accesoDatos.CargarCadetes();


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
        cadeteria.AsignarPedido(pedido);
        cadeteria.AsignarCadeteAPedido(pedido, idCadete);
        
    }

static void AsignarPedido(Cadeteria cadeteria)
{
    // Mostrar todos los pedidos sin asignar cadete
    Console.WriteLine("Seleccione el número del pedido para asignar un cadete:");
    foreach (var pedido in cadeteria.ListadoPedidos)
    {
        if (pedido.idCadete == 0) // Asumiendo que un pedido sin cadete tiene idCadete = 0
        {
            Console.WriteLine($"Pedido Nro: {pedido.Nro}, Observación: {pedido.Obs}");
        }
    }
    int nroPedido = int.Parse(Console.ReadLine());

    // Buscar el pedido seleccionado
    Pedido pedidoSeleccionado = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

    if (pedidoSeleccionado == null)
    {
        Console.WriteLine("Pedido no encontrado.");
        return;
    }

    // Mostrar lista de cadetes
    Console.WriteLine("Seleccione el ID del cadete para asignar al pedido:");
    foreach (var cadete in cadeteria.ListadoCadetes)
    {
        Console.WriteLine($"{cadete.Id}. {cadete.Nombre}");
    }
    int idCadete = int.Parse(Console.ReadLine());

    // Intentar asignar el cadete al pedido
    bool asignado = cadeteria.AsignarCadeteAPedido(pedidoSeleccionado, idCadete);

    if (asignado)
    {
        Console.WriteLine("Cadete asignado correctamente al pedido.");
    }
    else
    {
        Console.WriteLine("Error al asignar el cadete al pedido.");
    }
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
    // Mostrar todos los pedidos con cadete asignado
    Console.WriteLine("Seleccione el número del pedido que desea reasignar:");
    foreach (var pedido in cadeteria.ListadoPedidos)
    {
        if (pedido.idCadete != 0) // Mostrar solo pedidos que ya tienen cadete
        {
            Console.WriteLine($"Pedido Nro: {pedido.Nro}, Estado: {pedido.Estado}, Cadete Asignado: {pedido.idCadete}");
        }
    }
    int nroPedido = int.Parse(Console.ReadLine());

    // Buscar el pedido seleccionado
    Pedido pedidoSeleccionado = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

    if (pedidoSeleccionado == null)
    {
        Console.WriteLine("Pedido no encontrado.");
        return;
    }

    // Mostrar lista de cadetes disponibles
    Console.WriteLine("Seleccione el ID del nuevo cadete para asignar al pedido:");
    foreach (var cadete in cadeteria.ListadoCadetes)
    {
        if (cadete.Id != pedidoSeleccionado.idCadete) // Excluir al cadete actualmente asignado
        {
            Console.WriteLine($"{cadete.Id}. {cadete.Nombre}");
        }
    }
    int idNuevoCadete = int.Parse(Console.ReadLine());

    // Llamar a la función reasignar dd cadeteria.cs
    bool reasignado = cadeteria.ReasignarPedido(pedidoSeleccionado, idNuevoCadete);

    if (reasignado)
    {
        Console.WriteLine("Pedido reasignado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al reasignar el pedido.");
    }
}


}


