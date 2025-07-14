using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MixItUp.Annotations;
using Xamarin.Forms;

namespace MixItUp.Model
{
    public class ProductCategory : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private TextDecorations _textDecorationStyle;

        public string Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public TextDecorations TextDecorationStyle
        {
            get => _textDecorationStyle;
            set
            {
                if (value == _textDecorationStyle) return;
                _textDecorationStyle = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
