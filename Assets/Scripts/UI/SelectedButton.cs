using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    private bool _selected;
    private Sprite _defaultSripte;
    private Sprite _selectedSripte;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ToggleButton);
        _defaultSripte = this.GetComponent<Image>().sprite;
        _selectedSripte = this.GetComponent<Button>().spriteState.selectedSprite;
    }

    public bool Selected => _selected;

    public void ToggleButton()
    {
        _selected = !_selected;
        this.GetComponent<Image>().sprite = _selected ? _selectedSripte : _defaultSripte;
    }

    public void ForceState(bool selected)
    {
        _selected = selected;
        this.GetComponent<Image>().sprite = _selected ? _selectedSripte : _defaultSripte;
    }
}
