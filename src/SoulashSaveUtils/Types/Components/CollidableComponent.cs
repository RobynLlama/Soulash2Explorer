namespace SoulashSaveUtils.Types;

public static class CollidableComponent
{
  public static IEntityComponent BuildComponent(string[] args) => new GenericComponent("Collidable");
}
