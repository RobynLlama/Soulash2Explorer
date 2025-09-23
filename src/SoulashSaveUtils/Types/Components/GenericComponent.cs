namespace SoulashSaveUtils.Types;

public class GenericComponent(string name) : IEntityComponent
{
  public string ComponentID => GenericName;
  private readonly string GenericName = name;
}
