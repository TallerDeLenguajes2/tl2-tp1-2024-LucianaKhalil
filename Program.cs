using System;
using SistemaCadeteria;
using System.Collections.Generic;
using System.Linq; // Importar LINQ para usar FirstOrDefault

class Program
{
    static void Main(string[] args)
    {
         AccesoADatos accesoDatos = null; // Inicializamos la variable

        Console.WriteLine("Seleccione el tipo de acceso a datos:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        var opcion = Console.ReadLine();

        string rutaCadetes = "";
        string rutaCadeteria = "";


        if (opcion == "1")
        {
            rutaCadetes = @"C:/Users/Usuario/OneDrive/Documentos/2024/Taller2/tl2-tp1-2024-LucianaKhalil/archivos/cadetes.csv";
            rutaCadeteria = @"C:/Users/Usuario/OneDrive/Documentos/2024/Taller2/tl2-tp1-2024-LucianaKhalil/archivos/cadeteria.csv";
            accesoDatos = new AccesoCSV();
        }
        else if (opcion == "2")
        {
            rutaCadetes = @"C:/Users/Usuario/OneDrive/Documentos/2024/Taller2/tl2-tp1-2024-LucianaKhalil/archivos/cadetes.json";
            rutaCadeteria = @"C:/Users/Usuario/OneDrive/Documentos/2024/Taller2/tl2-tp1-2024-LucianaKhalil/archivos/cadeteria.json";
            accesoDatos = new AccesoJSON();
        }
        else
        {
            Console.WriteLine("Opción no válida.");
            return;
        }

        // Cargar datos
        List<Cadete> cadetes = accesoDatos.CargarCadetes(rutaCadetes);
        Cadeteria cadeteria = accesoDatos.CargarCadeteria(cadetes, rutaCadeteria);

        while (true)
        {
            Console.WriteLine("1. Dar de alta un pedido");
            Console.WriteLine("2. Asignar pedido a cadete");
            Console.WriteLine("3. Cambiar estado de pedido");
            Console.WriteLine("4. Reasignar pedido a otro cadete");
            Console.WriteLine("5. Mostrar informe de pedidos");
            Console.WriteLine("0. Salir");

            var opcionMenu = Console.ReadLine();

            if (opcionMenu == "0") break;

            switch (opcionMenu)
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
                    cadeteria.ObtenerInformeCadetes();
                    break;
            }
        }
    }

    static void AltaPedido(Cadeteria cadeteria)
    {
        Console.WriteLine("Ingrese el número del pedido:");
        int nroPedido = int.Parse(Console.ReadLine());

        Console.WriteLine("Ingrese la observación del pedido:");
        string observacion = Console.ReadLine();

        // Captura de datos del cliente
        Console.WriteLine("Ingrese el nombre del cliente:");
        string nombreCliente = Console.ReadLine();

        Console.WriteLine("Ingrese la dirección del cliente:");
        string direccionCliente = Console.ReadLine();

        Console.WriteLine("Ingrese el teléfono del cliente:");
        string telefonoCliente = Console.ReadLine();

        Console.WriteLine("Ingrese datos de referencia para la dirección:");
        string referenciaCliente = Console.ReadLine();

        // Crear un nuevo cliente directamente
        Cliente clienteNuevo = new Cliente(nombreCliente, direccionCliente, telefonoCliente, referenciaCliente);

        Console.WriteLine("Ingrese el estado del pedido (Ej. Pendiente, En Proceso, Entregado):");
        string estado = Console.ReadLine();

        // Crear un nuevo pedido
        Pedido nuevoPedido = new Pedido(nroPedido, observacion, clienteNuevo, estado);

        // Agregar el nuevo pedido a la lista de pedidos en Cadeteria
        cadeteria.ListadoPedidos.Add(nuevoPedido);
        Console.WriteLine($"Pedido Nro {nroPedido} creado con éxito.");
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
    bool asignado = cadeteria.AsignarCadeteAPedido(nroPedido, idCadete);

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
    bool reasignado = cadeteria.ReasignarPedido(nroPedido, idNuevoCadete);

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


