
namespace RPG.Utils
{
    public class LazyVar<T>
    {
        T _value;
        bool _isInitialized = false;

        public delegate T Initializer();
        Initializer _initializer;

        public LazyVar(Initializer initializer)
        {
            _initializer = initializer;
        }

        public T value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _isInitialized = true;
            }
        }

        public void Init()
        {
            if(!_isInitialized)
            {
                _value = _initializer();
                _isInitialized = true;
            }
        }
    }
}
