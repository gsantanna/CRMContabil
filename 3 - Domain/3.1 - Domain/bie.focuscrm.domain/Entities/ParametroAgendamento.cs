namespace bie.focuscrm.domain.Entities
{
    public class ParametroAgendamento
    {
        //Empresa
        public int id_empresa { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int id_setor { get; set; }
        public virtual Setor Setor { get; set; }

        public int Intervalo { get; set; }

    }
}