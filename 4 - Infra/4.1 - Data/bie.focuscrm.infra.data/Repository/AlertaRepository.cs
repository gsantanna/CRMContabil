using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class AlertaRepository : RepositoryBase<Alerta>, IAlertaRepository
    {

        public new void Remove(Alerta obj)
        {

            Db.Set<AlertaAlcance>().RemoveRange(obj.Alcances);
            Db.Set<AlertaVisualizacao>().RemoveRange(obj.Visualizacoes);
            Db.Set<Alerta>().Remove(obj);
            Db.SaveChanges();

        }

    }
}