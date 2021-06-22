using System;
using UnityEngine;
using Random = UnityEngine.Random;

public struct Cell
{
    public bool isAlive;

    public Cell(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
public class CellBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer_;
    [SerializeField] private Sprite[] sprites_;
    
    private bool isAlive = true;

    private void Start()
    {
        spriteRenderer_.sprite = sprites_[Random.Range(0, sprites_.Length)];
    }
    
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            UpdateColor(isAlive ? Color.white : Color.black);
        }
    }

    public void UpdateColor(Color color)
    {
        spriteRenderer_.color = color;
    }

    
}