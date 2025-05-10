using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Services;

/*! IncidentReportViewModel handles the logic for displaying the report to the user
     *  
     *  
     */

namespace environmentMonitoring.ViewModels;

public partial class IncidentReportViewModel:ObservableObject, IQueryAttributable
{
    private IncidentReports _incidentReport;
    private readonly IncidentReportService? _repService;

    public IncidentReportViewModel(IncidentReportService incidentReportService) {
        _repService = incidentReportService;
        _incidentReport = new IncidentReports();
    }
    public IncidentReportViewModel(IncidentReportService incidentReportService, IncidentReports incidentReport) {
        _repService = incidentReportService;
        _incidentReport = incidentReport;
    }

    public int? reportId => _incidentReport.incident_Id;
    public int? sensorId => _incidentReport.r_sensor_Id;
    public DateTime? reportDateTime => _incidentReport.reportDate;
    public DateTime? reportLastUpdateDateTime => _incidentReport.lastUpdatedDate;
    public string? incidentDescription => _incidentReport.description;
    public string? incidentStatus => _incidentReport.status;
    public string? incidentType => _incidentReport.type;
    public string? incidentNextSteps => _incidentReport.next_steps;
    public string? incidentResolution => _incidentReport.resolution;

    /*! EditIncidentReport method navigated the user to the IncidentReportEditPage
     *  Passes report ID as query parameter 
     *  Displays error message if navigation fails after saving a report
     */
    [RelayCommand]
    private async Task EditIncidentReport() 
    {
        try {
            await Shell.Current.GoToAsync($"{nameof(Views.IncidentReportEditPage)}?load={_incidentReport.incident_Id}");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }

    /*! SaveIncidentReport method deletes the current report 
     *  Displays error message if theres an error when trying to delete the report
     *  Displays error message if navigation fails after deleting report a report
     */
    [RelayCommand]
    private async Task DeleteIncidentReport() 
    {
        bool confirmation = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this report?", "Yes", "No");
        if (!confirmation) return;

        try {
            _repService.DeleteIncidentReport(_incidentReport);
            await Shell.Current.DisplayAlert("Successful", "Report Deleted", "OK");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "An Error occurred while attempting to delete the report", "OK");
        }

        try {
            await Shell.Current.GoToAsync("..");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Report deleted, error during navigation", "OK");
        }
    }


    /*! IQueryAttributable.ApplyQueryAttributes method retrieves the role from the database using an ID passed in the query
     *  If no ID is passed in the query, a new role is created
     *  @param query The query dictionary containing the ID of the role to be retrieved
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            try {
                _incidentReport = _repService.GetIncidentReportById(int.Parse(query["load"].ToString()));
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "Unable to retrieve the report.", "OK");
                return;
            }
        }
        RefreshProperties();
    }

    /*! RefreshProperties refreshes the information in the report when called 
     */
     private void RefreshProperties()
    {
        OnPropertyChanged(nameof(sensorId));
        OnPropertyChanged(nameof(reportDateTime));
        OnPropertyChanged(nameof(incidentType));
        OnPropertyChanged(nameof(incidentStatus));
        OnPropertyChanged(nameof(reportLastUpdateDateTime));
        OnPropertyChanged(nameof(incidentDescription));
        OnPropertyChanged(nameof(incidentNextSteps));
        OnPropertyChanged(nameof(incidentResolution));
    }

}
