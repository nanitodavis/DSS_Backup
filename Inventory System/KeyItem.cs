﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Key Item")]
public class KeyItem : BaseItem
{
    public string secondDescription;
    public bool enableSkill;
    public BaseSkill skillEnabled;
}
