using Feudal.Interfaces;
using System;

namespace Feudal.Godot.Presents;

partial class WorkingContainerPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        WorkHoods = new[]
        {
            new WorkHood_Mock()
            {
                Id = DiscoverWorkHoodPanelView.DefaultId,
                CurrentWorking = new ProgressWorking_Mock(){ Percent = 10 },
                OptionWorkings = new IWorking[]
                {
                    new ProgressWorking_Mock(){ Percent = 20 },
                    new ProductWorking_Mock() { ProductType = ProductType.Food, ProductCount = 1.0 },
                    new ProductWorking_Mock() { ProductType = ProductType.Bronze, ProductCount = 1.4 },
                }
            }
        }
    };
}

public class ProgressWorking_Mock : IProgressWorking
{
    public static int count = 0;

    public string Key { get; }

    public string Name => Key;

    public int Percent { get; set; }

    public ProgressWorking_Mock()
    {
        Key = $"ProgressWorking{count++}";
    }
}

public class ProductWorking_Mock : IProductWorking
{
    public static int count = 0;

    public string Key { get; }

    public string Name => Key;

    public ProductType ProductType { get; set; }

    public double ProductCount { get; set; }

    public ProductWorking_Mock()
    {
        Key = $"ProductWorking{count++}";
    }
}
