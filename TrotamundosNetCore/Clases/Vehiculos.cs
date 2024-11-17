using System.ComponentModel.DataAnnotations.Schema;

namespace TrotamundosNetCore.Clases
{
    [Table("Vehiculos")]
    public class Vehiculos
    {
        public string NombreCliente { get; set; }
        public string NombreEmpleado { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string No_serie { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Motor { get; set; }
        public string Kms { get; set; }
        public string Espejo_retrovisor { get; set; }
        public string Espejo_izquierdo { get; set; }
        public string Antena { get; set; }
        public string Tapones_ruedas { get; set; }
        public string Radio { get; set; }
        public string Encendedor { get; set; }
        public string Gato { get; set; }
        public string Herramienta { get; set; }
        public string Llanta_refaccion { get; set; }
        public string Limpiadores { get; set; }
        public string Pintura_rayada { get; set; }
        public string Cristales_rotos { get; set; }
        public string Golpes { get; set; }
        public string Tapetes { get; set; }
        public string Extintor { get; set; }
        public string Tapones_gasolina { get; set; }
        public string Calaveras_rotas { get; set; }
        public string Molduras_completas { get; set; }

        public string Espejo_retrovisor_foto { get; set; }
        public string Espejo_izquierdo_foto { get; set; }
        public string Antena_foto { get; set; }
        public string Tapones_ruedas_foto { get; set; }
        public string Radio_foto { get; set; }
        public string Encendedor_foto { get; set; }
        public string Gato_foto { get; set; }
        public string Herramienta_foto { get; set; }
        public string Llanta_refaccion_foto { get; set; }
        public string Limpiadores_foto { get; set; }
        public string Pintura_rayada_foto { get; set; }
        public string Cristales_rotos_foto { get; set; }
        public string Golpes_foto { get; set; }
        public string Tapetes_foto { get; set; }
        public string Extintor_foto { get; set; }
        public string Tapones_gasolina_foto { get; set; }
        public string Calaveras_rotas_foto { get; set; }
        public string Molduras_completas_foto { get; set; }


        public string Espejo_retrovisor_video { get; set; }
        public string Espejo_izquierdo_video { get; set; }
        public string Antena_video { get; set; }
        public string Tapones_ruedas_video { get; set; }
        public string Radio_video { get; set; }
        public string Encendedor_video { get; set; }
        public string Gato_video { get; set; }
        public string Herramienta_video { get; set; }
        public string Llanta_refaccion_video { get; set; }
        public string Limpiadores_video { get; set; }
        public string Pintura_rayada_video { get; set; }
        public string Cristales_rotos_video { get; set; }
        public string Golpes_video { get; set; }
        public string Tapetes_video { get; set; }
        public string Extintor_video { get; set; }
        public string Tapones_gasolina_video { get; set; }
        public string Calaveras_rotas_video { get; set; }
        public string Molduras_completas_video { get; set; }




        public int ID { get; set; }
    }
}
