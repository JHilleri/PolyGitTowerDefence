using UnityEngine;
using System.Collections;
using System;

public class RandomAI : AI
{
    public override void beginRound()
    {
        Joueur player = gameObject.GetComponent<Joueur>();
        int i = 0;
        if (player == null) throw new NullReferenceException("Player script not found");
        while( i++ < 100000)
        {
            Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(player.area.bounds.min.x, player.area.bounds.max.x), UnityEngine.Random.Range(player.area.bounds.min.y, player.area.bounds.max.y));
            Element randomElement = player.basicBarrackList[UnityEngine.Random.Range(0, player.basicBarrackList.Length)].newBuilding.element;
            if (player.isBarrackPlaceable(randomPosition, randomElement))
            {
                player.buildBarrack(randomPosition, randomElement);
            }
        }
    }
}
