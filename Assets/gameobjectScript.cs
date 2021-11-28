using UnityEngine;

public class gameobjectScript : MonoBehaviour
{
    public bool end = false;
    public float _speed;
    public GameObject scripts;
    private void FixedUpdate()
    {
        if (this.GetComponent<RectTransform>().position.y > 600)
        {
            Destroy(this.gameObject);
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
                end = true;
                return;
            }
            else
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y + _speed);
            }
        }
        if (this.name == "2")
        {
            if (scripts.GetComponent<watcher>().K2 == false)
            {
                
                end = true;
                return;
            }
            else
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y + _speed);
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        scripts = GameObject.Find("scripts");
        _speed = scripts.GetComponent<Programm.Programm>().speed;
    }
}