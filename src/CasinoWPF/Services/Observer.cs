using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Services
{
    // Observer Pattern Interface - Beobachter
    public interface IObserver<T>
    {
        void Update(T data);
    }

    // Observer Pattern Interface - Beobachtbares Objekt
    public interface IObservable<T>
    {
        void AddObserver(IObserver<T> observer);
        void RemoveObserver(IObserver<T> observer);
        void NotifyObservers();
    }

    // Konkrete Implementierung eines beobachtbaren Objekts
    public abstract class Observable<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> _observers;
        protected T State { get; set; }

        protected Observable()
        {
            _observers = new List<IObserver<T>>();
        }

        public void AddObserver(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void RemoveObserver(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(State);
            }
        }
    }
}
