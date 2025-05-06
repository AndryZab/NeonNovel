using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ClickSound : MonoBehaviour
{
    private AudioClip clickSound;
    private AudioSource clickSoundSource;

    private HashSet<Button> registeredButtons = new HashSet<Button>();
    private HashSet<Toggle> registeredToggles = new HashSet<Toggle>();
    private HashSet<Dropdown> registeredDropdowns = new HashSet<Dropdown>();
    private HashSet<InputField> registeredInputs = new HashSet<InputField>();

    private void Start()
    {
        clickSoundSource = GetComponent<AudioSource>();
        clickSoundSource.volume = 0.5f;
        clickSound = Resources.Load<AudioClip>("Sounds/button");
        if (clickSound == null)
        {
            return;
        }

        InvokeRepeating(nameof(ScanUI), 0f, 0.1f);
    }

    private void ScanUI()
    {
        foreach (Button button in FindObjectsOfType<Button>(true))
        {
            if (registeredButtons.Contains(button)) continue;
            registeredButtons.Add(button);
            button.onClick.AddListener(() => clickSoundSource.PlayOneShot(clickSound));
        }

        foreach (Toggle toggle in FindObjectsOfType<Toggle>(true))
        {
            if (registeredToggles.Contains(toggle)) continue;
            registeredToggles.Add(toggle);

            EventTrigger trigger = toggle.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = toggle.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((_) => clickSoundSource.PlayOneShot(clickSound));
            trigger.triggers.Add(entry);
        }

        foreach (Dropdown dropdown in FindObjectsOfType<Dropdown>(true))
        {
            if (registeredDropdowns.Contains(dropdown)) continue;
            registeredDropdowns.Add(dropdown);
            dropdown.onValueChanged.AddListener((_) => clickSoundSource.PlayOneShot(clickSound));
        }

        foreach (InputField input in FindObjectsOfType<InputField>(true))
        {
            if (registeredInputs.Contains(input)) continue;
            registeredInputs.Add(input);
            input.onEndEdit.AddListener((_) => clickSoundSource.PlayOneShot(clickSound));
        }
    }
}
