using GKH;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public partial class watcher : MonoBehaviour
{
    public string currentKey = "";
    public bool K1, K2, menu = false, active1 = false, active2 = false;
    public string Key1 = "none", Key2 = "none";
    public Text ky1, ky2;
    public GameObject Object1, Object2, ParrentObject;
   

    private GlobalKeyboardHook _globalKeyboardHook;

    private void BeginnHook()
    {
        // Hooks only into specified Keys (here "A" and "B").
        //   _globalKeyboardHook = new GlobalKeyboardHook(new Keys[] { Keys.A, Keys.B });

        // Hooks into all keys.
        _globalKeyboardHook = new GlobalKeyboardHook();
        _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
    }

    private void OnApplicationQuit()
    {
        _globalKeyboardHook.Dispose();
    }

    private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
    {
        // EDT: No need to filter for VkSnapshot anymore. This now gets handled
        // through the constructor of GlobalKeyboardHook(...).
        if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
        {
            // Now you can access both, the key and virtual code
            Keys loggedKey = e.KeyboardData.Key;
            int loggedVkCode = e.KeyboardData.VirtualCode;

            currentKey = e.KeyboardData.Key.ToString();

            if (e.KeyboardData.Key.ToString() == Key1)
            {
                if (active1 == true)
                {
                    return;
                }
                else
                {
                    K1 = true;
                    ky1.text = "Key1: true";
                    Instantiate(Object1, ParrentObject.transform, false).name = "1";
                    active1 = true;
                }
            }
            if (e.KeyboardData.Key.ToString() == Key2)
            {
                if (active2 == true)
                {
                    return;
                }
                else
                {
                    K2 = true;
                    ky2.text = "Key2: true";
                    Instantiate(Object2, ParrentObject.transform, false).name = "2";
                    active2 = true;
                }
            }
        }

        if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
        {
           
            Keys loggedKey = e.KeyboardData.Key;
            int loggedVkCode = e.KeyboardData.VirtualCode;

            if (e.KeyboardData.Key.ToString() == Key1)
            {
                active1 = false;
                K1 = false;
                ky1.text = "Key1: false";
            }
            if (e.KeyboardData.Key.ToString() == Key2)
            {
                active2 = false;
                K2 = false;
                ky2.text = "Key2: false";
            }
        }
    }

    private void Start()
    {
        ParrentObject = GameObject.Find("keycanvas");
      
        BeginnHook();
    }
}