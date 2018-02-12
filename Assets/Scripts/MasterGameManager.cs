using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NewtonVR;
using UnityEngine.UI;

public class MasterGameManager : MonoBehaviour {

	[HideInInspector]
	public GestureGameManager gestureGameManager;

	[HideInInspector]
	public NVRPlayer nvrPlayer;

    public HumanWatchTower tower;

	[HideInInspector]
	public SpellEngine spellEngine;

	[HideInInspector]
	public Transform rightHandTransform;

	[HideInInspector]
	public Transform LeftHandTransform;

	[HideInInspector]
	public AssembledSpellQuickBar assembledSpellQuickBar;

	public bool useQuickBarMechanics = false;

    public Text goldText;
    public int gold;

    public Text expText;
    public int exp;

    public int goldRequiredToUpgrade;


    void Awake ()
	{
		nvrPlayer =  GameObject.FindWithTag("Player").GetComponent<NVRPlayer>();
        tower = GameObject.FindWithTag("tower").GetComponent<HumanWatchTower>();

		gestureGameManager = GetComponent<GestureGameManager>();
		gestureGameManager.nvrPlayer = nvrPlayer;
		gestureGameManager.masterGameManager = this;
		assembledSpellQuickBar = GetComponent<AssembledSpellQuickBar>();

		spellEngine = GetComponent<SpellEngine>();
		spellEngine.masterGameManager = this;


	}

	// Use this for initialization
	void Start () {
        gold = 100;
        goldText = GameObject.FindWithTag("canv").transform.GetChild(0).GetComponent<Text>();
        expText = GameObject.FindWithTag("canv").transform.GetChild(1).GetComponent<Text>();
        exp = 0;
        goldRequiredToUpgrade = 200;
    }

	// Update is called once per frame
	void Update () {

		//since the controllers could be turned off and on, make sure we still got hands
		if (rightHandTransform == null)
		{
			rightHandTransform = nvrPlayer.transform.Find("RightHand");
		}

		if (LeftHandTransform == null)
		{
			LeftHandTransform = nvrPlayer.transform.Find("LeftHand");
		}
        goldText.text = "Gold: " + gold;
        expText.text = "Exp: " + exp;
        print(tower.currentHealth);
    }

    public void addGold(int newGold)
    {
        gold += newGold;
    }


    public void addExp(int newExp)
    {
        exp += newExp;
    }

    public void upgradeTower()
    {
        if (gold < goldRequiredToUpgrade)
        {
            print("You do not have enough gold.");
            return;
        }
        int newHealth = (int) (tower.maximumHealth * tower.upgradeHealthMultiplier);
        tower.maximumHealth = newHealth;
        tower.currentHealth = tower.maximumHealth;
        gold = gold - goldRequiredToUpgrade;
        goldRequiredToUpgrade = goldRequiredToUpgrade * 2;
    }

    public void repairTower()
    {
        int goldNeeded = 0;
        while ((goldNeeded <= gold) && (tower.currentHealth <= tower.maximumHealth)) 
        {
            tower.currentHealth += 50;
            goldNeeded += 1;
            if (tower.currentHealth > tower.maximumHealth)
            {
                tower.currentHealth = tower.maximumHealth;
                break;
            }
        }
        gold = gold - goldNeeded;
    }
}
