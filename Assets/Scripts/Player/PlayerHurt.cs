using System.Collections;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    [Header("Sprite Flash")]
    public SpriteRenderer spriteRenderer;
    public Material defaultMat;
    public Material flashMat;
    public float flashDuration = 0.1f;

    [Header("iFrames")]
    public float iFrameDuration = 1f;
    private bool isInvulnerable = false;

    void Start()
    {
        // Ensure defaultMat is assigned if not manually
        if (defaultMat == null)
            defaultMat = spriteRenderer.material;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        // Handle damage here (subtract health, play animation, etc.)
        Debug.Log("Player hurt!");

        StartCoroutine(FlashWhite());
        StartCoroutine(StartInvulnerability());
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.material = flashMat;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = defaultMat;
    }

    private IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(iFrameDuration);
        isInvulnerable = false;
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
}
