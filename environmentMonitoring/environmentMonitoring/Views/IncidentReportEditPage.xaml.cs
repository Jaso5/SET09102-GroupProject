using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class IncidentReportEditPage : ContentPage
{
	public IncidentReportEditPage(IncidentReportEditViewModel incidentReportEditViewModel)
	{
		this.BindingContext = incidentReportEditViewModel;
		InitializeComponent();
	}
}