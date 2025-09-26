/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System.Collections.Generic;
using System.IO;
using Godot;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class CompanyListing
{
  public static bool Create(FileInfo companyFile, Dictionary<int, Company> table)
  {
    if (!companyFile.Exists)
      return false;

    using StreamReader companyData = new(companyFile.FullName);

    //split and skip the next ID and total entries fields
    string[] companyItems = companyData.ReadToEnd().Split('|');
    var recordCount = int.Parse(companyItems[3]);

    for (int i = 4; i < recordCount + 4; i++)
    {
      var data = companyItems[i].Split('*');

      if (data.Length < 2)
        continue;

      var id = int.Parse(data[0]);
      var name = data[2];
      table[id] = new(id, name);
    }

    return true;
  }
}
