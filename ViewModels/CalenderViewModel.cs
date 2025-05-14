using DutydoneClient.Services;
using Plugin.Maui.Calendar.Models;

namespace DutydoneClient.ViewModels;

public class CalenderViewModel : ViewModelBase
{
	private DutyDoneAPIProxy proxy;
	public CalenderViewModel(DutyDoneAPIProxy proxy)
	{
		this.proxy = proxy;
		Events = new EventCollection();
		ReadEvents();
	}

	private EventCollection events;
    public EventCollection Events 
	{ 
		get
		{
			return events;
		}
		set
		{
			events = value;
			OnPropertyChanged();
		}
	}

	private async void ReadEvents()
	{
		EventCollection events = new EventCollection();

        List<Models.Task>? tasks = await proxy.GetTasks();
		tasks = tasks.OrderBy(t=>t.DueDayTime).ToList();
		
		if (tasks != null && tasks.Count > 0)
		{
            DateTime current = tasks[0].DueDayTime;
			List<Models.Task> currentList = new List<Models.Task>();
            events[current] = currentList;
			foreach (Models.Task task in tasks)
			{
				if (task.DueDayTime == current)
				{
					currentList.Add(task);
				}
				else
				{
					currentList = new List<Models.Task>();
					current = task.DueDayTime;
                    events[current] = currentList;
					currentList.Add(task);
				}
			}

			Events = events;
		}

	}
}
