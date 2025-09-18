using System;
using Godot;

namespace Soulash2Explorer;

public partial class EntityListItem : PanelContainer
{
  [Export]
  public PanelContainer PortraitContainer;

  [Export]
  public Label Desc;

  [Export]
  public Button ViewButton;

  public event Action<int> EntityHistoryRequested;
  public int EntityID;

  public override void _Ready()
  {
    ViewButton.Pressed += OnPressedView;
  }

  private void OnPressedView() =>
    EntityHistoryRequested?.Invoke(EntityID);
}
