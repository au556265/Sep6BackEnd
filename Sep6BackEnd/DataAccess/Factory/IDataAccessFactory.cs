using Sep6BackEnd.DataAccess.Repository;

namespace Sep6BackEnd.DataAccess.Factory
{
    public interface IDataAccessFactory
    {
        HelloWorldRepo HelloWorldRepo();
    }
}