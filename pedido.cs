namespace SistemaCadeteria
{
    public class Pedido
    {
        public int Nro { get; set; }
        public string Obs { get; set; }
        public Cliente Cliente { get; set; }
        public string Estado { get; set; }
        
        public int idCadete { get; set; }//referencia a cadete 

        public Pedido(int nro, string obs, Cliente cliente, string estado = "pendiente")
        {
            Nro = nro;
            Obs = obs;
            idCadete=-999;
            Cliente = cliente;
            Estado = estado;

        }
        public void CambiarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}
