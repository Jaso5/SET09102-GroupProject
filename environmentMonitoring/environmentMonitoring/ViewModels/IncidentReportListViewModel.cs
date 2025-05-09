using System;
using environmentMonitoring.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

public partial class IncidentReportListViewModel
{
    private readonly IncidentReportService? _repService;
    public ObservableCollection<IncidentReportViewModel> incidentReportList { get; }
    public ObservableCollection<IncidentReportViewModel> newList { get; set; }

    public ICommand SelectIncidentReportCommand { get; }


    public IncidentReportListViewModel(IncidentReportService incidentReportService) {
        _repService = incidentReportService;
        incidentReportList = new ObservableCollection<IncidentReportViewModel>(_repService.GetIncidentReportList().Select(r => new IncidentReportViewModel(_repService, r)));
        SelectIncidentReportCommand = new AsyncRelayCommand<IncidentReportViewModel>(SelectIncidentReportAsync);
    }



    private async Task SelectIncidentReportAsync(IncidentReportViewModel incidentReport)
    {
        if (incidentReport != null) {
            try {
                await Shell.Current.GoToAsync($"{nameof(Views.IncidentReportPage)}?load={incidentReport.reportId}");
            } catch (Exception) {
                await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Back()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.HomePage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }



    public async Task ReloadList()
    {
        incidentReportList.Clear();

        newList = new ObservableCollection<IncidentReportViewModel>(_repService.GetIncidentReportList().Select(r => new IncidentReportViewModel(_repService, r)));
        
        foreach (IncidentReportViewModel report in newList)
        {
            incidentReportList.Add(report);
        }
    }


}
