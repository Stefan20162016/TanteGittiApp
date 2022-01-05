using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;



public delegate void MyEventHandler();

namespace TanteGittiApp
{

    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            ButtonCommand = new Command(RunButton);
        }
        private void RunButton()
        {
            Debug.WriteLine("RUNBUTTON");
            
                if (TestEvent != null)
                {
                    TestEvent.Invoke();
                }
                NotifyPropertyChanged();
            
        }

        public ICommand ButtonCommand { get; set; }

        public event MyEventHandler TestEvent;

        private string content;

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (content != value)
                {
                    content = value;
                    if (TestEvent != null)
                    {
                        TestEvent.Invoke();
                    }
                    NotifyPropertyChanged();
                }
            }
        }
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
