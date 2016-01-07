using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
   /*
    public GameObject LeftButton;
    public GameObject RightButton;
   	public GameObject AttackButton;
	public GameObject JumpButton;
	 */
	public GameObject OnScreenButtons;
    public bool TextToToggle = true;

    public void ToggleMenu(GameObject SettingsMenu)
    {
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void ToggleButtons()
    {
        if (TextToToggle == false)
        {
         	/*
          	LeftButton.SetActive(!LeftButton.activeSelf);
         	RightButton.SetActive(!RightButton.activeSelf);
         	AttackButton.SetActive(!AttackButton.activeSelf);
			JumpButton.SetActive(!JumpButton.activeSelf);
			 */
			OnScreenButtons.SetActive(!OnScreenButtons.activeSelf);
        }
    }

    public void ButtonOrTouchToggle(GameObject Player)
    {
        PlayerButtonControls pbc = Player.gameObject.GetComponent<PlayerButtonControls>();
        PlayerTouchControls ptc = Player.gameObject.GetComponent<PlayerTouchControls>();

        if (        (pbc.enabled && !ptc.enabled) // button controls enabled
            ||      (!pbc.enabled && ptc.enabled) // touch controls enabled
           )
        {
            pbc.enabled = !pbc.enabled;
            ptc.enabled = !ptc.enabled;

            TextToToggle = !TextToToggle;
        }

    }

    public void TextToggleText(Text TextToggle)
    {
        if (TextToToggle == true)
        {
            TextToggle.text = "Button Enabled";
        }
        if (TextToToggle == false)
        {
            TextToggle.text = "Touch Enabled";
        }
    }

    public void RestartLevel(string SceneName)
    {
        Application.LoadLevel(SceneName);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
