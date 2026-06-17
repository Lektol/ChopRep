using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int Hp = 5;
    private int CurrentHp = 5;
    [SerializeField] private float secCoolDown;
    [SerializeField] private Vector2 countTrees = new Vector2(2, 5);
    private bool canTakeDamage = true;
    public bool CanTakeDamage
    {
        get { return canTakeDamage; }
        private set { canTakeDamage = value; }  
    }
    [Header("Particle")]
    [SerializeField] private ParticleSystem particleSystemLeaves;
    [SerializeField] private Vector2 secondsBetweenLeavesFall = new Vector2(10f, 20f);
    private Transform PlayerTransform;
    [SerializeField] private float LeavesFallDistancePlayer = 15f;
    [SerializeField] private ParticleSystem particleSystemWood;
    private Animator _anim;
    private AudioSource _audioSource;


    void OnEnable()
    {
        CurrentHp = Hp;
        CanTakeDamage = true;
        StartCoroutine(LeavesDown());
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        PlayerTransform = FindObjectOfType<AxeController>().GetComponent<Transform>();
    }
    public void GetDamage(int dam)
    {
        CurrentHp -= dam;
        PlaySound();
        StartCoroutine(CoolDown(secCoolDown));
        particleSystemWood.Play();
        if (CurrentHp <= 0)
        {
            StartCoroutine(Death(0.3f)); //время анимации смерти
            return;
        }
        _anim.SetTrigger("TakeDam");
    }

    IEnumerator CoolDown(float sec)
    {
        CanTakeDamage = !CanTakeDamage;
        yield return new WaitForSeconds(sec);
        CanTakeDamage = !CanTakeDamage;
    }

    IEnumerator Death(float sec)
    {
        int CountTreesRandom = Random.Range((int)countTrees.x, (int)countTrees.y);
        InventorySystem.Instance.AddTrees(CountTreesRandom);
        EventManager.OnTreeDead?.Invoke();
        _anim.SetTrigger("Death");
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }

    IEnumerator LeavesDown()
    {
        yield return new WaitForSeconds(Random.Range(secondsBetweenLeavesFall.x, secondsBetweenLeavesFall.y));
        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        if(distance <= LeavesFallDistancePlayer) particleSystemLeaves.Play();
        StartCoroutine(LeavesDown());
    }

    public void PlaySound()
    {
        _audioSource.pitch = Random.Range(0.8f,1);
        _audioSource.Play();
    }
}
