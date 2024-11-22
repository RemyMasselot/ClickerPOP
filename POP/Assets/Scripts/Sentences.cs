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
    public string FRToolTipBucket;

    [Space(10)]

    [Header("FR BOUTONS BOARD")]
    public string FRBtnMachine;
    public string FRBtnBucket;
    public string FRBtnBucket1;
    public string FRBtnBucket2;
    public string FRBtnBucket3;

    [Space(10)]

    [Header("FR UPGRADES MACHINE")]
    public string FRMachineAutoclickTitle;
    public string FRMachineAutoclick1;
    public string FRMachineAutoclick2;
    public string FRTipsTitle;
    public string FRTips;
    public string FRPopcornCreationTitle;
    public string FRPopcornCreation;
    public string FRBurnTitle;
    public string FRBurn;
    public string FRShieldDescTitle;
    public string FRShieldDesc;

    [Space(10)]

    [Header("FR UPGRADES BUCKET")]
    public string FRBucketAutoclickTitle;
    public string FRBucketAutoclick1;
    public string FRBucketAutoclick2;
    public string FRFillTitle;
    public string FRFill;
    public string FRStorageTitle;
    public string FRStorage;
    public string FRReloadTitle;
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
    public string ENToolTipBucket;

    [Space(10)]

    [Header("EN BOUTONS BOARD")]
    public string ENBtnMachine;
    public string ENBtnBucket;
    public string ENBtnBucket1;
    public string ENBtnBucket2;
    public string ENBtnBucket3;

    [Space(10)]

    [Header("EN UPGRADES MACHINE")]
    public string ENMachineAutoclickTitle;
    public string ENMachineAutoclick1;
    public string ENMachineAutoclick2;
    public string ENTipsTitle;
    public string ENTips;
    public string ENPopcornCreationTitle;
    public string ENPopcornCreation;
    public string ENBurnTitle;
    public string ENBurn;
    public string ENShieldDescTitle;
    public string ENShieldDesc;

    [Space(10)]

    [Header("EN UPGRADES BUCKET")]
    public string ENBucketAutoclickTitle;
    public string ENBucketAutoclick1;
    public string ENBucketAutoclick2;
    public string ENFillTitle;
    public string ENFill;
    public string ENStorageTitle;
    public string ENStorage;
    public string ENReloadTitle;
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
    public Tuto Tuto;
    public List<string> TutoTextsFR = new List<string>();
    public List<string> TutoTextsEN = new List<string>();
    public TextMeshProUGUI TutoText;
    public TextMeshProUGUI ToolTipBucket;

    [Space(10)]
    
    [Header("BOUTONS BOARD")]
    public TextMeshProUGUI BtnMachine;
    public TextMeshProUGUI BtnBucket;
    public TextMeshProUGUI BtnBucket1;
    public TextMeshProUGUI BtnBucket2;
    public TextMeshProUGUI BtnBucket3;
    
    [Space(10)]
    
    [Header("UPGRADES MACHINE")]
    public MachineAutoClick MachineAutoclick;
    public List<string> MachineAutoclickTextsFR = new List<string>();
    public List<string> MachineAutoclickTextsEN = new List<string>();
    public TextMeshProUGUI MachineAutoclickTitleText;
    public TextMeshProUGUI MachineAutoclickText;
    public TextMeshProUGUI TipsTitle;
    public TextMeshProUGUI Tips;
    public TextMeshProUGUI PopcornCreationTitle;
    public TextMeshProUGUI PopcornCreation;
    public TextMeshProUGUI BurnTitle;
    public TextMeshProUGUI Burn;
    public TextMeshProUGUI ShieldDescTitle;
    public TextMeshProUGUI ShieldDesc;

    [Space(10)]
    
    [Header("UPGRADES BUCKET")]
    public List<string> BucketAutoclickTextsFR = new List<string>();
    public List<string> BucketAutoclickTextsEN = new List<string>();
    [Space(5)]

    [Header("BUCKET 1")]
    public BucketAutoclick BucketAutoclick1;
    public TextMeshProUGUI BucketAutoclickTitleText1;
    public TextMeshProUGUI BucketAutoclickText1;
    public TextMeshProUGUI FillTitle1;
    public TextMeshProUGUI Fill1;
    public TextMeshProUGUI StorageTitle1;
    public TextMeshProUGUI Storage1;
    public TextMeshProUGUI ReloadTitle1;
    public TextMeshProUGUI Reload1;

    [Space(5)]

    [Header("BUCKET 2")]
    public BucketAutoclick BucketAutoclick2;
    public TextMeshProUGUI BucketAutoclickTitleText2;
    public TextMeshProUGUI BucketAutoclickText2;
    public TextMeshProUGUI FillTitle2;
    public TextMeshProUGUI Fill2;
    public TextMeshProUGUI StorageTitle2;
    public TextMeshProUGUI Storage2;
    public TextMeshProUGUI ReloadTitle2;
    public TextMeshProUGUI Reload2;

    [Space(5)]

    [Header("BUCKET 3")]
    public BucketAutoclick BucketAutoclick3;
    public TextMeshProUGUI BucketAutoclickTitleText3;
    public TextMeshProUGUI BucketAutoclickText3;
    public TextMeshProUGUI FillTitle3;
    public TextMeshProUGUI Fill3;
    public TextMeshProUGUI StorageTitle3;
    public TextMeshProUGUI Storage3;
    public TextMeshProUGUI ReloadTitle3;
    public TextMeshProUGUI Reload3;

    [Space(10)]
    
    [Header("Stats")]
    public TextMeshProUGUI StatMoney;
    public TextMeshProUGUI StatPopcorn;
    public TextMeshProUGUI StatBucket;
    public TextMeshProUGUI BtnPhysic;
    public TextMeshProUGUI Language;
    public TextMeshProUGUI BtnQuit;
    #endregion


    private void Awake()
    {
        Tuto = FindObjectOfType<Tuto>();
        TutoTextsFR.Add(FRTutoMachine);
        TutoTextsFR.Add(FRTutoBucket);
        TutoTextsFR.Add(FRBravo);
        TutoTextsFR.Add(FRYourTurn);
        TutoTextsEN.Add(ENTutoMachine);
        TutoTextsEN.Add(ENTutoBucket);
        TutoTextsEN.Add(ENBravo);
        TutoTextsEN.Add(ENYourTurn);

        MachineAutoclick = FindObjectOfType<MachineAutoClick>();
        MachineAutoclickTextsFR.Add(FRMachineAutoclick1);
        MachineAutoclickTextsFR.Add(FRMachineAutoclick2);
        MachineAutoclickTextsEN.Add(ENMachineAutoclick1);
        MachineAutoclickTextsEN.Add(ENMachineAutoclick2);

        BucketAutoclickTextsFR.Add(FRBucketAutoclick1);
        BucketAutoclickTextsFR.Add(FRBucketAutoclick2);
        BucketAutoclickTextsEN.Add(ENBucketAutoclick1);
        BucketAutoclickTextsEN.Add(ENBucketAutoclick2);
    }

    private void Start()
    {
        SetEnglish();
    }


    public void SetFrench()
    {
        // TUTO
        TutoText.text = TutoTextsFR[Tuto.Part];
        ToolTipBucket.text = FRToolTipBucket;

        // BTN BOARD
        BtnMachine.text = FRBtnMachine;
        BtnBucket.text  = FRBtnBucket;
        BtnBucket1.text = FRBtnBucket1;
        BtnBucket2.text = FRBtnBucket2;
        BtnBucket3.text = FRBtnBucket3;

        // MACHINE
        MachineAutoclickTitleText.text  = FRMachineAutoclickTitle;
        MachineAutoclickText.text = MachineAutoclickTextsFR[MachineAutoclick.Part];
        TipsTitle.text = FRTipsTitle;
        Tips.text = FRTips;
        PopcornCreationTitle.text = FRPopcornCreationTitle;
        PopcornCreation.text = FRPopcornCreation;
        BurnTitle.text = FRBurnTitle;
        Burn.text = FRBurn;

        // SHIELD
        ShieldDescTitle.text = FRShieldDescTitle;
        ShieldDesc.text = FRShieldDesc;

        // BUCKET 1
        BucketAutoclickTitleText1.text = FRBucketAutoclickTitle;
        BucketAutoclickText1.text = BucketAutoclickTextsFR[BucketAutoclick1.Part];
        FillTitle1.text = FRFillTitle;
        Fill1.text = FRFill;
        StorageTitle1.text = FRStorageTitle;
        Storage1.text = FRStorage;
        ReloadTitle1.text = FRReloadTitle;
        Reload1.text = FRReload;

        // BUCKET 2
        BucketAutoclickTitleText2.text = FRBucketAutoclickTitle;
        BucketAutoclickText2.text = BucketAutoclickTextsFR[BucketAutoclick2.Part];
        FillTitle2.text = FRFillTitle;
        Fill2.text = FRFill;
        StorageTitle2.text = FRStorageTitle;
        Storage2.text = FRStorage;
        ReloadTitle2.text = FRReloadTitle;
        Reload2.text = FRReload;

        // BUCKET 3
        BucketAutoclickTitleText3.text = FRBucketAutoclickTitle;
        BucketAutoclickText3.text = BucketAutoclickTextsFR[BucketAutoclick3.Part];
        FillTitle3.text = FRFillTitle;
        Fill3.text = FRFill;
        StorageTitle3.text = FRStorageTitle;
        Storage3.text = FRStorage;
        ReloadTitle3.text = FRReloadTitle;
        Reload3.text = FRReload;

        // STATS
        StatMoney.text = FRStatMoney;
        StatPopcorn.text = FRStatPopcorn;
        StatBucket.text = FRStatBucket;
        BtnPhysic.text = FRBtnPhysic;
        Language.text = FRLanguage;
        BtnQuit.text = FRBtnQuit;
    }

    public void SetEnglish()
    {
        // TUTO
        TutoText.text = TutoTextsEN[Tuto.Part];
        ToolTipBucket.text = ENToolTipBucket;
        
        // BTN BOARD
        BtnMachine.text = ENBtnMachine;
        BtnBucket.text  = ENBtnBucket;
        BtnBucket1.text = ENBtnBucket1;
        BtnBucket2.text = ENBtnBucket2;
        BtnBucket3.text = ENBtnBucket3;

        // MACHINE
        MachineAutoclickTitleText.text  = ENMachineAutoclickTitle;
        MachineAutoclickText.text = MachineAutoclickTextsEN[MachineAutoclick.Part];
        TipsTitle.text = ENTipsTitle;
        Tips.text = ENTips;
        PopcornCreationTitle.text = ENPopcornCreationTitle;
        PopcornCreation.text = ENPopcornCreation;
        BurnTitle.text = ENBurnTitle;
        Burn.text = ENBurn;

        // SHIELD
        ShieldDescTitle.text = ENShieldDescTitle;
        ShieldDesc.text = ENShieldDesc;

        // BUCKET 1
        BucketAutoclickTitleText1.text = ENBucketAutoclickTitle;
        BucketAutoclickText1.text = BucketAutoclickTextsEN[BucketAutoclick1.Part];
        FillTitle1.text = ENFillTitle;
        Fill1.text = ENFill;
        StorageTitle1.text = ENStorageTitle;
        Storage1.text = ENStorage;
        ReloadTitle1.text = ENReloadTitle;
        Reload1.text = ENReload;

        // BUCKET 2
        BucketAutoclickTitleText2.text = ENBucketAutoclickTitle;
        BucketAutoclickText2.text = BucketAutoclickTextsEN[BucketAutoclick2.Part];
        FillTitle2.text = ENFillTitle;
        Fill2.text = ENFill;
        StorageTitle2.text = ENStorageTitle;
        Storage2.text = ENStorage;
        ReloadTitle2.text = ENReloadTitle;
        Reload2.text = ENReload;

        // BUCKET 3
        BucketAutoclickTitleText3.text = ENBucketAutoclickTitle;
        BucketAutoclickText3.text = BucketAutoclickTextsEN[BucketAutoclick3.Part];
        FillTitle3.text = ENFillTitle;
        Fill3.text = ENFill;
        StorageTitle3.text = ENStorageTitle;
        Storage3.text = ENStorage;
        ReloadTitle3.text = ENReloadTitle;
        Reload3.text = ENReload;

        // STATS
        StatMoney.text = ENStatMoney;
        StatPopcorn.text = ENStatPopcorn;
        StatBucket.text = ENStatBucket;
        BtnPhysic.text = ENBtnPhysic;
        Language.text = ENLanguage;
        BtnQuit.text = ENBtnQuit;
    }
}
