using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTaskManager.Models;
using System.Collections.ObjectModel;

namespace MauiTaskManager.ViewModels;

public partial class TasksViewModel : ObservableObject
{
    [ObservableProperty]
    private string newTaskTitle;

    [ObservableProperty]
    private TaskItem selectedTask;

    public ObservableCollection<TaskItem> Tasks { get; } = new();

    public int TotalCount => Tasks.Count;

    public int CompletedCount => Tasks.Count(t => t.IsCompleted);

    public TasksViewModel()
    {
        Tasks.CollectionChanged += (_, __) =>
        {
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(CompletedCount));
        };
    }

    [RelayCommand]
    private void AddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle))
            return;

        var task = new TaskItem
        {
            Title = NewTaskTitle
        };

        task.PropertyChanged += (_, __) =>
        {
            OnPropertyChanged(nameof(CompletedCount));
        };

        Tasks.Add(task);
        NewTaskTitle = string.Empty;
    }
}
