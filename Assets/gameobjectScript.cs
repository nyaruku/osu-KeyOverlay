using UnityEngine;
using UnityEngine.UI;

public class gameobjectScript : MonoBehaviour
{
    public bool end = false;
    public float _speed;
    public GameObject scripts;
    public Color NoteColor;
    public bool inner = false;
    public float Opacity=1f;
    

    private void FixedUpdate()
    {
        if (this.GetComponent<RectTransform>().position.y > 600)
        {
            Destroy(this.gameObject);
            return;
        }

        if (end)
        {
            this.GetComponent<RectTransform>().position = new Vector2(this.GetComponent<RectTransform>().position.x, this.GetComponent<RectTransform>().position.y + _speed);
            return;
        }

        if (this.name == "1")
        {
            if (scripts.GetComponent<watcher>().K1 == false)
            {
                
                scripts.GetComponent<Programm.Programm>().Key1c++;
                end = true;           
                return;
            }
            else
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y + _speed);
                return;
            }
        }
        if (this.name == "2")
        {
            if (scripts.GetComponent<watcher>().K2 == false)
            {
             
                scripts.GetComponent<Programm.Programm>().Key2c++;
                end = true;           
                return;
            }
            else
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y + _speed);
                return;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        scripts = GameObject.Find("scripts");

        if (scripts.GetComponent<Programm.Programm>().StartInner.isOn)
        {
            this.GetComponent<RectTransform>().position = new Vector2(this.GetComponent<RectTransform>().position.x, this.GetComponent<RectTransform>().position.y - 66);
        }
        Opacity = scripts.GetComponent<Programm.Programm>().NoteOpacity;

        ColorUtility.TryParseHtmlString(scripts.GetComponent<FormColorPicker>().NoteColor, out NoteColor);
        Color TempNoteColor = NoteColor;
        TempNoteColor.a = Opacity;
        this.GetComponent<Image>().color = TempNoteColor;
        _speed = scripts.GetComponent<Programm.Programm>().speed;
    }
}