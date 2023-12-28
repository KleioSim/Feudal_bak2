using Feudal.Interfaces;

namespace Feudal.WorkHoods.Workings;

class ProductWorking : IProductWorking
{
    public ProductType ProductType => (def as IProductWorkingDef).Type;

    public double ProductCount => (def as IProductWorkingDef).Count;

    public string Key => Name;

    public string Name => def.Name;

    public IWorkingDef def { get; init; }

    public void Do()
    {
        throw new NotImplementedException();
    }
}
