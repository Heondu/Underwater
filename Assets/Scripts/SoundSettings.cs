using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[System.Serializable]
public class AudioGroup
{
    [Range(-40, 0)]
    public float master = 0;
    [Range(-40, 0)]
    public float bgm = 0;
    [Range(-40, 0)]
    public float sfx = 0;

    public void CopyTo(AudioGroup audioGroup)
    {
        audioGroup.master = master;
        audioGroup.bgm = bgm;
        audioGroup.sfx = sfx;
    }
}

public class SoundSettings : MonoBehaviour
{
    private static SoundSettings instance;
    public static SoundSettings Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoundSettings>();
            return instance;
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    [Header("UI")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI masterText;
    [SerializeField] private TextMeshProUGUI bgmText;
    [SerializeField] private TextMeshProUGUI sfxText;

    [SerializeField] private AudioGroup audioGroup;
    private AudioGroup audioGroupTemp = new AudioGroup();

    private void Awake()
    {
        SoundSettingsUI soundSettingsUI = FindObjectOfType<SoundSettingsUI>();
        soundSettingsUI.onCancelPressed.AddListener(Cancel);
        soundSettingsUI.onAcceptPressed.AddListener(Accept);
        soundSettingsUI.onUIOpened.AddListener(OpenUI);
        soundSettingsUI.onUIClosed.AddListener(CloseUI);
    }

    private void Start()
    {
        Load();
        UpdateUI();
    }

    private void UpdateUI()
    {
        masterSlider.value = audioGroup.master;
        bgmSlider.value = audioGroup.bgm;
        sfxSlider.value = audioGroup.sfx;
    }

    private void OpenUI()
    {
        audioGroup.CopyTo(audioGroupTemp);
    }

    private void CloseUI()
    {

    }

    public void Cancel()
    {
        audioGroupTemp.CopyTo(audioGroup);
        UpdateUI();
    }

    public void Accept()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", audioGroup.master);
        PlayerPrefs.SetFloat("bgmVolume", audioGroup.bgm);
        PlayerPrefs.SetFloat("sfxVolume", audioGroup.sfx);
    }

    private void Load()
    {
        audioGroup.master = PlayerPrefs.GetInt("masterVolume");
        audioGroup.bgm = PlayerPrefs.GetInt("bgmVolume");
        audioGroup.sfx = PlayerPrefs.GetInt("sfxVolume");
    }

    public void SetMasterVolume(float value)
    {
        audioGroup.master = value <= masterSlider.minValue ? -80f : value;
        audioMixer.SetFloat("MasterVolume", audioGroup.master);
        UpdateUI();
    }
    public void SetBGMVolume(float value)
    {
        audioGroup.bgm = value <= bgmSlider.minValue ? -80f : value;
        audioMixer.SetFloat("BGMVolume", audioGroup.bgm);
        UpdateUI();
    }
    public void SetSFXVolume(float value)
    {
        audioGroup.sfx = value <= sfxSlider.minValue ? -80f : value;
        audioMixer.SetFloat("SFXVolume", audioGroup.sfx);
        UpdateUI();
    }
}
