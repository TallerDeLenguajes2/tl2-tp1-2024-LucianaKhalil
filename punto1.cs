namespace PedidosYa{

        //constructoras
        public class Cliente{
            private string nombreCliente;
            private string direccionCliente;

            private string telefonoCliente;

            private string datosReferencia;

         // Constructor
        public Cliente(string nombre, string direccion, string telefono, string referencia)
        {
            NombreCliente = nombre;
            DireccionCliente = direccion;
            TelefonoCliente = telefono;
            DatosReferencia = referencia;
        }
        //getters y setters
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string DireccionCliente { get => direccionCliente; set => direccionCliente = value; }

        public string TelefonoCliente { get => telefonoCliente; set => telefonoCliente = value; }
        public string DatosReferencia { get => datosReferencia; set => datosReferencia = value; }

    }
        public class Pedidos{
        private int nro;
        private string obs;
        private Cliente cliente;
        private string estado;

        //getters y setters
        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }

        
        public Pedidos(int numero, string observacion, Cliente cliente, string estadoInicial = "pendiente")
        {
            Nro = numero;
            Obs = observacion;
            Cliente = cliente;
            Estado = estadoInicial;
        }

        }
        public class Cadete{
            private int idCadete;
            private string nombreCadete;

            private string direccionCadete;

            private string telefonoCadete;

            private List<Pedidos>listadoPedidos;

            //constructora
               public Cadete(int Id, string nombre, string direccion, string telefono)
        {
            idCadete=Id;
            nombreCadete = nombre;
            direccionCadete = direccion;
            telefonoCadete = telefono;
            
        }
            private float JornalACobrar(){
                float Jornal=0;
                foreach(var Pedidos in listadoPedidos){
                    if(Pedidos.Estado=="entregado"){
                        Jornal+=500;
                    }
                }
                return Jornal;
            }

        }

        public class Cadeteria{
            private string nombre;
            private string telefono;
            private List<Cadete> listadoCadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }




        // Método para agregar un cadete
        public void AgregarCadete(Cadete cadete)
        {
            ListadoCadetes.Add(cadete);
        }

        // Método para remover un cadete
        public void RemoverCadete(Cadete cadete)
        {
            ListadoCadetes.Remove(cadete);
        }
    }

}

