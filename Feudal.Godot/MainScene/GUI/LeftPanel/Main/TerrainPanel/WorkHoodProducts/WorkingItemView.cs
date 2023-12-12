﻿using Feudal.Interfaces;
using Godot;
using System;

public partial class WorkingItemView : ItemView
{
    public override object Id { get; set; }

    public Button Button => GetNode<Button>("HBoxContainer/Button");

    public Control ProgressPanel => GetNode<Control>("HBoxContainer/PanelContainer/ProgressBar");
    public Control ProductPanel => GetNode<Control>("HBoxContainer/PanelContainer/ProductContainer");

    public ProgressBar ProgressBar => ProgressPanel.GetNode<ProgressBar>(".");
    public Label ProductType => ProductPanel.GetNode<Label>("HBoxContainer/Type");
    public Label ProductCount => ProductPanel.GetNode<Label>("HBoxContainer/Count");
}
