using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class IncidentReportPage : ContentPage
{
	public IncidentReportPage(IncidentReportViewModel incidentReportViewModel)
	{
		this.BindingContext = incidentReportViewModel;
		InitializeComponent();
	}
}