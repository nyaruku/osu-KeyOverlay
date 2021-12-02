using System;
using System.Windows.Forms;
using UnityEngine;

public class FormColorPicker : MonoBehaviour
{
    public string NoteColor;
    public string BackgroundColor;
    public string KeyBaseColor;
    public string KeyBorderColor;
    public string KeyTextColor;
    public GameObject scripts;

    // Start is called before the first frame update
    private void Start()
    {
        //Initialize Color Dialog
    }

    public void ShowColorDialog()
    {
        ColorDialog cd = new ColorDialog()
        {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            ShowHelp = false,
            SolidColorOnly = false,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            NoteColor = HexConverter(cd.Color);
            scripts.GetComponent<Programm.Programm>().SaveSettings();
        }
    }


    public void ChangeBackGroundColor()
    {
        ColorDialog cd = new ColorDialog()
        {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            ShowHelp = false,
            SolidColorOnly = false,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            BackgroundColor = HexConverter(cd.Color);
            scripts.GetComponent<Programm.Programm>().SaveSettings();
        }
    }



    public void SetKeyBaseColor()
    {
        ColorDialog cd = new ColorDialog()
        {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            ShowHelp = false,
            SolidColorOnly = false,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            KeyBaseColor = HexConverter(cd.Color);
            scripts.GetComponent<Programm.Programm>().SaveSettings();
        }
    }

    public void SetKeyBorderColor()
    {
        ColorDialog cd = new ColorDialog()
        {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            ShowHelp = false,
            SolidColorOnly = false,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            KeyBorderColor = HexConverter(cd.Color);
            scripts.GetComponent<Programm.Programm>().SaveSettings();
        }
    }
    public void SetKeyTextColor()
    {
        ColorDialog cd = new ColorDialog()
        {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            ShowHelp = false,
            SolidColorOnly = false,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            KeyTextColor = HexConverter(cd.Color);
            scripts.GetComponent<Programm.Programm>().SaveSettings();
        }
    }

    private static String HexConverter(System.Drawing.Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}