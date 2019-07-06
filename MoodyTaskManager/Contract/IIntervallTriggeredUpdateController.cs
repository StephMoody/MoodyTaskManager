using System;

namespace MoodyTaskManager.Contract
{
    interface IIntervallTriggeredUpdateController
    {
        void RegisterTimer(TimeSpan timeSpan);

        void RegisterUpdateItem(IUpdateItem updateItem);

        void Start();

        void Stop();
    }
}
