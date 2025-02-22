using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float _WaitSeconds;
    [SerializeField] private List<TutorialContent> _Contents;
    private int _index;

    private WaitForSeconds _waitSeconds;

    private void Awake()
    {
        _waitSeconds = new WaitForSeconds(_WaitSeconds);
        _index = 0;

        foreach (var content in _Contents)
        {
            content.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        _Contents[_index].Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if( _index+ 1 < _Contents.Count)
            {
                _Contents[_index].Skip();
                _index++;
                _Contents[_index].Show();
            }
            else
            {
                _Contents[_index].Skip();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
