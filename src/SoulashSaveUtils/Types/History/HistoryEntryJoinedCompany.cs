/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class HistoryEntryJoinedCompany(int eventID, int year, int day, EventType what, int who, int company) : HistoryEntry(eventID, year, day, what, who)
{

  public int WhichCompany = company;

  public override string DescribeEvent(SaveCollection save)
  {
    string companyName = $"Unknown {WhichCompany}";

    if (save.AllCompanies.TryGetValue(WhichCompany, out var wCompany))
      companyName = wCompany.Name;

    return $"joined the {companyName} company";
  }
}
