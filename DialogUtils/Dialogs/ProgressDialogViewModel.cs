﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DialogUtils.Dialogs
{
    public class ProgressDialogViewModel : ObservableRecipient
    {
        private readonly IDialogHostService _dialogHostService;
        private string _dialogIdentifier;

        private bool _isIndeterminate;
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set => SetProperty(ref _isIndeterminate, value);
        }

        private int _value;
        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _cancellable;
        public bool Cancellable
        {
            get => _cancellable;
            set => SetProperty(ref _cancellable, value);
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(CancelImpl));
        private void CancelImpl()
        {
            _dialogHostService.CloseDialog(_dialogIdentifier);

            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Cancelled;

        public ProgressDialogViewModel(IDialogHostService dialogHostService)
        {
            _dialogHostService = dialogHostService;
        }

        public void Init(string dialogIdentifier, bool isIndeterminate, bool cancellable = false)
        {
            IsIndeterminate = isIndeterminate;
            Value = 0;
            Cancellable = cancellable;
            _dialogIdentifier = dialogIdentifier;
        }
    }
}
