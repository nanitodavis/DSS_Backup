using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ability<T>
{
    ActiveSkill ActiveSkillExecution(T obj);
}
