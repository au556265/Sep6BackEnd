using Sep6BackEnd.DataAccess.Factory;

namespace Sep6BackEnd.BusinessLogic
{
    public class HandlerFactory
    {
        public HelloWorldLogic _helloWorldlogic { get; set; }
        private IDataAccessFactory _iDataAccessFactory { get; set; }

        public HandlerFactory()
        {
            _iDataAccessFactory = new DataAccessFactory();
            
            _helloWorldlogic = new HelloWorldLogic(_iDataAccessFactory);
        }
        
        
        public HelloWorldLogic HelloWorldLogic()
        {
            //google "Lazy instantion" til rapport 123
            if (_helloWorldlogic is null)
                _helloWorldlogic = new HelloWorldLogic(_iDataAccessFactory);

            return _helloWorldlogic;
        }
        
        
    }
}