using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Attributes")]
    public bool canTakeFallDamage = true;
    public bool detectEnemies = false;
    public float purificationSpeedMultiplier = 1f;
    public bool autoReviveAvailable = false;

    [Header("Detection")]
    public float detectionRadius = 10f;

    [Header("Purification")]
    public float purificationCooldown = 2f;

    void Awake()
    {
        currentHealth = maxHealth;
    }
}