namespace SoulashExplorer.Types;

public class SaveEntityName
{
  public string NamingConvention => NameStrings[0] ?? string.Empty;

  public string GivenName => NameStrings[1] ?? string.Empty;
  public string FamilyName => NameStrings[4] ?? string.Empty;
  public string FullActorName => $"{GivenName} {FamilyName}".Trim();

  public string ItemName => NameStrings[1] ?? string.Empty;
  public string ItemPrefix => NameStrings[2] ?? string.Empty;
  public string ItemPostfix => NameStrings[3] ?? string.Empty;
  public string FullEntityName => $"{ItemPrefix} {ItemName} {ItemPostfix}".Trim();

  public string[] NameStrings;
  public SaveEntityName(string nameString)
  {
    //Name;core_2_Necrotyrant|Tomb|||the Bone Reaper
    //Name;310|Bone Knife||Versatile|
    //Name;|Ukona|||Ugdujab
    NameStrings = nameString.Split('|');
  }
}
