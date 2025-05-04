using environmentMonitoring.Database.Models;

namespace environmentMonitoring.PartialViews;

public partial class VirtualSensorView : ContentView
{
	private VirtualSensor vs;

	public VirtualSensorView(VirtualSensor vs)
	{
		this.vs = vs;
		InitializeComponent();

		Quantity.Text = vs.Quantity.quantity;
	}
}