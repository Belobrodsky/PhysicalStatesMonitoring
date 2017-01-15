using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

using MonitorWPF.Commands;

namespace MonitorWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DelegateCommand exitCommand;

        #region Constructor

        public MainViewModel()
        {
            // Blank
        }

        #endregion

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
