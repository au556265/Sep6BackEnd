using Sep6BackEnd.DataAccess.Factory;
using Sep6BackEnd.DataAccess.Repository;

namespace Sep6BackEnd.BusinessLogic
{
    public class HelloWorldLogic
    {
        private HelloWorldRepo _helloWorldRepo;
        public HelloWorldLogic(IDataAccessFactory idaf)
        {
            _helloWorldRepo = idaf.HelloWorldRepo();
        }
        
        public string GetHelloWorld()
        {
            return _helloWorldRepo.GetHelloWorld();
        }
        
        public string GetHelloWorldWithName(string name)
        {
            return _helloWorldRepo.GetHelloWorldWithName(name);
        }
    }
}