using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private readonly HttpClient _httpClient = new HttpClient();
    private readonly List<string> _bagString = new List<string>();

    // only use async void on EVENT DELEGATE method such as Button Click
    private async void btnSameThread_Click(object sender, RoutedEventArgs e)
    {
        _bagString.Clear();
        _bagString.Add($"Step 01 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        var text = await Do();
        _bagString.Add($"Step 04 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        txtText.Text = DateTime.Now.ToString();
        txtText.Text += Environment.NewLine + text;
        foreach (var s in _bagString)
        {
            txtText.Text += Environment.NewLine + s;
        }
    }

    public async Task<string> Do(bool configureAwait = true)
    {
        _bagString.Add($"Step 02 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        var content = await _httpClient.GetStringAsync("https://www.google.com")
            // ConfigureAwait(false) not so important of WebApp but important for WPF when building Library as we don't want to run the task on UI Thread
            .ConfigureAwait(configureAwait);
        _bagString.Add($"Step 03 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        return "Hello Word";
    }

    private async void btnDifferentThread_Click(object sender, RoutedEventArgs e)
    {
        _bagString.Clear();
        _bagString.Add($"Step 01 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        var text = await Do(false);
        _bagString.Add($"Step 04 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
        txtText.Text = DateTime.Now.ToString()  + " Configure Await FALSE";
        txtText.Text += Environment.NewLine + text;
        foreach (var s in _bagString)
        {
            txtText.Text += Environment.NewLine + s;
        }
    }

    private async void btnThrowException_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            txtText.Text = "System.InvalidOperationException: 'The calling thread cannot access this object because a different thread owns it.'";
            _bagString.Clear();
            _bagString.Add($"Step 01 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
            var text = await Do().ConfigureAwait(false);
            _bagString.Add($"Step 04 => Thread Id {Thread.CurrentThread.ManagedThreadId}");
            // accessing txtText need to be same Thread as UI Thread
            txtText.Text = DateTime.Now.ToString();
            txtText.Text += Environment.NewLine + text;
            foreach (var s in _bagString)
            {
                txtText.Text += Environment.NewLine + s;
            }
        }
        catch (Exception ex)
        {
            txtText.Text += Environment.NewLine + ex.Message;
        }
    }
}