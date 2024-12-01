using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSmall : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Block;

  void update()
    {
        if (!Block.activeSelf)
        {
            Player.SetActive(false);

        }

    }
}
