namespace SoulashExplorer.Types;

public class SaveEntity(int ID, SaveEntityName name, bool isHumanoid, Glyph[] glyphs)
{
  public int EntityID = ID;
  public SaveEntityName Name = name;
  public bool IsHumanoid = isHumanoid;
  public Glyph[] Glyphs = glyphs;
  public string GetFullName
  {
    get
    {
      if (IsHumanoid)
        return Name.FullActorName;

      return Name.FullEntityName;
    }
  }
}
