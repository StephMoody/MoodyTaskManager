using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Monitoring.Contract;
using System.Windows;
using MoodyTaskManager.Contract;

namespace MoodyTaskManager.ViewModel
{
    public abstract class TaskViewModelBase : ViewModelBase
    {
        private IProcessData _selectedProcess;

        internal IProcesDataProvider ProcessInfoProvider { get; set; }

        public ObservableCollection<IProcessData> ProcessData { get; } = new ObservableCollection<IProcessData>();

        public RelayCommand KillSelectedProcessCommand => new RelayCommand(KillSelectedProcess);

        public RelayCommand ToggleFilterCommand => new RelayCommand(ToggleFilter);

        protected IFilteredTasksHost FilteredTasksHost { get; set; }

        public bool IsVisible => ProcessData.Any();

        public abstract string Designation { get; }

        public IProcessData SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged();
            }
        }

        private void KillSelectedProcess()
        {
            try
            {
                _selectedProcess?.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToggleFilter()
        {
            try
            {
                if (SelectedProcess == null)
                    return;

                if (!FilteredTasksHost.IsProcessFiltered(SelectedProcess.Name))
                    FilteredTasksHost.FilterProcess(SelectedProcess.Name);
                else
                    FilteredTasksHost.UnfilterProcess(SelectedProcess.Name);
            }
            catch (Exception)
            {
                // Add Exception Handling
            }
        }

        protected virtual void Refresh()
        {
            OnPropertyChanged(nameof(ProcessData));
            OnPropertyChanged(nameof(IsVisible));
        }
    }
}