using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bie.focuscrm.domain.Enums
{
    public enum TipoPesquisa
    {
        Satisfacao, Feedback
    }


    public enum TipoUsuario
    {
        Cliente, Setor, Focus, Administrador, Superadmin

    }


    public enum ClassificacaoAlerta
    {
        Informacao, Aviso, Urgente
    }


    public enum ClassificacaoNotificacao
    {
        Atendimento, Pendencia, Integracao, Sistema
    }


    public enum RespostaNotificacao
    {
        Sucesso, Falha
    }

    public enum TipoEntregaNotificacao
    {
        EmailTemplate, Email, SMS, NetSend
    }


    public enum StatusATA
    {
        Agendada , Atrasada , Finalizada 
    }



}
