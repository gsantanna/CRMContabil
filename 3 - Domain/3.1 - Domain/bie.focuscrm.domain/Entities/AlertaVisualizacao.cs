using System;

namespace bie.focuscrm.domain.Entities
{
    public class AlertaVisualizacao
    {
        public int id_visualizacao { get; set; }
        public int id_alerta { get; set; }
        public string id_usuario { get; set; }

        public DateTime data_visualizacao { get; set; }

        //backw nav 
        public virtual Alerta Alerta { get; set; }



    }
}