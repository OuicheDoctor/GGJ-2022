using GGJ.Character.ICharacter;

namespace GGJ.Core
{

    public class Logger : Singleton<Logger>
    {
        #region Exposed 
        public void Error(string message)
        {
            System.Console.WriteLine(message);
        }
        #endregion
    }

}