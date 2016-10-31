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
        while(player.argent > player.basicBarrack.GetComponent<Baraquement>().cout && i++ < 100)
        {
            bool tower = UnityEngine.Random.Range(0, 2) == 1;
            Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(player.area.bounds.min.x, player.area.bounds.max.x), UnityEngine.Random.Range(player.area.bounds.min.y, player.area.bounds.max.y));
            if(!tower && player.isBarrackPlaceable(randomPosition))
            {
                player.buildBarrack(randomPosition, player.basicBarrackList[UnityEngine.Random.Range(0, player.basicBarrackList.Length)].GetComponent<Baraquement>().element);
            }
            if (tower && player.isTowerPlaceable(randomPosition))
            {
                player.buildTower(randomPosition, player.basicTowerList[UnityEngine.Random.Range(0, player.basicTowerList.Length)].GetComponent<Tour>().element);
            }
        }
    }
}
