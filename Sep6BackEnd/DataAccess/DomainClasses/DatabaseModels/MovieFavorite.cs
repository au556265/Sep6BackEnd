namespace Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels;

public class MovieFavorite
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Favorite { get; set; }
}