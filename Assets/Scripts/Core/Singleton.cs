using GGJ.Character.ICharacter;

namespace GGJ.Core
{

    public class Singleton<T>
    {

        #region Internals
        private static T instance = null;

        private T()
        {
            InitializationSingleton();
        }

        #endregion

        #region Exposed 
        public static readonly T Instance
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

        public virtual InitializationSingleton()
        {
        }

        #endregion
    }

}