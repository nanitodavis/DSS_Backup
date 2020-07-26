[System.Serializable]
public class BaseCharacterCharacteristic {

    //General Attributes
    public int characterId;
	public string name;
    public string log;
	public int level;
    public EnumsScript.Element affinityElement;
    public EnumsScript.Condition condition;
    private int totalExperience;
	private int experienceToNextLevel;

	//Stats
	public int healthPoints;
    public int spiritualPoints;
    public int physicalAttack;

    /*public static explicit operator BaseCharacterCharacteristic(UnityEngine.Object v)
    {
        throw new NotImplementedException();
    }*/

    public int physicalDefense;
    public int spiritualAttack;
    public int spiritualDefense;
    public float critic;

	//constructors
	public BaseCharacterCharacteristic(){
		name="";
		log = "";
		level = 1;
		affinityElement = EnumsScript.Element.NEUTRAL;
		totalExperience = 0;
		experienceToNextLevel = 1;
		healthPoints = 1;
		spiritualPoints = 1;
		physicalAttack = 1;
		physicalDefense=1;
		spiritualAttack = 1;
		spiritualDefense = 1;
		critic = 0f;
	}
		
	public BaseCharacterCharacteristic(string nName, string nLog, int nLevel, EnumsScript.Element nAffinityElement, int nTotalExperience, int nExperienceToNextLevel, int nHealthPoints,
		int nSpiritualPoints, int nPhysicalAttack, int nPhysicalDefense, int nSpiritualAttack, int nSpiritualDefense, float nCritic){
		name = nName;
		log = nLog;
		level = nLevel;
		affinityElement = nAffinityElement;
		totalExperience = nTotalExperience;
		experienceToNextLevel = nExperienceToNextLevel;
		healthPoints = nHealthPoints;
		spiritualPoints = nSpiritualPoints;
		physicalAttack = nPhysicalAttack;
		physicalDefense = nPhysicalDefense;
		spiritualAttack = nSpiritualAttack;
		spiritualDefense = nSpiritualDefense;
		critic = nCritic;
	}

	public void AddHealthPoint(int HPToAdd){
		if (healthPoints < 9999) {
			healthPoints += HPToAdd;
			if (healthPoints > 9999) {
				healthPoints = 9999;
			}
		}
	}

    //change all getters n setters to this method
    public int HealthPointsProperty
    {
        get
        {
            return healthPoints;
        }
        set
        {
            healthPoints = value;
            if (healthPoints > 9999)
            {
                healthPoints = 9999;
            }else if (healthPoints < 0)
            {
                healthPoints = 0;
            }
        }
    }

	public void AddSpiritualPoints(int SPToAdd){
		if (spiritualPoints < 9999) {
			spiritualPoints += SPToAdd;
			if (spiritualPoints > 9999) {
				spiritualPoints = 9999;
			}
		}
	}

    public int SpiritualPointsProperty
    {
        get
        {
            return spiritualPoints;
        }
        set
        {
            spiritualPoints = value;
            if (spiritualPoints > 9999)
            {
                spiritualPoints = 9999;
            }
            else if (spiritualPoints < 0)
            {
                spiritualPoints = 0;
            }
        }
    }

	public void AddPhysicalAttack(int phyAttToAdd){
		if (physicalAttack < 999) {
			physicalAttack += phyAttToAdd;
			if (physicalAttack > 999) {
				physicalAttack = 999;
			}
		}
	}

	public int PhysicalAttackProperty
    {
        get
        {
            return physicalAttack;
        }
        set
        {
            physicalAttack = value;
            if (physicalAttack > 999)
            {
                physicalAttack = 999;
            }else if (physicalAttack <= 0)
            {
                physicalAttack = 1;
            }
        }
    }

	public void AddPhysicalDefense(int phyDefToAdd){
		if (physicalDefense < 999) {
			physicalDefense += phyDefToAdd;
			if (physicalDefense > 999) {
				physicalDefense = 999;
			}
		}
    }

    public int PhysicalDefenseProperty
    {
        get
        {
            return physicalDefense;
        }
        set
        {
            physicalDefense = value;
            if (physicalDefense > 999)
            {
                physicalDefense = 999;
            }
            else if (physicalDefense <= 0)
            {
                physicalDefense = 1;
            }
        }
    }

    public void AddSpiritualAttack(int SpiAttToAdd){
		if (spiritualAttack < 999) {
			spiritualAttack += SpiAttToAdd;
			if (spiritualAttack > 999) {
				spiritualAttack = 999;
			}
		}
    }

    public int SpiritualAttackProperty
    {
        get
        {
            return spiritualAttack;
        }
        set
        {
            spiritualAttack = value;
            if (spiritualAttack > 999)
            {
                spiritualAttack = 999;
            }
            else if (spiritualAttack <= 0)
            {
                spiritualAttack = 1;
            }
        }
    }

    public void AddSpiritualDefense(int SpiDefToAdd){
		if (spiritualDefense < 999) {
			spiritualDefense += SpiDefToAdd;
			if (spiritualDefense > 999) {
				spiritualDefense = 999;
			}
		}
    }

    public int SpiritualDefenseProperty
    {
        get
        {
            return spiritualDefense;
        }
        set
        {
            spiritualDefense = value;
            if (spiritualDefense > 999)
            {
                spiritualDefense = 999;
            }
            else if (spiritualDefense <= 0)
            {
                spiritualDefense = 1;
            }
        }
    }

    public void AddCritic(float criticToAdd){
		if (critic < 99) {
			critic += criticToAdd;
			if (critic > 99) {
				critic = 99;
			}
		}
    }

    public float CriticProperty
    {
        get
        {
            return spiritualDefense;
        }
        set
        {
            critic = value;
            if (critic > 99)
            {
                critic = 99;
            }
            else if (critic <= 0)
            {
                critic = 1;
            }
        }
    }
}