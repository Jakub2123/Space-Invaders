using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaidersController : MonoBehaviour
{
    public List<Invader> invadersPrefabs;
    [Range(1, 6)]
    public int rows = 6;
    [Range(1, 11)]
    public int invadersInRow = 11;
    public float topYPosition = 4f;
    public Transform container;

    private Invader[,] invaders;

    private const float INVADERS_DISTANCE = 1.5f * 0.75f;
    private const float MAX_POS_X = INVADERS_DISTANCE * 5 / 0.75f;
    private float maxXPosition;
    private float minXPosition;

    private int leftColumnIndex;
    private int rightColumnIndex;

    private bool goingRight;

    private float delay;
    private int amount;

    public void Start()
    {
        Prepare();
    }

    public void Prepare()
    {
        goingRight = true;
        int rowsOccupied = rows / invadersPrefabs.Count;
        rowsOccupied = rows % invadersPrefabs.Count == 0 ? 0 : 1;
        bool even = invadersInRow % 2 == 0;
        float offset = even ? INVADERS_DISTANCE / 2 : 0;
        float maxPosOffset = even ? 0 : INVADERS_DISTANCE / 2;

        float startX = -((invadersInRow - 1) / 2 * INVADERS_DISTANCE + offset);
        int distanceMultiplier = even ? Mathf.Max(0, (invadersInRow / 2) - 1) : invadersInRow / 2;

        leftColumnIndex = 0;
        rightColumnIndex = invadersInRow - 1;

        maxXPosition = MAX_POS_X - (INVADERS_DISTANCE * distanceMultiplier) + maxPosOffset;
        minXPosition = -maxXPosition;

        invaders = new Invader[invadersInRow, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < invadersInRow; j++)
            {
                var invader = Instantiate(invadersPrefabs[i / rowsOccupied], container);
                float x = startX + j * INVADERS_DISTANCE;
                invader.Setup(new Vector3(x, topYPosition - i * 0.75f, 0), OnInvaderDestroy, new Vector2Int(j, i));
                if (i == rows - 1)
                {
                    invader.StartShooting();
                }
                invaders[j, i] = invader;
                delay = 0.025f * amount / 2;

                StartCoroutine(MoveCoroutine());

            }

        }
        amount = rows * invadersInRow;

    }

    private void OnInvaderDestroy(Invader invader)
    {
        invaders[invader.ArrayPosition.x, invader.ArrayPosition.y] = null;
        if(invader.ArrayPosition.y !=0)
        {
            var higherInvader = invaders[invader.ArrayPosition.x, invader.ArrayPosition.y - 1];
            if(higherInvader!=null)
            {
                higherInvader.StartShooting();
            }

        }
        delay -= 0.0125f;
        ColumnCheck();
    }

    private void ColumnCheck()
    {
        int leftNulls = 0;
        int rightNulls = 0;
        bool recheck = false;
        for (int y = 0; y < invaders.GetLength(1); y++) 
        {
            leftNulls += invaders[leftColumnIndex, y] == null ? 1 : 0;
            rightNulls += invaders[rightColumnIndex, y] == null ? 1 : 0;
        }
        if(leftNulls == invaders.GetLength(1))
        {
            leftColumnIndex++;
            minXPosition -= INVADERS_DISTANCE;
            recheck = true;

        }

        
        if (rightNulls == invaders.GetLength(1))
        {
            rightColumnIndex--;
            maxXPosition -= INVADERS_DISTANCE;
            recheck = true;
        }
        if (recheck&& leftColumnIndex<=rightColumnIndex)
        {
            ColumnCheck();

        }
    }
    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            while (goingRight && container.transform.position.x < maxXPosition - 0.25f)
            {
                container.transform.position += Vector3.right * 0.25f;
                yield return new WaitForSeconds(delay);

            }
            if (goingRight)
            {
                container.transform.position += Vector3.down * 0.75f;
                goingRight = false;
            }
            yield return new WaitForSeconds(delay);



            while (!goingRight && container.transform.position.x > minXPosition + 0.25f)
            {
                container.transform.position += Vector3.left * 0.25f;
                yield return new WaitForSeconds(delay);

            }
            if (!goingRight)
            {
                container.transform.position += Vector3.down * 0.75f;
                goingRight = true;

            }
            yield return new WaitForSeconds(delay);

        }
    }
}
