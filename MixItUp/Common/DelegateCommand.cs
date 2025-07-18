﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MixItUp.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private Action<int> getStudentProfile;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute,
                      Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(object onUnfriendAsync)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
