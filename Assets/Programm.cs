using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Programm
{
    public class Programm : MonoBehaviour
    {
        public GameObject DebugContainer;
        public Text debugt;
        public Toggle DebugToggle;
        public string K1;
        public string K2;
        public float speed;
        public Text key1;
        public Text key2;
        public bool keyoverlay = false;
        public GameObject KeyOverlay;
        public string rootDrive;
        public GameObject ScriptComponent;
        public bool settings = false;
        public GameObject SettingsOverlay;
        public GameObject UI;
        public bool ui_visibility = true;
        public int whichKey;
        private SaveManager.SaveManager sm = new SaveManager.SaveManager();
        public float deltaTime;
        public Slider SpeedSlider;
        public Camera MainCamera;
        public Color BGcolor;
        public Color KeyBaseColor;
        public Color KeyBorderColor;
        public Color KeyTextColor;
        public Toggle Fps;
        public GameObject Fade;
        public GameObject KeyObject1;
        public GameObject KeyObject2;
        public GameObject KeyText1;
        public GameObject KeyText2;
        public float KeyBorderWidth;
        public Slider BorderWidthSlider;
        public Toggle KeyName;
        public Toggle StartInner;
        public float KeyOpacity;
        public float NoteOpacity;
        public Slider KeyOpSlider;
        public Slider NoteOpSlider;
        public Toggle ShowKeyCount;
        public Toggle WipeCountOnStart;
        public Toggle ShowKeyText;
        public GameObject KeyCount1;
        public GameObject KeyCount2;
        public double Key1c, Key2c;


        public void EventCheck()
        {
            Debug.Log("Event has been triggered");
        }

        public void OverrideKeyText()
        {
            key1.text = "K1: " + K1;
            key2.text = "K2: " + K2;
        }

        public void SetKey(int k, string key)
        {
            switch (k)
            {
                case 1:
                    K1 = key;
                    ScriptComponent.GetComponent<watcher>().Key1 = K1;
                    SaveSettings();
                    break;

                case 2:

                    K2 = key;
                    ScriptComponent.GetComponent<watcher>().Key2 = K2;
                    SaveSettings();
                    break;
            }
        }

        public void ToggleAddKeyOverlay(int k)
        {
            whichKey = k;
            keyoverlay = !keyoverlay;
            KeyOverlay.SetActive(keyoverlay);
        }

        public void ToggleFps()
        {
            if (Fps.isOn == true)
            {
                Application.targetFrameRate = 60;
            }
            else
            {
                Application.targetFrameRate = 9999;
            }
            SaveSettings();
        }

        public void ToggleInnerNotes()
        {
         
            SaveSettings();
        }

        public void ToggleKeyCount()
        {
            KeyCount1.gameObject.SetActive(ShowKeyCount.isOn);
         
            KeyCount2.gameObject.SetActive(ShowKeyCount.isOn);

            SaveSettings();
        }

        public void ToggleWipeOnStart()
        {
            SaveSettings();
        }

        public void ToggleShowKeyText()
        {


            KeyText1.SetActive(ShowKeyText.isOn);
            KeyText2.SetActive(ShowKeyText.isOn);
            SaveSettings();
        }

        public void ToggleSettings()
        {
            settings = !settings;
            SettingsOverlay.SetActive(settings);
        }

        // Start is called before the first frame update
        private void Start()
        {
            rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
            if (File.Exists(rootDrive + "\\osu!keyoverlay\\settings.txt"))
            {
                LoadSettings();
            }
            else
            {
                CreateSettings();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;

            debugt.text = "Res: " + Screen.width + "x" + Screen.height + Environment.NewLine + "FPS: " + Mathf.Ceil(fps).ToString();
            KeyCount1.GetComponent<Text>().text = "" + Key1c;
            KeyCount2.GetComponent<Text>().text = "" + Key2c;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settings == false)
                {
                    ui_visibility = !ui_visibility;
                    UI.SetActive(ui_visibility);
                }
            }

            if (keyoverlay == true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    keyoverlay = !keyoverlay;
                    KeyOverlay.SetActive(keyoverlay);
                    return;
                }
                else if (Input.anyKeyDown)
                {
                    SetKey(whichKey, ScriptComponent.GetComponent<watcher>().currentKey);
                    keyoverlay = !keyoverlay;
                    KeyOverlay.SetActive(keyoverlay);
                }
            }

        }

        public void SetSpeed()
        {
            speed = SpeedSlider.value;
        }

        public void SetOpacity()
        {
            KeyOpacity = KeyOpSlider.value;
            NoteOpacity = NoteOpSlider.value;
        }

        public void SetKeyBorderWidth()
        {
            KeyBorderWidth = BorderWidthSlider.value;
            // since this slider has only 10 fixed values and not a float with 10*1000... numbers we can save the settings when slider value changed
            SaveSettings();
        }

        public void SaveSpeed()
        {
            // Code has been moved to SaveSettings(); im just lazy to remove SaveSpeed() function lol
            SaveSettings();
        }

        public void SaveSettings()
        {
            sm.Clear_Data();
            sm.AddKey("k1", K1);
            sm.AddKey("k2", K2);
            sm.AddKey("speed", speed);
            sm.AddKey("NoteColor", ScriptComponent.GetComponent<FormColorPicker>().NoteColor);
            sm.AddKey("BackgroundColor", ScriptComponent.GetComponent<FormColorPicker>().BackgroundColor);
            sm.AddKey("KeyBaseColor", ScriptComponent.GetComponent<FormColorPicker>().KeyBaseColor);
            sm.AddKey("KeyBorderColor", ScriptComponent.GetComponent<FormColorPicker>().KeyBorderColor);
            sm.AddKey("KeyTextColor", ScriptComponent.GetComponent<FormColorPicker>().KeyTextColor);
            sm.AddKey("KeyBorderWidth", KeyBorderWidth);
            sm.AddKey("KeyNameMode", KeyName.isOn);
            sm.AddKey("InnerNotes", StartInner.isOn);
            sm.AddKey("KeyOpacity", KeyOpacity);
            sm.AddKey("NoteOpacity", NoteOpacity);     
            sm.AddKey("ShowKeyCount", ShowKeyCount.isOn);
            sm.AddKey("Key1 PressCount", Key1c);
            sm.AddKey("Key2 PressCount", Key2c);
            sm.AddKey("WipeKeyCountOnStart", WipeCountOnStart.isOn);

            if (ShowKeyText.isOn == true)
            {
                sm.AddKey("ShowKeyText", 1);
            }
            else
            {
                sm.AddKey("ShowKeyText", 0);
            }
          
            sm.AddKey("60FPS", Fps.isOn);
            Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
            sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
            OverrideKeyText();
            OverrideUI();
        }

        public void LoadSettings()
        {
            sm.Read_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
            K1 = sm.GetKey("k1");
            K2 = sm.GetKey("k2");
            speed = sm.GetKey_float("speed");
            SpeedSlider.value = speed;
            KeyBorderWidth = sm.GetKey_float("KeyBorderWidth");
            BorderWidthSlider.value = KeyBorderWidth;
            ScriptComponent.GetComponent<watcher>().Key1 = K1;
            ScriptComponent.GetComponent<watcher>().Key2 = K2;
            ScriptComponent.GetComponent<FormColorPicker>().NoteColor = sm.GetKey("NoteColor");
            ScriptComponent.GetComponent<FormColorPicker>().BackgroundColor = sm.GetKey("BackgroundColor");
            ScriptComponent.GetComponent<FormColorPicker>().KeyBaseColor = sm.GetKey("KeyBaseColor");
            ScriptComponent.GetComponent<FormColorPicker>().KeyBorderColor = sm.GetKey("KeyBorderColor");
            ScriptComponent.GetComponent<FormColorPicker>().KeyTextColor = sm.GetKey("KeyTextColor");
            KeyName.isOn = sm.GetKey_bool("KeyNameMode");
            StartInner.isOn = sm.GetKey_bool("InnerNotes");
            KeyOpacity = sm.GetKey_float("KeyOpacity");
            NoteOpacity = sm.GetKey_float("NoteOpacity");
            KeyOpSlider.value = KeyOpacity;
            NoteOpSlider.value = NoteOpacity;         
            WipeCountOnStart.isOn = sm.GetKey_bool("WipeKeyCountOnStart");
   
            ShowKeyCount.isOn = sm.GetKey_bool("ShowKeyCount");
            Key1c = sm.GetKey_double("Key1 PressCount");
            Key2c = sm.GetKey_double("Key2 PressCount");

          
            if (sm.GetKey_int("ShowKeyText") == 0)
            {
                ShowKeyText.isOn = false;
            }
            else if (sm.GetKey_int("ShowKeyText") == 1)
            {
                ShowKeyText.isOn = true;
            }
            
            Fps.isOn = sm.GetKey_bool("60FPS");

            if (WipeCountOnStart.isOn)
            {
                Key1c = 0;
                Key2c = 0;
             
            }
          //  SaveSettings();
            OverrideKeyText();
            OverrideUI();
        }

        public void CreateSettings()
        {
            sm.Clear_Data();
            sm.AddKey("k1", "Y");
            sm.AddKey("k2", "X");
            speed = 0.1f;
            KeyBorderWidth = 0f;
            SpeedSlider.value = speed;
            BorderWidthSlider.value = KeyBorderWidth;
            sm.AddKey("speed", speed);
            sm.AddKey("NoteColor", "#ffffff");
            sm.AddKey("BackgroundColor", "#000000");
            sm.AddKey("KeyBaseColor", "#ffffff");
            sm.AddKey("KeyBorderColor", "#202020");
            sm.AddKey("KeyTextColor", "#000000");
            sm.AddKey("KeyBorderWidth", KeyBorderWidth);
            sm.AddKey("KeyNameMode", false);
            sm.AddKey("InnerNotes", false);          
            sm.AddKey("KeyOpacity", 1f);
            sm.AddKey("NoteOpacity", 1f);
            sm.AddKey("ShowKeyCount", false);
            sm.AddKey("Key1 PressCount", 0);
            sm.AddKey("Key2 PressCount", 0);
            sm.AddKey("WipeKeyCountOnStart", true);
            sm.AddKey("ShowKeyText", 1);
            sm.AddKey("60FPS", false);
            KeyOpacity = 1f;
            NoteOpacity = 1f;
            ShowKeyText.isOn = true;
            WipeCountOnStart.isOn = true;
            ShowKeyCount.isOn = false;
            KeyName.isOn = false;
            Fps.isOn = false;

            K1 = "X";
            K2 = "Y";
            Application.targetFrameRate = 9999;
            ScriptComponent.GetComponent<watcher>().Key1 = K1;
            ScriptComponent.GetComponent<watcher>().Key2 = K2;
            ScriptComponent.GetComponent<FormColorPicker>().NoteColor = "#ffffff";
            ScriptComponent.GetComponent<FormColorPicker>().BackgroundColor = "#000000";
            ScriptComponent.GetComponent<FormColorPicker>().KeyBaseColor = "#ffffff";
            ScriptComponent.GetComponent<FormColorPicker>().KeyBorderColor = "#202020";
            ScriptComponent.GetComponent<FormColorPicker>().KeyTextColor = "#000000";
  
        //    ScriptComponent.GetComponent<gameobjectScript>().Opacity = NoteOpacity;
            Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
            sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
            SaveSettings();
            OverrideKeyText();
            OverrideUI(); // why not \;-;/
        }

        public void ToggleDebug()
        {
            DebugContainer.SetActive(DebugToggle.isOn);
        }

        public void ToggleKeyName()
        {
            if (KeyName.isOn)
            {
                KeyText1.GetComponent<Text>().text = ScriptComponent.GetComponent<watcher>().Key1;
                KeyText2.GetComponent<Text>().text = ScriptComponent.GetComponent<watcher>().Key2;
            }
            else
            {
                KeyText1.GetComponent<Text>().text = "K1";
                KeyText2.GetComponent<Text>().text = "K2";
            }

            SaveSettings();
        }

        public void OverrideUI()
        {

           
            KeyText1.SetActive(ShowKeyText.isOn);
            KeyText2.SetActive(ShowKeyText.isOn);           
            KeyCount1.SetActive(ShowKeyCount.isOn);
            KeyCount2.SetActive(ShowKeyCount.isOn);
            
         
            if (KeyName.isOn)
            {
                KeyText1.GetComponent<Text>().text = ScriptComponent.GetComponent<watcher>().Key1;
                KeyText2.GetComponent<Text>().text = ScriptComponent.GetComponent<watcher>().Key2;
            }
            else
            {
                KeyText1.GetComponent<Text>().text = "K1";
                KeyText2.GetComponent<Text>().text = "K2";
            }
            if (Fps.isOn == true)
            {
                Application.targetFrameRate = 60;
            }
            else
            {
                Application.targetFrameRate = 9999;
            }
            ColorUtility.TryParseHtmlString(ScriptComponent.GetComponent<FormColorPicker>().BackgroundColor, out BGcolor);
            ColorUtility.TryParseHtmlString(ScriptComponent.GetComponent<FormColorPicker>().KeyBaseColor, out KeyBaseColor);
            ColorUtility.TryParseHtmlString(ScriptComponent.GetComponent<FormColorPicker>().KeyBorderColor, out KeyBorderColor);
            ColorUtility.TryParseHtmlString(ScriptComponent.GetComponent<FormColorPicker>().KeyTextColor, out KeyTextColor);
            Color TempKeyColor = KeyBaseColor;
            TempKeyColor.a = KeyOpacity;

            KeyObject1.GetComponent<Image>().color = TempKeyColor;
            KeyObject2.GetComponent<Image>().color = TempKeyColor;
            KeyObject1.GetComponent<Outline>().effectColor = KeyBorderColor;
            KeyObject2.GetComponent<Outline>().effectColor = KeyBorderColor;
            KeyObject1.GetComponent<Outline>().effectDistance = new Vector2(KeyBorderWidth, KeyBorderWidth);
            KeyObject2.GetComponent<Outline>().effectDistance = new Vector2(KeyBorderWidth, KeyBorderWidth);
            KeyText1.GetComponent<Text>().color = KeyTextColor;
            KeyText2.GetComponent<Text>().color = KeyTextColor;
            KeyCount1.GetComponent<Text>().color = KeyTextColor;
            KeyCount2.GetComponent<Text>().color = KeyTextColor;
            MainCamera.backgroundColor = BGcolor;
            Fade.GetComponent<Image>().color = BGcolor;
        }

        private void OnApplicationQuit()
        {
            SaveSettings();
        }
    }
}