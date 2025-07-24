using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damagetext;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [NaughtyAttributes.Button] // Create Button for Debug Animation
    public void Animate()
    {
        damagetext.text = Random.Range(1,100).ToString();
        animator.Play("Animate");
    }
}
