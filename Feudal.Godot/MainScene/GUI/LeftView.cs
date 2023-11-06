using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

public partial class LeftView : ViewControl
{
    public Button Close => GetNode<Button>("HBoxContainer/Main/HBoxContainer/ControlContainer/XButton");
    public Button Next => GetNode<Button>("HBoxContainer/Main/HBoxContainer/ControlContainer/>Button");
    public Button Prev => GetNode<Button>("HBoxContainer/Main/HBoxContainer/ControlContainer/<Button");

    public Control Container => GetNode<Control>("HBoxContainer/Main/HBoxContainer/Content");

    public override void _Ready()
    {
        Close.Pressed += () =>
        {
            this.QueueFree();
        };

        Prev.Pressed += () =>
        {
            var mainPanels = Container.GetChildren().OfType<IMainPanelView>().ToArray();
            var index = Array.FindIndex(mainPanels, x => ((Control)x).Visible);

            ((Control)mainPanels[index]).SetHidden(true);
            ((Control)mainPanels[index-1]).SetHidden(false);

            Prev.Disabled = index == 1;
            Next.Disabled = false;

        };

        Next.Pressed += () =>
        {
            var mainPanels = Container.GetChildren().OfType<IMainPanelView>().ToArray();
            var index = Array.FindIndex(mainPanels, x => ((Control)x).Visible);

            ((Control)mainPanels[index]).SetHidden(true);
            ((Control)mainPanels[index + 1]).SetHidden(false);

            Next.Disabled = index == mainPanels.Length - 2;
            Prev.Disabled = false;
        };

        ClanItemView.ShowClan = (clanId) => ShowClanPanel(clanId);

        base._Ready();
    }

    internal ClanPanelView ShowClanPanel(string clanId)
    {
        var manPanel = AddOrFindMainPanel<ClanPanelView>(x => x.ClanId == clanId);
        manPanel.ClanId = clanId;

        return manPanel;
    }

    internal ClanArrayPanelView ShowClanArrayPanel()
    {
        var manPanel = AddOrFindMainPanel<ClanArrayPanelView>();

        return manPanel;
    }

    private T AddOrFindMainPanel<T>(Predicate<T> predicate = null) where T : ViewControl, IMainPanelView
    {
        var mainPanels = Container.GetChildren().OfType<IMainPanelView>().ToList();
        var index = mainPanels.FindIndex(x => ((Control)x).Visible);

        if (index != -1)
        {
            var needRemoves = mainPanels.Skip(index + 1).ToArray();
            foreach(var item in needRemoves)
            {
                mainPanels.Remove(item);

                var control = item as Control;
                Container.RemoveChild(control);
                control.QueueFree();
            }

            ((Control)mainPanels[index]).SetHidden(true);
        }

        var manPanel = mainPanels.OfType<T>().SingleOrDefault(x => predicate != null ? predicate(x) : true);
        if (manPanel == null)
        {
            manPanel = Container.GetNode<InstancePlaceholder>(typeof(T).Name).CreateInstance() as T;
        }

        manPanel.SetHidden(false);

        Container.MoveChild(manPanel, -1);

        Next.Disabled = true;
        Prev.Disabled = Container.GetChildren().OfType<IMainPanelView>().Count() <= 1;

        return manPanel;
    }
}