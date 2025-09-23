namespace SoulashSaveUtils.Types;

public static class PlayerComponent
{
  public static IEntityComponent BuildComponent(string[] args) => new GenericComponent("Player");
}
