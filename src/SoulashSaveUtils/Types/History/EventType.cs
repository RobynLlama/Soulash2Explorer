/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public enum EventType
{
  /// <summary>
  /// An actor was born
  /// </summary>
  Born = 1,

  /// <summary>
  /// An actor died in some way
  /// </summary>
  Died = 2,

  /// <summary>
  /// An actor married another actor
  /// </summary>
  Married = 3,

  /// <summary>
  /// An actor got a job
  /// </summary>
  GotJob = 4,

  /// <summary>
  /// An actor became leader of a family
  /// </summary>
  BecameFamilyLeader = 9,

  /// <summary>
  /// An actor became leader of a settlement
  /// </summary>
  BecameSettlementLeader = 10,

  /// <summary>
  /// An actor joined a company
  /// </summary>
  JoinedCompany = 12,

  /// <summary>
  /// An actor joined a family
  /// </summary>
  JoinedFamily = 13,

  /// <summary>
  /// A company raided a location outside of a war
  /// This is usually the first battle that declares a war
  /// but is not officially in one
  /// </summary>
  CompanyRaided = 14,

  /// <summary>
  /// A company took part in a battle that was recorded as
  /// part of an active war on the attacking side.
  /// </summary>
  CompanyWarOffensive = 16,

  /// <summary>
  /// A company took part in a battle that was recorded as
  /// part of an active war on the defending side.
  /// </summary>
  CompanyWarDefensive = 17,

  /// <summary>
  /// Only the Necrotyrant seems to use this event and can die multiple
  /// times. I have not encountered one in gameplay and do not know how
  /// they work but this feels very much like a phylactery reviving a
  /// Lich since Necrotyrants always have this event and multiple deaths
  /// whereas no other entity dies more than once, ever. Even skeletons
  /// get a new entity when raised so the original entity dies one time
  /// </summary>
  NecrotyrantReborn = 19,
}
