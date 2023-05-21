using System.Threading.Tasks;

namespace Sep6BackEnd.BusinessLogic;

public interface IStatisticHandler
{
    Task<double> GetAverageRatingTotal(int movieId);
    Task<double> GetAverageRatingForActorsMovie(int actorId);
}