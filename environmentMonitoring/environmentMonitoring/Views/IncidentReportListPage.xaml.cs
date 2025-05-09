using System.Threading.Tasks;
using environmentMonitoring.ViewModels;
namespace environmentMonitoring.Views;

public partial class IncidentReportListPage : ContentPage
{
	private readonly IncidentReportListViewModel _incidentReportListViewModel;
	public IncidentReportListPage(IncidentReportListViewModel incidentReportListViewModel)
	{
		_incidentReportListViewModel = incidentReportListViewModel;
		this.BindingContext = _incidentReportListViewModel;
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        incidentReportList.SelectedItem = null;
		_incidentReportListViewModel.ReloadList();
    }
}