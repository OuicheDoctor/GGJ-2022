using GGJ.Character.ICharacter;

namespace GGJ.Core
{

    public class Singleton<T>
    {
        private static T instance = null;

        private T()
        {
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }

}