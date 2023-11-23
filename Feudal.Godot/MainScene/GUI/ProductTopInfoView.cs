using System;

public partial class ProductTopInfoView : ViewControl
{
    public ProductContainer Container => GetNode<ProductContainer>("Containter");
}
