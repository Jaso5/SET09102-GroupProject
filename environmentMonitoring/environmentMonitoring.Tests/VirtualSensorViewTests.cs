using environmentMonitoring.Database.Models;
using environmentMonitoring.PartialViews;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

[TestClass]
public sealed class VirtualSensorViewTests
{
    [TestMethod]
    public void TestReadingsToSeries()
    {
        // Arrange
        var readings = new List<Readings> {
            new Readings { timestamp = new DateTime(1746000000), value = 0.1f},
            new Readings { timestamp = new DateTime(1747000000), value = 0.2f},
            new Readings { timestamp = new DateTime(1748000000), value = 0.3f},
        };
        var quantity = new Quantities
        {
            quantity = "Test Quantity"
        };

        // Act
        var series = VirtualSensorView.ReadingsToSeries(readings, "Test Quantity");

        // Assert
        Assert.Equals(
            series.First(),
            new LineSeries<DateTimePoint>
            {
                Values = new[] {
                    new DateTimePoint(new DateTime(1746000000), 0.1f),
                    new DateTimePoint(new DateTime(1747000000), 0.2f),
                    new DateTimePoint(new DateTime(1748000000), 0.3f)
                }
            }
        );
    }
}
