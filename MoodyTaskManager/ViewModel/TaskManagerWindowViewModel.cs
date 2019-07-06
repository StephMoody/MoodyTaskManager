namespace MoodyTaskManager.ViewModel
{
    public class TaskManagerWindowViewModel : ViewModelBase
    {
        public TaskManagerWindowViewModel(AllTasksViewModel allCurrentTaskViewModel, FilteredTasksViewModel filteredTasksViewModel)
        {
            AllCurrentTaskViewModel = allCurrentTaskViewModel;
            FilteredTasks = filteredTasksViewModel;
        }

        public AllTasksViewModel AllCurrentTaskViewModel { get; }

        public FilteredTasksViewModel FilteredTasks { get; }

    }
}
