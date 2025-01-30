using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] private Bullet playerBullet;
    [Header("Bullets Amounts")]
    [SerializeField] private int playerBulletCount;


    public static BulletPooler Instance;
    private Dictionary<string, Queue<Bullet>> poolDictionary = new Dictionary<string, Queue<Bullet>>();

    private Bullet currentBullet; // To avoid creation of a new object each time GetPoolObject() is called
    private Queue<Bullet> sceneBullets = new();

    void Awake()
    {
        Queue<Bullet> playerBullets = new Queue<Bullet>();
        for (int i = 0; i < playerBulletCount; i++)
        {
            currentBullet = Instantiate(playerBullet, transform);
            currentBullet.Initialize(transform);
            playerBullets.Enqueue(currentBullet);
        }
        poolDictionary.Add("PlayerBullets", playerBullets);
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
        sceneBullets.Enqueue(currentBullet);
        return currentBullet;
    }
}
