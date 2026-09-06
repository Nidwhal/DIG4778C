using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int boardSize = 8;
    [SerializeField]
    private float squareSize = 1f;

    private void OnDrawGizmos()
    {
        //Create float value for board size to work on odd numbered boards
        float boardSizeCalc = boardSize;
        //Create dynamic starting position to center board
        Vector3 startPosition = new Vector3((-1 * boardSizeCalc/2 * squareSize)+(squareSize/2), (-1 * boardSizeCalc / 2 * squareSize)+(squareSize/2),0);
        //Loop for the board size
        for (int i = 0; i < boardSize; i++)
        {
            //Loop for the amount of squares in a row
            for(int x = 0; x < boardSize; x++)
            {
                //Set alternating colors for the squares
                if((i+x) % 2 == 0)
                {
                    Gizmos.color = Color.lightBlue;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                //Create a vector3 for moving where the gizmo is drawing the cubes.
                Vector3 current = startPosition + new Vector3(i * squareSize, x * squareSize, 0);

                //Draw the cube at the current location with the size of the squareSize
                Gizmos.DrawCube(current, new Vector3(squareSize, squareSize, 0));
            }
        }
    }
}
