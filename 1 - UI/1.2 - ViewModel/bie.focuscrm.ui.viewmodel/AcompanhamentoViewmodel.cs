using bie.focuscrm.domain.Entities.StoredProcedures;
using System.Collections.Generic;
using System.Linq;


namespace bie.focuscrm.ui.viewmodel
{
    public class AcompanhamentoViewmodel
    {
        public List<AcompanhamentoEmpresa> Empresas { get; set; }

        public AcompanhamentoViewmodel()
        {
            Empresas = new List<AcompanhamentoEmpresa>();
            Acomp = new List<AcompanhamentoAtendimentoMacro>();
        }

        public class AcompanhamentoEmpresa
        {
            public string Nome { get; set; }
            public List<AcompanhamentoRazao> Razoes { get; set; }
            public int id_ano { get; set; }



            public AcompanhamentoEmpresa()
            {
                Razoes = new List<AcompanhamentoRazao>();
            }
        }

        public class AcompanhamentoRazao
        {
            public string Nome { get; set; }

            public List<AcompanhamentoSetor> Setores { get; set; }

            public AcompanhamentoRazao()
            {
                Setores = new List<AcompanhamentoSetor>();
            }
        }

        public class AcompanhamentoSetor
        {
            public string Jan { get; set; }
            public string Fev { get; set; }
            public string Mar { get; set; }
            public string Abr { get; set; }
            public string Mai { get; set; }
            public string Jun { get; set; }
            public string Ago { get; set; }
            public string Set { get; set; }
            public string Nov { get; set; }
            public string Dez { get; set; }
            public string Detalhes { get; set; }


        }


        public List<AcompanhamentoAtendimentoMacro> Acomp { get; set; }

        public List<AcompanhamentoAtendimentoDetalhe> AcompDetalhe { get; set; }

        public string GetDetalhePeriodo(int id_empresa, int id_setor, int id_mes)
        {

            var items = AcompDetalhe.Where(x => x.mes == id_mes && x.id_empresa == id_empresa && x.id_setor == id_setor);

            string strSaida = "";

            foreach (var item in items)
            {
                strSaida += $"<p><a href='/Atendimentos/ATA/?id_atendimento={item.id_atendimento}' target='_blank'>{item.DataAgendada.ToString("dd/MM")}</a></p>";
            }

            return strSaida;

        }

        public string GetTotalEmpresa(int id_empresa)
        {
            string strSaida = "";
            var items = AcompDetalhe.Where(x => x.id_empresa == id_empresa);
            foreach (var item in items)
            {
                strSaida += $"<a href='/atendimentos/ata?id_atendimento={item.id_atendimento}'>{item.DataAgendada:dd/MM}</a><br/>";
            }

            return strSaida;

        }

        public string GetDetalheEmpresa(int id_empresa)
        {
            string strSaida = "";

            var AcompEmp = Acomp.Where(x => x.id_empresa == id_empresa);

            foreach (var item in AcompEmp.Where(x => !x.intervalo_ultima_visita.HasValue))
            {
                strSaida += $"<p>{item.nome_setor} --> Não possui atendimento cadastrado</p>";
            }
            
            foreach (var item in AcompEmp.Where(ac => ac.intervalo_ultima_visita.HasValue && ac.intervalo_ultima_visita.Value > ac.intervalo_parametro))
            {
                strSaida += $"<p>{item.nome_setor} --> A ultima visita foi á {item.intervalo_ultima_visita} e o exigido é {item.intervalo_parametro}</p>";
            }

            return strSaida;


        }




    }



}
