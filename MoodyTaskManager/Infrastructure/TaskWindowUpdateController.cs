using MoodyTaskManager.Contract;
using System;
using System.Windows.Threading;

namespace MoodyTaskManager.Infrastructure
{
    public class TaskWindowUpdateController : IIntervallTriggeredUpdateController
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private IUpdateItem _updateItem;

        public void RegisterTimer(TimeSpan timeSpan)
        {
            _timer.Interval = timeSpan;
            _timer.IsEnabled = true;
            _timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.IsEnabled = false;
                await _updateItem.Update();
            }
            catch(Exception ex)
            {
                // TODO  add exception handling
            }
            finally
            {
                _timer.IsEnabled = true;
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void RegisterUpdateItem(IUpdateItem updateItem)
        {
            _updateItem = updateItem;
        }
    }
}
