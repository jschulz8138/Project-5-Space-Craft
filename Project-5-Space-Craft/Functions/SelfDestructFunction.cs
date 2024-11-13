//PayloadOps
//Implementation of self destruct function, inheriting from IFunction
namespace Payload_Ops
{
    public class SelfDestructFunction : IFunction
    {
        private bool authorization;

        public SelfDestructFunction(bool authorization) {
            this.authorization = authorization;
        }

        public SelfDestructFunction(string authorization)
        {
            this.authorization = bool.Parse(authorization);
        }

        public string GetCommand(){
            return this.authorization.ToString();
        }

        public void RunCommand(){
            if (this.authorization){
                throw new Exception("The spaceship has blown up and crashed");
            }
        }
    }
}
