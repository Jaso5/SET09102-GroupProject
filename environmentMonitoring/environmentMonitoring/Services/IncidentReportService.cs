using System;
using environmentMonitoring.Database;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace environmentMonitoring.Services;

public class IncidentReportService
{
    private EnvironmentAppDbContext _context;

    public IncidentReportService(EnvironmentAppDbContext context) {
        _context = context;
    }
     
     
     /*! CreateIncidentReport method adds a new incident report to the database
     *  @param Takes an incident report as a parameter 
     *  @throws Exception if there is an error when attempting to add it to the database
     */
    public void CreateIncidentReport(IncidentReports incidentReports)
    {
        try {
            _context.IncidentReports.Add(incidentReports);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error creating Incident report");
        }
    }

    /*! GetIncidentReportById method retrieves an incident report from the database by it's ID
     *  @param Takes an id as a parameter 
     *  @throws Exception if there is an issue when attempting to retrieve it from the database
     *  @return Returns the incident report to the user
     */
    public IncidentReports GetIncidentReportById(int reportId)
    {
        try {
            return  _context.IncidentReports.Single(r => r.incident_Id == reportId);
        } catch (Exception) {
            throw new Exception("Error retrieving report");
        }
    }

    /*! UpdateIncidentReport method updates an incident report in the database
     *  @param Takes an incident report as a parameter 
     *  @throws Exception if there is an issue when trying to update the incident report
     */
    public void UpdateIncidentReport(IncidentReports incidentReports)
    {
        try {
            _context.IncidentReports.Update(incidentReports);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error updating report");
        }
    }

    /*! DeleteIncidentReport method deletes an incident report from the database
     *  @param Takes an incident report as a parameter 
     *  @throws Exception if there is an issue when trying to delete the incident report
     */
    public void DeleteIncidentReport(IncidentReports incidentReports)
    {
        try {
            _context.IncidentReports.Remove(incidentReports);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error deleting report");
        }
    }

    /*! GetIncidentReportList method retrieves a list of all incident reports in the database 
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns a list of incident reports
     */
    public List<IncidentReports> GetIncidentReportList()
    {
        try {
            return _context.IncidentReports.ToList();
        } catch (Exception) {
            throw new Exception("Error retrieving incident report list");
        }
    }

}
