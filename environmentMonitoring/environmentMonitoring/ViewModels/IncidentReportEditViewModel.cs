using System;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

public class IncidentReportEditViewModel: ObservableObject, IQueryAttributable
{
    private IncidentReports _incidentReport;

    public int? sensorId
    {
        get => _incidentReport.r_sensor_Id;
        set
        {
            if(_incidentReport.r_sensor_Id != value)
            {
                _incidentReport.r_sensor_Id = value;
                OnPropertyChanged();
            }
        }
    }
    

    public DateTime? reportDateTime
    {
        get => _incidentReport.reportDate;
        set
        {
            if(_incidentReport.reportDate == null)
            {
                _incidentReport.reportDate = value;
                OnPropertyChanged();
            }
        }
    }

    public string incidentType
    {
        get => _incidentReport.type;
        set
        {
            if(_incidentReport.type != value)
            {
                _incidentReport.type = value;
                OnPropertyChanged();
            }
        }
    }

    public string incidentStatus
    {
        get => _incidentReport.status;
        set
        {
            if(_incidentReport.status != value)
            {
                _incidentReport.status = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime? reportLastUpdateDateTime
    {
        get => _incidentReport.lastUpdatedDate;
        set
        {
            if(_incidentReport.lastUpdatedDate != value)
            {
                _incidentReport.lastUpdatedDate = value;
                OnPropertyChanged();
            }
        }
    }

    public string? incidentDescription
    {
        get => _incidentReport.description;
        set
        {
            if(_incidentReport.description != value)
            {
                _incidentReport.description = value;
                OnPropertyChanged();
            }
        }
    }

    public string? incidentNextSteps
    {
        get => _incidentReport.next_steps;
        set
        {
            if(_incidentReport.next_steps != value)
            {
                _incidentReport.next_steps = value;
                OnPropertyChanged();
            }
        }
    }

    public string? incidentResolution
    {
        get => _incidentReport.resolution;
        set
        {
            if(_incidentReport.resolution != value)
            {
                _incidentReport.resolution = value;
                OnPropertyChanged();
            }
        }
    }


    private readonly IncidentReportService? _repService;

    public IncidentReportEditViewModel(IncidentReportService incidentReportService) {
        _repService = incidentReportService;
        _incidentReport = new IncidentReports();
    }
/*
    public IncidentReportEditViewModel(IncidentReportService incidentReportService, IncidentReports incidentReport) {
        _repService = incidentReportService;
        _incidentReport = incidentReport;
    }
*/

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {

        if (query.ContainsKey("new"))
        {
            _incidentReport.r_sensor_Id = int.Parse(query["new"].ToString());
            _incidentReport.reportDate = DateTime.Now;
        }

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

     /*! RefreshProperties method refreshes the properties of the incident report to ensure they are up to date
    *  
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
