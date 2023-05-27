using System.Threading.Tasks;

namespace Sep6BackEnd.BusinessLogic.Logic;

public interface IStatisticHandler
{
    Task<double> GetAverageRatingTotal(int movieId);
    Task<double> GetAverageRatingForActorsMovie(int actorId);
}