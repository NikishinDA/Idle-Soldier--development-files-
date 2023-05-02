using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private Button dpsUpgradeButton;
    [SerializeField] private Button armorUpgradeButton;
    [SerializeField] private Button moneyUpgradeButton;
    [SerializeField] private Image dpsUpgradeButtonImage;
    [SerializeField] private Image armorUpgradeButtonImage;
    [SerializeField] private Image moneyUpgradeButtonImage;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text dpsUpgradeCost;
    [SerializeField] private Text armorUpgradeCost;
    [SerializeField] private Text moneyUpgradeCost;
    [SerializeField] private Text dpsUpgradeLevel;
    [SerializeField] private Text armorUpgradeLevel;
    [SerializeField] private Text moneyUpgradeLevel;
    [SerializeField] private Image progressBar;
    [SerializeField] private Sprite activeDamage;
    [SerializeField] private Sprite activeArmor;
    [SerializeField] private Sprite activeMoney;
    [SerializeField] private Sprite deactiveDamage;
    [SerializeField] private Sprite deactiveArmor;
    [SerializeField] private Sprite deactiveMoney;
    [Header("Debug")]
    [SerializeField] private Button resetButton;
    private MoneyManager _moneyManager;
    private float _progress;
    private void Awake()
    {
        dpsUpgradeButton.onClick.AddListener(OnDPSUpgradeButtonClick);
        armorUpgradeButton.onClick.AddListener(OnArmorUpgradeButtonClick);
        moneyUpgradeButton.onClick.AddListener(OnMoneyUpgradeButtonClick);
        resetButton.onClick.AddListener(OnResetButtonClick);
        EventManager.AddListener<MoneyAmountChangeEvent>(OnMoneyAmountChange);
        EventManager.AddListener<PlayerProgressEvent>(OnPlayerProgress);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnPlayerCheckPointCross);
        _moneyManager = GetComponent<MoneyManager>();
        _moneyManager.MoneyEnoughMoney += ActivateMoneyButton;
        _moneyManager.WeaponEnoughMoney += ActivateWeaponButton;
        _moneyManager.ArmorEnoughMoney += ActivateArmorButton;
        LoadProgress();
    }

    private void ActivateMoneyButton(bool obj)
    {
        moneyUpgradeButtonImage.sprite = obj ? activeMoney : deactiveMoney;
    }
    private void ActivateWeaponButton(bool obj)
    {
        dpsUpgradeButtonImage.sprite = obj ? activeDamage : deactiveDamage;
    }
    private void ActivateArmorButton(bool obj)
    {
        armorUpgradeButtonImage.sprite = obj ? activeArmor : deactiveArmor;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyAmountChangeEvent>(OnMoneyAmountChange);
        EventManager.RemoveListener<PlayerProgressEvent>(OnPlayerProgress);
        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnPlayerCheckPointCross);
    }


    private void OnPlayerCheckPointCross(PlayerCheckpointCrossEvent obj)
    {
        VarSaver.SectionNumber++;
        if (VarSaver.SectionNumber == 3)
        {
            VarSaver.SectionNumber = 0;
            progressBar.fillAmount = 0f;
            _progress = 0f;
        }
        PlayerPrefs.SetInt(PlayerPrefsStrings.SectionNumber, VarSaver.SectionNumber);
    }

    private void LoadProgress()
    {
        VarSaver.SectionNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.SectionNumber, 0);
        _progress = (float)VarSaver.SectionNumber / 3;
        progressBar.fillAmount = _progress;
    }
    private void OnPlayerProgress(PlayerProgressEvent obj)
    {
        _progress += 1f / (3 * VarSaver.LevelLength);
    }

    private void OnMoneyAmountChange(MoneyAmountChangeEvent obj)
    {
        moneyText.text = obj.Amount.ToString("N0", CultureInfo.InvariantCulture);
    }

    private void Start()
    {
        moneyUpgradeCost.text = _moneyManager.MoneyUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);
        dpsUpgradeCost.text = _moneyManager.DamageUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);
        armorUpgradeCost.text = _moneyManager.HealthUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);
        moneyUpgradeLevel.text = "Level " + _moneyManager.MoneyUpgradeLevel.ToString();
        dpsUpgradeLevel.text = "Level " + _moneyManager.DamageUpgradeLevel.ToString();
        armorUpgradeLevel.text = "Level " + _moneyManager.HealthUpgradeLevel.ToString();
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, _progress, Time.deltaTime);
    }


    private void OnMoneyUpgradeButtonClick()
    {
        if (_moneyManager.TryMoneyUpgrade())
        {
            moneyUpgradeCost.text = _moneyManager.MoneyUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);

            moneyUpgradeLevel.text = "Level " + _moneyManager.MoneyUpgradeLevel.ToString();
        }
    }

    private void OnArmorUpgradeButtonClick()
    {
        if (_moneyManager.TryHealthUpgrade())
        {
            armorUpgradeCost.text = _moneyManager.HealthUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);
            armorUpgradeLevel.text = "Level " + _moneyManager.HealthUpgradeLevel.ToString();

        }
    }

    private void OnDPSUpgradeButtonClick()
    {
        if (_moneyManager.TryWeaponUpgrade())
        {
            dpsUpgradeCost.text = _moneyManager.DamageUpgradeCost.ToString("N0", CultureInfo.InvariantCulture);
            dpsUpgradeLevel.text = "Level " + _moneyManager.DamageUpgradeLevel.ToString();

        }
    }

    private void OnResetButtonClick()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneLoader.ReloadLevel();
    }

}
