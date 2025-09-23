namespace SoulashSaveUtils.Types;

public static class ActorComponent
{
  public static IEntityComponent BuildComponent(string[] args) => new GenericComponent("Actor");
}
