using System;
using environmentMonitoring.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

/*! IncidentReportListViewModel 
     * Handles the logic for displaying the list of reports to the user
     *  Keeps the list up to date, and allows the user to select a specific report
     */

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


    /*! SelectIncidentReportAsync allows the user to select an incident report
     * User is then navigated to the incident report for viewing
     *  @param Takes an IncidentReportViewModel as a parameter
     */
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

    /*! ReloadList reloads the list of incident reports from the database o page appearing
     *  The command is bound to the on appearing EventToCommandBehavior on the IncidentReportListPage.xaml
     */
    [RelayCommand]
    private async Task ReloadList()
    {
        incidentReportList.Clear();

        newList = new ObservableCollection<IncidentReportViewModel>(_repService.GetIncidentReportList().Select(r => new IncidentReportViewModel(_repService, r)));
        
        foreach (IncidentReportViewModel report in newList)
        {
            incidentReportList.Add(report);
        }
    }


}
