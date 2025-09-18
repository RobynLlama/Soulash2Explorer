/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

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
