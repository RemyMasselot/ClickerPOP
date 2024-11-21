using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sentences : MonoBehaviour
{
    public bool IsFrench;
    
    #region CONTENT
    [Space(10)]
    
    [Header("FRANCAIS")]
    [Header("FR TUTO")]
    public string FRTutoMachine;
    public string FRTutoBucket;
    public string FRBravo;
    public string FRYourTurn;
    public string FRMorePopcorns;

    [Space(10)]

    [Header("FR BOUTONS BOARD")]
    public string FRBtnMachine;
    public string FRBtnBucket;
    public string FRBtnBucket1;
    public string FRBtnBucket2;
    public string FRBtnBucket3;

    [Space(10)]

    [Header("FR UPGRADES MACHINE")]
    public string FRMachineAutoclick1;
    public string FRMachineAutoclick2;
    public string FRTips;
    public string FRPopcornCreation;
    public string FRBurn;
    public string FRShieldDesc;

    [Space(10)]

    [Header("FR UPGRADES BUCKET")]
    public string FRBucketAutoclick1;
    public string FRBucketAutoclick2;
    public string FRFill;
    public string FRStorage;
    public string FRReload;

    [Space(10)]

    [Header("FR Stats")]
    public string FRStatMoney;
    public string FRStatPopcorn;
    public string FRStatBucket;
    public string FRBtnPhysic;
    public string FRLanguage;
    public string FRBtnQuit;

    [Space(20)]

    [Header("ANGLAIS")]
    [Header("EN TUTO")]
    public string ENTutoMachine;
    public string ENTutoBucket;
    public string ENBravo;
    public string ENYourTurn;
    public string ENMorePopcorns;

    [Space(10)]

    [Header("EN BOUTONS BOARD")]
    public string ENBtnMachine;
    public string ENBtnBucket;
    public string ENBtnBucket1;
    public string ENBtnBucket2;
    public string ENBtnBucket3;

    [Space(10)]

    [Header("EN UPGRADES MACHINE")]
    public string ENMachineAutoclick1;
    public string ENMachineAutoclick2;
    public string ENTips;
    public string ENPopcornCreation;
    public string ENBurn;
    public string ENShieldDesc;

    [Space(10)]

    [Header("EN UPGRADES BUCKET")]
    public string ENBucketAutoclick1;
    public string ENBucketAutoclick2;
    public string ENFill;
    public string ENStorage;
    public string ENReload;

    [Space(10)]

    [Header("EN Stats")]
    public string ENStatMoney;
    public string ENStatPopcorn;
    public string ENStatBucket;
    public string ENBtnPhysic;
    public string ENLanguage;
    public string ENBtnQuit;

    [Space(20)]
    #endregion

    #region REFERENCES
    [Header("REFERENCES")]
    [Header("TUTO")]
    public TextMeshProUGUI TutoMachine;
    public TextMeshProUGUI TutoBucket;
    public TextMeshProUGUI Bravo;
    public TextMeshProUGUI YourTurn;
    public TextMeshProUGUI MorePopcorns;

    [Space(10)]

    [Header("BOUTONS BOARD")]
    public TextMeshProUGUI BtnMachine;
    public TextMeshProUGUI BtnBucket;
    public TextMeshProUGUI BtnBucket1;
    public TextMeshProUGUI BtnBucket2;
    public TextMeshProUGUI BtnBucket3;

    [Space(10)]

    [Header("UPGRADES MACHINE")]
    public TextMeshProUGUI MachineAutoclick1;
    public string MachineAutoclick2;
    public TextMeshProUGUI Tips;
    public TextMeshProUGUI PopcornCreation;
    public TextMeshProUGUI Burn;
    public TextMeshProUGUI ShieldDesc;

    //[Space(10)]
    //
    //[Header("UPGRADES BUCKET")]
    //public string ENBucketAutoclick1;
    //public string ENBucketAutoclick2;
    //public string ENFill;
    //public string ENStorage;
    //public string ENReload;
    //
    //[Space(10)]
    //
    //[Header("Stats")]
    //public string ENStatMoney;
    //public string ENStatPopcorn;
    //public string ENStatBucket;
    //public string ENBtnPhysic;
    //public string ENLanguage;
    //public string ENBtnQuit;
    #endregion


}
