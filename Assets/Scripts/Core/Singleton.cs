using GGJ.Characters;

namespace GGJ.Core
{

    public class Singleton<T> where T : new()
    {

        #region Internals
        private static T _instance = default(T);

        #endregion

        #region Exposed 
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        #endregion
    }

}