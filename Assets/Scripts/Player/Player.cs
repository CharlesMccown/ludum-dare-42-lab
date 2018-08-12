using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{    
    public Action OnDie;
    public Action OnWin;

    [SerializeField]
    private ParticleSystem deathParticles;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI resultText;
    [SerializeField]
    private Canvas canvas;

    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private Movement movement;
    private bool isDead = false;
    
    private readonly string LOST = "C0ongRatulatioNs huMan! DetecTed WeightLoss goal AcheivEd!" + Environment.NewLine +
        "YoU have Been vaporiseD by malfunctioning lab equipment." + Environment.NewLine +
        "hoPe you have a gOod daY To-to-tomorrow!";

    private readonly string WON = "You narrowly escaped the collapsing lab." + Environment.NewLine +
        "Warm sunlight streams down upon your face." + Environment.NewLine +
        "An electronic voice calls from somewhere behind you," + Environment.NewLine + 
        "\"goOdbYe HumAn\"";
        

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            ShowMenu();
    }

    internal void ShowMenu()
    {
        if (isDead == false)
            canvas.enabled = !canvas.enabled;
    }

    internal void Die(Vector3 killer)
    {
        isDead = true;
        resultText.text = LOST;
        scoreText.text = $"yOu surviVed foR {movement.Moves} Moves!";
        OnDie?.Invoke();
        rigidbody2D.AddForce(-Vector3.Normalize(killer)*.1f);
        deathParticles.Play();
        animator.SetBool("isDead", true);
        canvas.enabled = true;
    }
    internal void Win()
    {
        resultText.text = WON;
        scoreText.text = $"You reached the exit in only {movement.Moves} moves.";
        canvas.enabled = true;
    }
}
