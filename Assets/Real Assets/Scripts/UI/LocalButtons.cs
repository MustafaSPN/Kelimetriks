using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalButtons : MonoBehaviour
{
    private void Awake()
    {
        int locale = 123;
        locale = PlayerPrefs.GetInt("KELIMETRIKS_LOCALE_SETTINGS");
        if (locale ==0 || locale==1)
        {
            StartCoroutine(SetLocale(locale));
        }
    }

    public void Turkish()
    {
        StartCoroutine(SetLocale(1));
    }

    public void English()
    {
        StartCoroutine(SetLocale(0));
    }

    private IEnumerator SetLocale(int local)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[local];
        PlayerPrefs.SetInt("KELIMETRIKS_LOCALE_SETTINGS",local);
    }
}
