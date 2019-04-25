namespace bie.focuscrm.domain.Entities
{
    public class AlertaAlcance
    {
        public int id_alerta { get; set; }
        public virtual Alerta Alerta { get; set; }

       // public int? id_empresa { get; set; }
        //public virtual Empresa Empresa { get; set; }

        public string id_usuario { get; set; }
        public virtual Usuario Usuario { get; set; }


        



    }
}