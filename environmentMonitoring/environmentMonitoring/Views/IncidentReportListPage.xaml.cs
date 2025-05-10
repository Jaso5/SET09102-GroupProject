using System.Threading.Tasks;
using environmentMonitoring.ViewModels;
namespace environmentMonitoring.Views;

public partial class IncidentReportListPage : ContentPage
{
	public IncidentReportListPage(IncidentReportListViewModel incidentReportListViewModel)
	{
		this.BindingContext = incidentReportListViewModel;
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        incidentReportList.SelectedItem = null;
    }
}