using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlazorBird.Client.Models;

public class GameManager : INotifyPropertyChanged
{
    private readonly int _gravity = 2;
    public BirdModel Bird { get; set; } = new ();
    public bool IsRunning { get; set; } = false;

    public async void MainLoop()
    {
        IsRunning = true;
        
        while (IsRunning)
        {
           Bird.Fall(_gravity);
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));

           if (Bird.DistanceFromGround <= 0)
           {
               GameOver();
           }
           
           await Task.Delay(20);
        }
    }

    public void StartGame()
    {
        if (IsRunning) return;
        Bird = new();
        MainLoop();
    }

    public void GameOver()
    {
        IsRunning = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}