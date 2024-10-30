using C_D.Controllers;

namespace C_D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting C&D System HTTP Server...");
            var controller = new CAndDController();
            controller.StartServer();
        }
    }
}
