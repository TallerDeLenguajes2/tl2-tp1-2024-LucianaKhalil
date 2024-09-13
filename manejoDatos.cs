using System;
using System.IO;
using System.Collections.Generic;
using SistemaCadeteria;

public abstract class AccesoADatos{
    public abstract List<Cadete> LeerDatos();//metodos abstractos
    public abstract void EscribirDatos(List<Cadete> cadetes);//metodos abstractos 

}

public class AccesoCSV : AccesoADatos{
    private string rutaArchivo;

    public AccesoCSV(string ruta)
    {
        this.rutaArchivo = ruta;
    }

    public override List<Cadete>LeerDatos()
    {    
        List<Cadete> ListadoCadetes = new List<Cadete>();

            }
        return ListadoCadetes;
    }
    public override void GuardarCadetes(List<Cadete> cadetes)
    {
              // Leemos todas las líneas del archivo CSV
        
    }




