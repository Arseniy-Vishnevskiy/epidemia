using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    public Image iconImage;
    public Sprite[] iconSprite;
    public Transform player;
    int aid_;
    int maxAid_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAid_ = player.GetComponent<PlayerCombat>().maxAID;
    }

    // Update is called once per frame
    void Update()
    {
        aid_ = player.GetComponent<PlayerCombat>().AID;
        iconImage.sprite = iconSprite[maxAid_ - aid_];
    }
}
