namespace Sep6BackEnd.DataAccess.Repository
{
    public class HelloWorldRepo
    {
        private string _connection;

        public HelloWorldRepo(string connection)
        {
            _connection = connection;
        }

        public string GetHelloWorld()
        {
            return "Hello World";
        }
        
        public string GetHelloWorldWithName(string name)
        {
            return $"Hello World, {name}";
        }

    }
}