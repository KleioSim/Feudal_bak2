﻿using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : Present<GUIView, ISession>
{
    [Export]
    public string PlayerClanName
    {
        get => MockModel.PlayerClan.Name;
        set
        {
            ((ClanMock)MockModel.PlayerClan).Name = value;

            isDirty = true;
        }
    }

    [Export]
    public int PlayerClanPopCount
    {
        get => MockModel.PlayerClan.PopCount;
        set
        {
            ((ClanMock)MockModel.PlayerClan).PopCount = value;

            isDirty = true;
        }
    }

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Tasks = new List<TaskMock>()
        {
            new TaskMock(),
            new TaskMock()
        },

        Clans = new List<IClan>()
        {
            new ClanMock(){ Name = "PlayerClan_Mock", PopCount = 1000  },
            new ClanMock(),
            new ClanMock(),
        }
    };
}

public class SessionMock : ISession
{
    public IClan PlayerClan => Clans.First();

    public IEnumerable<IClan> Clans { get; init; }
    public IEnumerable<ITask> Tasks { get; init; }
    public IEnumerable<ITerrain> Terrains { get; init; }

    public void ProcessUICommand(UICommand command)
    {
        GD.Print($"ProcessUICommand {command.GetType().Name}");
    }
}