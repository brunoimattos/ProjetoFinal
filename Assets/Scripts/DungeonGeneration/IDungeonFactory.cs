using UnityEngine;
using System.Collections;

public interface IDungeonFactory
{

	Dungeon createDungeon(int width, int height, float linearFactor);
}

