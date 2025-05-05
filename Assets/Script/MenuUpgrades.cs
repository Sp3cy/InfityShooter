using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class Binding
{
    [field: SerializeField] private float baseValue;
    [field: SerializeField] private float upgradeValue;
    [field: SerializeField] private string name;

    public float BaseValue
    {
        get { return this.baseValue; }
        set { this.baseValue = value; }
    }

    public float UpgradeValue
    {
        get { return this.upgradeValue; }
        set { this.upgradeValue = value; }
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }
}

public class MenuUpgrades : MonoBehaviour
{
    [field: SerializeField] private float _upgradeCost;
    [field: SerializeField] private float _upgradeCostMul;
    [field: SerializeField] private string _upgradeCostPref;

    // le cose da modificare per lo specifico upgrade, maxhp o rpm, damage
    [field: SerializeField] private Binding[] bindings;

    [Header("Mettere o upgradeTXT o le slider")]
    [field: SerializeField] private Text _upgradeTxt;
    [field: SerializeField] private string _upgradeTxtName;
    [field: SerializeField] private Slider[] _upgradeSliders;
    [field: SerializeField] private Text _upgradeCostTxt;
    [field: SerializeField] private Button _upgradeBtn;

    private void Awake()
    {
        ResetAll();
        foreach (Binding binding in bindings) 
        {
            binding.BaseValue = PlayerPrefs.GetFloat(binding.Name, binding.BaseValue);
        }

        //maxHp = PlayerPrefs.GetInt("PlayerMaxHP", 50);
        //money = PlayerPrefs.GetInt("PlayerMoney", 100);
        _upgradeCost = PlayerPrefs.GetFloat(_upgradeCostPref, _upgradeCost);

        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(Upgrade);
    }

    private void Start()
    {
        //RefreshValues();
    }

    public void Upgrade()
    {
        foreach (Binding binding in bindings)
        {
            PlayerPrefs.SetFloat(binding.Name, binding.UpgradeValue + binding.BaseValue);
            binding.BaseValue = PlayerPrefs.GetFloat(binding.Name, binding.BaseValue);
        }

        PlayerPrefs.Save();

        PlayerPrefs.SetFloat("PlayerMoney", PlayerPrefs.GetFloat("PlayerMoney", 100) - _upgradeCost);
        PlayerPrefs.SetFloat(_upgradeCostPref, (_upgradeCost * _upgradeCostMul));
        _upgradeCost = PlayerPrefs.GetFloat(_upgradeCostPref, _upgradeCost);

        PlayerPrefs.Save();

        RefreshValues();
    }

    private void RefreshValues()
    {
        if (_upgradeTxt != null)
        {
            StartCoroutine(AnimateNumber(_upgradeTxt, (int)(bindings[0].BaseValue - bindings[0].UpgradeValue), (int)bindings[0].BaseValue, 0.5f));
        }

        double money = PlayerPrefs.GetFloat("PlayerMoney", 100);
        _upgradeCostTxt.text = _upgradeCost.ToString();

        if (money >= _upgradeCost)
        {
            _upgradeBtn.interactable = true;
        }
        else if (money < _upgradeCost)
        {
            _upgradeBtn.interactable = false;
        }
    }

    // Coroutine per animare il numero crescente
    private IEnumerator AnimateNumber(Text txt, int from, int to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, elapsed / duration));
            txt.text = value.ToString() + _upgradeTxtName; // Aggiungi "HP" al valore
            elapsed += Time.deltaTime;
            yield return null;
        }
        txt.text = to.ToString() + _upgradeTxtName; // Assicurati che il numero finale sia quello giusto
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
