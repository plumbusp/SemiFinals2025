using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [Header("Bullets Prefabs")]
    [SerializeField] Transform playerbulletsParent, enemyBulletParent;
    [Header("Bullets Prefabs")]
    [SerializeField] private Bullet playerBullet, basicElderlyBullet;
    [Header("Bullets Amounts")]
    [SerializeField] private int playerBulletCount, basicElderlyBulletsCount;


    public static BulletPooler Instance;
    private Dictionary<string, Queue<Bullet>> poolDictionary = new Dictionary<string, Queue<Bullet>>();

    private Bullet currentBullet; // To avoid creation of a new object each time GetPoolObject() is called
    private Queue<Bullet> sceneBullets = new();

    void Awake()
    {
        Queue<Bullet> playerBullets = new Queue<Bullet>();
        for (int i = 0; i < playerBulletCount; i++)
        {
            currentBullet = Instantiate(playerBullet, playerbulletsParent);
            currentBullet.Initialize(transform);
            playerBullets.Enqueue(currentBullet);
        }
        poolDictionary.Add("PlayerBullets", playerBullets);

        Queue<Bullet> basicElderlyBullets = new Queue<Bullet>();
        for (int i = 0; i < basicElderlyBulletsCount; i++)
        {
            currentBullet = Instantiate(basicElderlyBullet, enemyBulletParent);
            currentBullet.Initialize(transform);
            basicElderlyBullets.Enqueue(currentBullet);
        }
        poolDictionary.Add("BasicElderlyBullets", playerBullets);

        Instance = this;
    }

    public Bullet GetPoolObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("poolDictionary doesn't contain " + tag + " tag!");
            return null;
        }

        sceneBullets = poolDictionary[tag];
        currentBullet = sceneBullets.Dequeue();
        Debug.Log(currentBullet.name);
        sceneBullets.Enqueue(currentBullet);
        return currentBullet;
    }
}
