using System;
using System.Windows.Input;

namespace DentalClinic.UI
{
    class CommandHelper : ICommand
    {
        /// <summary>
        /// Akcja wykonywana przy naciśnięciu przycisku
        /// </summary>
        Action executeMethod;

        /// <summary>
        /// Funkcją sprawdzająca czy można wywołać metodę przycisku np. MyCommand(jakaAkcja,wiek>0)
        /// </summary>
        Func<bool> canExcuteMethod;
        bool canexecute = true;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="executeMehtod">Metoda wywoływana</param>
        public CommandHelper(Action executeMehtod)
        {
            this.executeMethod = executeMehtod;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="executeMethod">Metoda wywoływana</param>
        /// <param name="canExecuteMethod">Funkcją sprawdzająca czy można wywołać metodę przycisku np. MyCommand(jakaAkcja,wiek>0)</param>
        public CommandHelper(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExcuteMethod = canExecuteMethod;
        }

        #region Implement ICommand

        /// <summary>
        /// Metoda określająca czy można wykonać akcję.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (canExcuteMethod != null)
            {
                bool _result = canExcuteMethod();
                if (canexecute != _result)
                {
                    canexecute = _result;
                    EventHandler handler = CanExecuteChanged;
                    if (handler != null)
                    {
                        handler(parameter, EventArgs.Empty);
                    }
                }
            }

            return canexecute;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Metoda wywołująca daną akcję.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            executeMethod();
            if (canExcuteMethod != null)
            {
                CanExecuteChanged(parameter, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Zmiana wartości CanExecute.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}
