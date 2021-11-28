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
        public void OverrideKeyText()
        {
            key1.text = "K1: " + K1;
            key2.text = "K2: " + K2;
        
        }

        public void SaveSettings()
        {
            SaveManager.SaveManager sm = new SaveManager.SaveManager();
            sm.Clear_Data();
            sm.AddKey("k1", K1);
            sm.AddKey("k2", K2);
            sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
        }

        public void SetKey(int k, string key)
        {
            switch (k)
            {
                case 1:
                    sm.Clear_Data();
                    sm.AddKey("k1", key);
                    sm.AddKey("k2", K2);
                    sm.AddKey("speed", speed);
                    K1 = key;
                    ScriptComponent.GetComponent<watcher>().Key1 = K1;
                    Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
                    sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
                    OverrideKeyText();
                    break;

                case 2:
                    sm.Clear_Data();
                    sm.AddKey("k1", K1);
                    sm.AddKey("k2", key);
                    sm.AddKey("speed", speed);
                    K2 = key;
                    ScriptComponent.GetComponent<watcher>().Key2 = K2;
                    Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
                    sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
                    OverrideKeyText();
                    break;
            }
        }

        public void ToggleAddKeyOverlay(int k)
        {
            whichKey = k;
            keyoverlay = !keyoverlay;
            KeyOverlay.SetActive(keyoverlay);
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
                sm.Read_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
                K1 = sm.GetKey("k1");
                K2 = sm.GetKey("k2");
                speed = sm.GetKey_float("speed");
                SpeedSlider.value = speed;
                ScriptComponent.GetComponent<watcher>().Key1 = K1;
                ScriptComponent.GetComponent<watcher>().Key2 = K2;
                OverrideKeyText();
            }
            else
            {
                sm.Clear_Data();
                sm.AddKey("k1", "X");
                sm.AddKey("k2", "Y");
                speed = 0.1f;
                SpeedSlider.value = speed;
                sm.AddKey("speed", speed);
                K1 = "X";
                K2 = "Y";
                ScriptComponent.GetComponent<watcher>().Key1 = K1;
                ScriptComponent.GetComponent<watcher>().Key2 = K2;
                Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
                sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
                OverrideKeyText();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;

            debugt.text = "Res: " + Screen.width + "x" + Screen.height + Environment.NewLine + "FPS: " +  Mathf.Ceil(fps).ToString();
            DebugContainer.SetActive(DebugToggle.isOn);
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

        public void SaveSpeed()
        {
            sm.Clear_Data();
            sm.AddKey("k1", K1);
            sm.AddKey("k2", K2);
            sm.AddKey("speed", speed);

            Directory.CreateDirectory(rootDrive + "\\osu!keyoverlay\\");
            sm.Save_Data(rootDrive + "\\osu!keyoverlay\\settings.txt");
            OverrideKeyText();
        }
    }
}