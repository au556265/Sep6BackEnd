using Sep6BackEnd.DataAccess.Repository;

namespace Sep6BackEnd.DataAccess.Factory
{
    public class DataAccessFactory : IDataAccessFactory
    {
        private HelloWorldRepo _HelloWorldRepo { get; set; }

        public HelloWorldRepo HelloWorldRepo()
        {
            //google "Lazy instantion" til rapport
            if (_HelloWorldRepo is null)
                _HelloWorldRepo = new HelloWorldRepo("connectionString");

            return _HelloWorldRepo;
        }
    }
}